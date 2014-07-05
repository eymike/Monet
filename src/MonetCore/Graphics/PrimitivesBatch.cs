using System;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace MonetCore.Graphics
{
    public class PrimitivesBatch<VertexType> where VertexType : struct
    {
        private MaterialInstance m_primMaterial;

        const int BUFFER_SIZE = 512;
        SharpDX.Direct3D11.Buffer m_primitiveBuffer;
        VertexBufferBinding m_bufferBinding;
        VertexType[] m_vertexArray = new VertexType[BUFFER_SIZE];
        
        int m_positionInBuffer = 0;
        PrimitiveTopology m_topology;
        DeviceContext1 m_renderingContext;
        bool m_begun = false;

        public PrimitivesBatch(GraphicsDevice graphics, MaterialInstance primitiveMaterial)
        {
            m_primMaterial = primitiveMaterial;

            var vertexBufferSizeInBytes = Marshal.SizeOf(typeof(VertexType)) * BUFFER_SIZE;

            BufferDescription desc = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = vertexBufferSizeInBytes,
                StructureByteStride = 0,
                Usage = ResourceUsage.Dynamic
            };

            m_primitiveBuffer = new SharpDX.Direct3D11.Buffer(graphics.Device, desc);
            m_bufferBinding = new VertexBufferBinding(m_primitiveBuffer, Marshal.SizeOf(typeof(VertexType)), 0);
        }

        public void Begin(DeviceContext1 context, Camera camera, Matrix world, PrimitiveTopology topology)
        {
            if(m_begun)
            {
                throw new InvalidOperationException("Cannot call Begin without first calling end.");
            }

            m_topology = topology;
            m_positionInBuffer = 0;
            m_renderingContext = context;
            m_primMaterial.World = world;

            camera.Bind(m_renderingContext);
            m_primMaterial.Material.Bind(m_renderingContext);
            m_primMaterial.Bind(m_renderingContext);
            m_renderingContext.InputAssembler.PrimitiveTopology = m_topology;
            m_renderingContext.InputAssembler.SetVertexBuffers(0, m_bufferBinding);

            m_begun = true;
        }

        public void AddVertex(VertexType v1)
        {
            m_vertexArray[m_positionInBuffer] = v1;
            m_positionInBuffer += 1;
            if(BUFFER_SIZE - m_positionInBuffer < D3DHelper.PrimitiveSizeForTopology(m_topology))
            {
                Flush();
            }
        }

        public void AddLine(VertexType v1, VertexType v2)
        {
            AddVertex(v1);
            AddVertex(v2);
        }

        public void AddTriangle(VertexType v1, VertexType v2, VertexType v3)
        {
            AddVertex(v1);
            AddVertex(v2);
            AddVertex(v3);
        }

        public void AddPrimitives(params VertexType[] verts)
        {
            foreach(var vertex in verts)
            {
                AddVertex(vertex);
            }
        }

        public void End()
        {
            if(!m_begun)
            {
                throw new InvalidOperationException("Cannot call end without first calling begin.");
            }

            m_begun = false;
            Flush();
        }

        private void Flush()
        {
            using(var dataStream = 
                new DataStream(
                    m_renderingContext.MapSubresource(m_primitiveBuffer, 0, MapMode.WriteDiscard, MapFlags.None).DataPointer, 
                    Marshal.SizeOf(typeof(VertexType)) * BUFFER_SIZE, 
                    false, 
                    true))
            {
                dataStream.WriteRange<VertexType>(m_vertexArray);
            }

            m_renderingContext.UnmapSubresource(m_primitiveBuffer, 0);
            m_renderingContext.Draw(m_positionInBuffer, 0);
            m_positionInBuffer = 0;
        }
    }
}

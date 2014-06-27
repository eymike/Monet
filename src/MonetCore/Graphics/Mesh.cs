using System;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.Direct3D11;

namespace MonetCore.Graphics
{
    using DXBuffer = SharpDX.Direct3D11.Buffer;

    public class MeshInstance
    {
        private DXBuffer m_vertexBuffer;
        private DXBuffer m_indexBuffer;

        public MeshInstance(DXBuffer vertex, DXBuffer index)
        {
            m_vertexBuffer = vertex;
            m_indexBuffer = index;
        }
    }

    public abstract class Mesh
    {
        protected DXBuffer m_vertexBuffer;
        protected DXBuffer m_indexBuffer;

        public MeshInstance CreateMeshInstance()
        {
            return new MeshInstance(m_indexBuffer, m_indexBuffer);
        }
    }

    public class Mesh<VertexType> : Mesh where VertexType : struct 
    {
        public Mesh(GraphicsDevice graphics, VertexType[] verts, int[] inds)
        {
            var vertexBufferSizeInBytes = Marshal.SizeOf(typeof(VertexType)) * verts.Length;

            var vertexBufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = vertexBufferSizeInBytes,
                StructureByteStride = 0,
                Usage = ResourceUsage.Default
            };

            using(var datastream = new DataStream(vertexBufferSizeInBytes, true, true))
            {
                datastream.WriteRange<VertexType>(verts);
                m_vertexBuffer = new DXBuffer(graphics.Device, datastream, vertexBufferDescription);
            }

            var indexBufferSizeInBytes = sizeof(int) * inds.Length;

            var indexBufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.IndexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = indexBufferSizeInBytes,
                StructureByteStride = 0,
                Usage = ResourceUsage.Default
            };

            using (var datastream = new DataStream(vertexBufferSizeInBytes, true, true))
            {
                datastream.WriteRange<int>(inds);
                m_indexBuffer = new DXBuffer(graphics.Device, datastream, indexBufferDescription);
            }
        }
    }
}

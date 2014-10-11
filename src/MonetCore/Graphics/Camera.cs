using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;


namespace MonetCore.Graphics
{
    public class Camera
    {
        public Matrix Projection
        {
            get { return m_cameraParams.Proj; }
            private set { m_cameraParams.Proj = Matrix.Transpose(value); }
        }

        public ViewportF m_viewport;
        public ViewportF Viewport
        {
            get { return m_viewport; }
            set
            {
                m_viewport = value;
                m_cameraParams.ScreenDimensions = new Vector2(m_viewport.Width, m_viewport.Height);
            }
        }

        public SceneNode SceneNode { get; set; }

        private CameraParams m_cameraParams;
        private SharpDX.Direct3D11.Buffer m_viewParamsBuffer; 

        public Camera(GraphicsDevice graphics, float near, float far, float aspect, float fov)
        {
            Projection = Matrix.PerspectiveFovRH(fov, aspect, near, far);
            SceneNode = new MonetCore.SceneNode();

            BufferDescription desc = new BufferDescription
            {
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = Marshal.SizeOf(typeof(CameraParams)),
                StructureByteStride = 0,
                Usage = ResourceUsage.Default
            };

            Viewport = new ViewportF(0, 0, graphics.ScreenDimensions.X, graphics.ScreenDimensions.Y);

            m_viewParamsBuffer = new SharpDX.Direct3D11.Buffer(graphics.Device, desc);
        }

        public void Bind(DeviceContext1 context)
        {
            var globalTransform = SceneNode.GlobalTransform;

            var forward = Vector3.TransformNormal(-Vector3.UnitZ, globalTransform);
            var up = Vector3.TransformNormal(Vector3.UnitY, globalTransform);
            var origin = Vector3.TransformCoordinate(Vector3.Zero, globalTransform);

            context.Rasterizer.SetViewport(Viewport);

            m_cameraParams.View = Matrix.Transpose(Matrix.LookAtRH(origin, origin + forward, up));
            context.UpdateSubresource<CameraParams>(ref m_cameraParams, m_viewParamsBuffer);
            context.VertexShader.SetConstantBuffer(0, m_viewParamsBuffer);
            context.GeometryShader.SetConstantBuffer(0, m_viewParamsBuffer);
        }
    }
}

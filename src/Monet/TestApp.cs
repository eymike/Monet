using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MonetCore;
using MonetCore.Graphics;
using MonetCore.Graphics.Geometry;

using SharpDX;

namespace Monet
{
    public class TestApp : MonetApp
    {
        PrimitivesBatch<VertexPositionColor> m_primBatch;
        Camera m_camera;
        const float rotRate = 1.0f;
        float rotation = 0.0f;

        protected override void LoadContent()
        {
            base.LoadContent();

            var graphics = (GraphicsDeviceService)Services.GetService(typeof(GraphicsDeviceService));
            var content = (ContentManager)Services.GetService(typeof(ContentManager));
            
            var vShader = content.Load<SharpDX.Direct3D11.VertexShader>(@"Content\vs_VertexPositionColor.cso");
            var pShader = content.Load<SharpDX.Direct3D11.PixelShader>(@"Content\ps_VertexPositionColor.cso");
            
            m_primBatch = new PrimitivesBatch<VertexPositionColor>(
                graphics.GrapicsDevice, 
                Material.Create(graphics.GrapicsDevice, vShader, null, pShader, VertexPositionColor.GetInputElements()).CreateInstance<MaterialInstance>());

            m_camera = new Camera(graphics.GrapicsDevice, 0.5f, 5.0f, 4.0f / 3.0f, MonetMath.ToRadians(90.0f));

        }

        public override void Update()
        {
            FrameTimer timer = (FrameTimer)Services.GetService(typeof(FrameTimer));

            rotation += (rotRate * (float)timer.ElapsedTime.TotalSeconds);
        }

        public override void Render()
        {
            var graphics = (GraphicsDeviceService)Services.GetService(typeof(GraphicsDeviceService));
            graphics.GrapicsDevice.Clear(SharpDX.Color.SeaGreen);
            base.Render();

            var world = Matrix.Scaling(0.4f) * Matrix.RotationY(rotation) * Matrix.Translation(new Vector3(0, 0, -1));
           // var world = Matrix.Identity;
            Vector4 color = new Vector4(1, 1, 1, 1);
            float s = 0.5f;

            m_primBatch.Begin(graphics.GrapicsDevice.ImmediateContext, m_camera, world, SharpDX.Direct3D.PrimitiveTopology.LineList);           
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, -s, -s), color), new VertexPositionColor(new Vector3(-s, s, -s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, s, -s), color), new VertexPositionColor(new Vector3(-s, s, s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, s, s), color), new VertexPositionColor(new Vector3(-s, -s, s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, -s, s), color), new VertexPositionColor(new Vector3(-s, -s, -s), color));

            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, -s, -s), color), new VertexPositionColor(new Vector3(s, -s, -s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, s, -s), color), new VertexPositionColor(new Vector3(s, s, -s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, s, s), color), new VertexPositionColor(new Vector3(s, s, s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(-s, -s, s), color), new VertexPositionColor(new Vector3(s, -s, s), color));
            
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(s, -s, -s), color), new VertexPositionColor(new Vector3(s, s, -s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(s, s, -s), color), new VertexPositionColor(new Vector3(s, s, s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(s, s, s), color), new VertexPositionColor(new Vector3(s, -s, s), color));
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(s, -s, s), color), new VertexPositionColor(new Vector3(s, -s, -s), color));
            
            m_primBatch.AddLine(new VertexPositionColor(new Vector3(0, 1, 0), new Vector4(1, 0, 1, 1)), new VertexPositionColor(new Vector3(0, -1, 0), new Vector4(0, 1, 1, 1)));
            m_primBatch.End();

            graphics.GrapicsDevice.Present();
        }
    }
}

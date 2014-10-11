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
        PrimitivesBatch<VertexPositionColorSize> m_primBatch;
        Camera m_camera;
        
        protected override void LoadContent()
        {
            base.LoadContent();

            var graphics = (GraphicsDeviceService)Services.GetService(typeof(GraphicsDeviceService));
            var content = (ContentManager)Services.GetService(typeof(ContentManager));
            
            var vShader = content.Load<SharpDX.Direct3D11.VertexShader>(@"Content\vs_VertexPositionColorSize.cso");
            var gShader = content.Load<SharpDX.Direct3D11.GeometryShader>(@"Content\gs_VertexPositionColorSize.cso");
            var pShader = content.Load<SharpDX.Direct3D11.PixelShader>(@"Content\ps_VertexPositionColor.cso");
            
            m_primBatch = new PrimitivesBatch<VertexPositionColorSize>(
                graphics.GrapicsDevice,
                Material.Create(
                graphics.GrapicsDevice, 
                vShader, 
                gShader, 
                pShader,
                true,
                SharpDX.Direct3D11.CullMode.Back,
                SharpDX.Direct3D11.FillMode.Solid,
                VertexPositionColorSize.GetInputElements()).CreateInstance<MaterialInstance>());

            m_camera = new Camera(graphics.GrapicsDevice, 0.5f, 5.0f, 4.0f / 3.0f, MonetMath.ToRadians(90.0f));
        }
    }
}

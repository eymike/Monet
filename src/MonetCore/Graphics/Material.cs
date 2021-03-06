﻿using System;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace MonetCore.Graphics
{
    public class MaterialInstance
    {
        private PerObjectVars m_vars;
        private SharpDX.Direct3D11.Buffer m_constantBuffer;

        private readonly Material m_material;
        public Material Material
        {
            get { return m_material; }
        }

        public Matrix World
        {
            get { return m_vars.World; }
            set { m_vars.World = Matrix.Transpose(value); }
        }

        public MaterialInstance(GraphicsDevice graphics, Material material)
        {
            m_material = material;

            m_vars.World = Matrix.Identity;

            var constantBufferSize = Marshal.SizeOf(typeof(PerObjectVars));

            BufferDescription desc = new BufferDescription
            {
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = constantBufferSize,
                StructureByteStride = 0,
                Usage = ResourceUsage.Default
            };

            m_constantBuffer = new SharpDX.Direct3D11.Buffer(graphics.Device, desc);
        }

        public virtual void Bind(DeviceContext1 context)
        {
            context.UpdateSubresource<PerObjectVars>(ref m_vars, m_constantBuffer);
            context.VertexShader.SetConstantBuffer(1, m_constantBuffer);
        }
    }

    public class Material
    {
        private readonly GraphicsDevice m_device;
        private readonly VertexShader m_vShader;
        private readonly GeometryShader m_gShader;
        private readonly PixelShader m_pShader;
        private readonly InputLayout m_layout;
        private readonly RasterizerState m_rasterizerState;
        private readonly DepthStencilState m_depthState;

        public static bool operator==(Material left, Material right)
        {
            return
                left.m_vShader == right.m_vShader &&
                left.m_gShader == right.m_gShader &&
                left.m_pShader == right.m_pShader &&
                left.m_depthState == right.m_depthState &&
                left.m_rasterizerState == right.m_rasterizerState;
        }

        public static bool operator !=(Material left, Material right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj is Material) && (Material)obj == this;
        }

        public static Material Create(
            GraphicsDevice graphics, 
            VertexShader vShader, 
            GeometryShader gShader, 
            PixelShader pShader, 
            bool depthEnable,
            CullMode cullMode, 
            FillMode fillMode,
            InputElement[] elements)
        {
            var layout = new InputLayout(graphics.Device, (byte[])vShader.Tag, elements);
            return new Material(graphics, vShader, gShader, pShader, depthEnable, cullMode, fillMode, layout);
        }

        private Material(
            GraphicsDevice graphics, 
            VertexShader vShader, 
            GeometryShader gShader, 
            PixelShader pShader,
            bool depthEnable,
            CullMode cullMode,
            FillMode fillMode,
            InputLayout layout)
        {
            m_device = graphics;
            m_vShader = vShader;
            m_gShader = gShader;
            m_pShader = pShader;
            m_layout = layout;

            var dsDesc = new DepthStencilStateDescription();
            dsDesc.DepthComparison = Comparison.Less;
            dsDesc.DepthWriteMask = DepthWriteMask.All;
            dsDesc.IsDepthEnabled = true;
            

            m_depthState = new DepthStencilState(graphics.Device, dsDesc);

            var rDesc = new RasterizerStateDescription
            {
                CullMode = cullMode,
                FillMode = fillMode
            };

            m_rasterizerState = new RasterizerState(graphics.Device, rDesc);
        }

        public void Bind(DeviceContext1 context)
        {
            context.InputAssembler.InputLayout = m_layout;
            context.VertexShader.Set(m_vShader);
            context.GeometryShader.Set(m_gShader);
            context.PixelShader.Set(m_pShader);

            context.Rasterizer.State = m_rasterizerState;
            context.OutputMerger.DepthStencilState = m_depthState;
        }

        public MaterialInstanceType CreateInstance<MaterialInstanceType>() where MaterialInstanceType : MaterialInstance
        {
            return (MaterialInstanceType)Activator.CreateInstance(typeof(MaterialInstanceType), m_device, this);
        }

        public MaterialInstanceType CreateInstance<MaterialInstanceType>(params object[] args) where MaterialInstanceType :MaterialInstance
        {
            return (MaterialInstanceType)Activator.CreateInstance(typeof(MaterialInstanceType), m_device, this, args);
        }
    }
}

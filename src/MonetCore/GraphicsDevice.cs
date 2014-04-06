using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace MonetCore
{
    public class GraphicsDeviceService
    {
        private readonly GraphicsDevice m_device;

        public GraphicsDeviceService(GraphicsDevice device)
        {
            m_device = device;
        }

        public GraphicsDevice GrapicsDevice { get { return m_device; } }
    }

    public class GraphicsDevice : IDisposable
    {
        private readonly SharpDX.Direct3D11.Device1 m_device;
        public SharpDX.Direct3D11.Device1 Device
        {
            get { return m_device; }
        }

        private readonly SharpDX.DXGI.SwapChain m_swapChain;

        private RenderTargetView m_renderTarget;
        private DepthStencilView m_depthStencil;

        private DeviceContext1 m_immContext;
        public DeviceContext1 ImmediateContext
        {
            get { return m_immContext; }
        }

        public GraphicsDevice(Form form)
        {
            DeviceCreationFlags flags = DeviceCreationFlags.BgraSupport;

            flags |= DeviceCreationFlags.Debug;

            FeatureLevel[] featureLevels = 
            { 
                FeatureLevel.Level_11_1,
                FeatureLevel.Level_11_0,
                FeatureLevel.Level_10_1,
                FeatureLevel.Level_10_0
            };

            var device = new SharpDX.Direct3D11.Device(DriverType.Hardware, flags, featureLevels);
            Monet.LogMsg(String.Format("Created Device with FeatureLevel {0}", device.FeatureLevel));

            m_device = device.QueryInterface<SharpDX.Direct3D11.Device1>();

            var dxgiDevice = m_device.QueryInterface<SharpDX.DXGI.Device2>();
            var dxgiFactory = new SharpDX.DXGI.Factory().QueryInterface<SharpDX.DXGI.Factory2>();

            var swapChainDesc = new SwapChainDescription1()
            {
                BufferCount = 2,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Format = Format.R8G8B8A8_UNorm,
                Usage = Usage.RenderTargetOutput,
            };

            m_swapChain = dxgiFactory.CreateSwapChainForHwnd(dxgiDevice, form.Handle, ref swapChainDesc, null, null);

            form.ResizeEnd += form_ResizeEnd;

            m_immContext = m_device.ImmediateContext1;

            ResetDevice(form.Width, form.Height);
        }

        public void ResetDevice(int width, int height)
        {
            if (m_renderTarget != null) { Monet.SafeDispose(m_renderTarget); }
            if (m_depthStencil != null) { Monet.SafeDispose(m_depthStencil); }

            using(var backBuffer = m_swapChain.GetBackBuffer<Texture2D>(0))
            {
                m_renderTarget = new RenderTargetView(m_device, backBuffer);
            }

            var depthStencilDesc = new Texture2DDescription()
            {
                Format = Format.D24_UNorm_S8_UInt,
                ArraySize = 1,
                Width = width,
                Height = height,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                BindFlags = BindFlags.DepthStencil
            };
           
            using(var depthStecilBuffer = new Texture2D(m_device, depthStencilDesc))
            {
                m_depthStencil = new DepthStencilView(m_device, depthStecilBuffer);
            }
        }

        void form_ResizeEnd(object sender, EventArgs e)
        {
            var form = sender as Form;
            ResetDevice(form.Width, form.Height);
        }

        public void Clear(Color clearColor)
        {
            m_immContext.ClearRenderTargetView(m_renderTarget, clearColor.ToColor4());
            m_immContext.ClearDepthStencilView(m_depthStencil, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1, 0);
        }

        public void Present()
        {
            m_swapChain.Present(1, PresentFlags.None);
        }

        public void Dispose()
        {
            Monet.SafeDispose(m_renderTarget);
            Monet.SafeDispose(m_depthStencil);
            Monet.SafeDispose(m_swapChain);
            Monet.SafeDispose(m_immContext);
            Monet.SafeDispose(m_device);
        }
    }
}

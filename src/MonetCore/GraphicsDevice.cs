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
    public class GraphicsDevice
    {
        private readonly SharpDX.Direct3D11.Device1 m_device;
        private readonly SharpDX.DXGI.SwapChain m_swapChain;

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
            m_device = device.QueryInterface<SharpDX.Direct3D11.Device1>();

            var dxgiDevice = m_device.QueryInterface<SharpDX.DXGI.Device>();
            var dxgiFactory = new SharpDX.DXGI.Factory().QueryInterface<SharpDX.DXGI.Factory2>();


            var swapChainDesc = new SwapChainDescription1()
            {
                BufferCount = 2,
                SampleDescription = new SampleDescription() { Count = 0, Quality = 1 },
                SwapEffect = SwapEffect.FlipSequential
            };

            m_swapChain = dxgiFactory.CreateSwapChainForHwnd(dxgiDevice, form.Handle, ref swapChainDesc, null, null);

        }

    }
}

using System;
using System.IO;

using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace MonetCore.Content
{
    public class VertexShaderContentLoader : ContentLoader, IContentLoader
    {
        public Type AssetType { get { return typeof(SharpDX.Direct3D11.VertexShader); } }

        public VertexShaderContentLoader(MonetServiceProvider serviceProvider) : base(serviceProvider) { }

        public object Load(Uri uri)
        {
            var graphicsDevice = m_services.GetService(typeof(GraphicsDeviceService)) as GraphicsDeviceService;
            var device = graphicsDevice.GrapicsDevice.Device;
            var byteCode = File.ReadAllBytes(uri.AbsolutePath);
            
            var vShader = new VertexShader(device, File.ReadAllBytes(uri.AbsolutePath));
            vShader.Tag = byteCode;
            
            return vShader;
        }
    }

    public class PixelShaderContentLoader : ContentLoader, IContentLoader
    {
        public Type AssetType { get { return typeof(SharpDX.Direct3D11.PixelShader); } }

        public PixelShaderContentLoader(MonetServiceProvider serviceProvider) : base(serviceProvider) { }

        public object Load(Uri uri)
        {
            var graphicsDevice = m_services.GetService(typeof(GraphicsDeviceService)) as GraphicsDeviceService;
            var device = graphicsDevice.GrapicsDevice.Device;

            return new PixelShader(device, File.ReadAllBytes(uri.AbsolutePath));
        }
    }

    public class GeometryShaderContentLoader : ContentLoader, IContentLoader
    {
        public Type AssetType { get { return typeof(SharpDX.Direct3D11.GeometryShader); } }

        public GeometryShaderContentLoader(MonetServiceProvider serviceProvider) : base(serviceProvider) { }

        public object Load(Uri uri)
        {
            var graphicsDevice = m_services.GetService(typeof(GraphicsDeviceService)) as GraphicsDeviceService;
            var device = graphicsDevice.GrapicsDevice.Device;

            return new GeometryShader(device, File.ReadAllBytes(uri.AbsolutePath));
        }
    }

    public class DomainShaderContentLoader : ContentLoader, IContentLoader
    {
        public Type AssetType { get { return typeof(SharpDX.Direct3D11.DomainShader); } }

        public DomainShaderContentLoader(MonetServiceProvider serviceProvider) : base(serviceProvider) { }

        public object Load(Uri uri)
        {
            var graphicsDevice = m_services.GetService(typeof(GraphicsDeviceService)) as GraphicsDeviceService;
            var device = graphicsDevice.GrapicsDevice.Device;

            return new DomainShader(device, File.ReadAllBytes(uri.AbsolutePath));
        }
    }

    public class HullShaderContentLoader : ContentLoader, IContentLoader
    {
        public Type AssetType { get { return typeof(SharpDX.Direct3D11.HullShader); } }

        public HullShaderContentLoader(MonetServiceProvider serviceProvider) : base(serviceProvider) { }

        public object Load(Uri uri)
        {
            var graphicsDevice = m_services.GetService(typeof(GraphicsDeviceService)) as GraphicsDeviceService;
            var device = graphicsDevice.GrapicsDevice.Device;

            return new HullShader(device, File.ReadAllBytes(uri.AbsolutePath));
        }
    }
}

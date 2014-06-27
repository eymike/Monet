using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Windows;

namespace MonetCore
{
    public class MonetApp
    {
        private MonetServiceProvider m_services;
        public MonetServiceProvider Services { get { return m_services; } }

        private GraphicsDevice m_graphicsDevice;
        private AppComponentCollection m_components;

        public void Run()
        {
            var form = new RenderForm("Monet");
            m_graphicsDevice = new GraphicsDevice(form);
            m_services = new MonetServiceProvider(new GraphicsDeviceService(m_graphicsDevice));

            m_components = (AppComponentCollection)m_services.GetService(typeof(AppComponentCollection));

            LoadContent();

            var timer = (FrameTimer)m_services.GetService(typeof(FrameTimer));
            timer.Start();

            // Main loop
            RenderLoop.Run(form, () =>
            {
                timer.Tick();
                InternalUpdate();
                InternalRender();
            });

            timer.Stop();

            UnloadContent();
        }

        private void InternalUpdate()
        {
            m_components.Update();
            Update();
        }

        private void InternalRender()
        {
            Render();
            m_graphicsDevice.Present();
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {
            m_components.Draw();
        }

        protected virtual void LoadContent()
        {

        }

        protected virtual void UnloadContent()
        {

        }
    }
}

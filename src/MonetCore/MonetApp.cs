﻿using System;
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
        public void Run()
        {
            var form = new RenderForm("Monet");
            m_graphicsDevice = new GraphicsDevice(form);
            m_services = new MonetServiceProvider(new GraphicsDeviceService(m_graphicsDevice));
           
            // Main loop
            RenderLoop.Run(form, () =>
            {
                InternalUpdate();
                InternalRender();
            });
        }

        private void InternalUpdate()
        {
            Update();
        }

        private void InternalRender()
        {
            Render();
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {
            m_graphicsDevice.Clear(Color.Chartreuse);

            m_graphicsDevice.Present();
            
        }
    }
}

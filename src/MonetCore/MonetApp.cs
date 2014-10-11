using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Windows;

using MonetCore.World;

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
            Run(new Scene());
        }

        public void Run(Scene initialScene)
        {
            
            var form = new RenderForm("Monet");
            m_graphicsDevice = new GraphicsDevice(form);
            m_services = new MonetServiceProvider(
                new GraphicsDeviceService(m_graphicsDevice),
                new FormService(form));

            ((Console.Console)m_services.GetService(typeof(Console.Console))).AddCommand(
                "quit",
                "Quits the applications.",
                (string[] args) =>
                {
                    try
                    {
                        form.Invoke(new Action(() => { form.Close(); }));
                    }
                    catch (InvalidOperationException) { }  //This is probably because the form has already closed.
                    return false;
                }
                );


            m_components = (AppComponentCollection)m_services.GetService(typeof(AppComponentCollection));

            var sceneManager = (SceneManager)m_services.GetService(typeof(SceneManager));
            m_components.Add(sceneManager);
            sceneManager.PushScene(initialScene);

            LoadContent();

            var timer = (FrameTimer)m_services.GetService(typeof(FrameTimer));
            timer.Start();

            bool exiting = false;

            ThreadStart gameLogicThreadStart = new ThreadStart( () =>
            {
                while(!exiting)
                {
                    timer.Tick();
                    InternalUpdate();
                }
            });

            ThreadStart renderThreadStart = new ThreadStart( () =>
            {
                while(!exiting)
                {
                    InternalRender();
                }
            });

            Thread logicThread = new Thread(gameLogicThreadStart);
            logicThread.Name = "Game Logic Thread";

            Thread renderThread = new Thread(renderThreadStart);
            renderThread.Name = "Render Thread";

            logicThread.Start();
            renderThread.Start();

            // Main loop
            RenderLoop.Run(form, () =>
            {
                Thread.Sleep(8);
            });

            exiting = true;

            logicThread.Join();
            renderThread.Join();

            timer.Stop();

            UnloadContent();

        }

        private void InternalUpdate()
        {
            m_components.Update();
        }

        private void InternalRender()
        {
            m_components.Draw();
            m_graphicsDevice.Present();
        }

        protected virtual void LoadContent()
        {

        }

        protected virtual void UnloadContent()
        {

        }
    }
}

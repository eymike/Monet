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
        public void Run()
        {
            var form = new RenderForm("Monet");

            // Main loop
            RenderLoop.Run(form, () =>
            {
               // context.ClearRenderTargetView(renderView, Color.Black);
                
                //swapChain.Present(0, PresentFlags.None);
            });
        }
    }
}

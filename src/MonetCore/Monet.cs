using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore
{
    public class Monet
    {
        public static void SafeDispose(IDisposable toDispose)
        {
            if (toDispose == null) { return; }
            toDispose.Dispose();
        }

        public static void LogMsg(string msg)
        {
            System.Console.WriteLine(string.Format("[{0}]: {1}", DateTime.Now.ToString("HH:mm:ss.ffff"), msg));
        }
    }
}

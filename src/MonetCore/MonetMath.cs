using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore
{
    public class MonetMath
    {
        static public float ToRadians(float x)
        {
            return (float)Math.PI * x / 180.0f;
        }
    }
}

using System;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.Direct3D11;

namespace MonetCore.Graphics
{
    [StructLayout(LayoutKind.Explicit)]
    struct CameraParams
    {
        [FieldOffset(0)]
        public Matrix View;

        [FieldOffset(sizeof(float) * 16)]
        public Matrix Proj;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct PerObjectVars
    {
        [FieldOffset(0)]
        public Matrix World;
    }
}
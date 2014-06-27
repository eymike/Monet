using System;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.Direct3D11;

namespace MonetCore.Graphics
{
    [StructLayout(LayoutKind.Explicit)]
    public struct VertexPosition
    {
        [FieldOffset(0)]
        public Vector3 Position;

        public static InputElement[] GetInputElements()
        {
            return new InputElement[]{ new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, 0) };
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct VertexPositionColor
    {
        [FieldOffset(0)]
        public Vector3 Position;

        [FieldOffset(12)]
        public Vector4 Color;

        public VertexPositionColor(Vector3 position, Vector4 color)
        {
            Position = position;
            Color = color;
        }

        public static InputElement[] GetInputElements()
        {
            return new InputElement[] 
            { 
                new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, InputElement.AppendAligned, 0),
                new InputElement("COLOR", 0, SharpDX.DXGI.Format.R32G32B32A32_Float, InputElement.AppendAligned, 0)
            };
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct VertexPositionNormal
    {
        [FieldOffset(0)]
        public Vector3 Position;

        [FieldOffset(12)]
        public Vector3 Normal;

        public static InputElement[] GetInputElements()
        {
            return new InputElement[] 
            { 
                new InputElement("POSITION", 0, SharpDX.DXGI.Format.R32G32B32_Float, InputElement.AppendAligned, 0),
                new InputElement("NORMAL", 0, SharpDX.DXGI.Format.R32G32B32_Float, InputElement.AppendAligned, 0)
            };
        }
    }
}
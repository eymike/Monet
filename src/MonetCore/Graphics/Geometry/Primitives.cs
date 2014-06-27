using System;
using SharpDX;

namespace MonetCore.Graphics.Geometry
{
    public class Cube
    {

        public static Mesh MakeVertexPositionCube(float edgeSize)
        {
            throw new NotImplementedException();
        }

        public static Mesh MakeVertexPositionNormalCube(GraphicsDevice graphics, float edgeSize)
        {
            var factory = new MeshBuilder<VertexPositionNormal>();
            float s = edgeSize * 0.5f;

            // + X
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, -s), Normal = Vector3.Right });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, -s), Normal = Vector3.Right });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, s), Normal = Vector3.Right });

            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, -s), Normal = Vector3.Right });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, s), Normal = Vector3.Right });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, s), Normal = Vector3.Right });

            // - X
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, s), Normal = Vector3.Left });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, s), Normal = Vector3.Left });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, -s), Normal = Vector3.Left });

            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, s), Normal = Vector3.Left });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, -s), Normal = Vector3.Left });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, -s), Normal = Vector3.Left });

            // + Y
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, -s), Normal = Vector3.Up });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, -s), Normal = Vector3.Up });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, s), Normal = Vector3.Up });

            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, -s), Normal = Vector3.Up });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, s), Normal = Vector3.Up });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, -s), Normal = Vector3.Up });

            // - Y
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, s), Normal = Vector3.Down });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, s), Normal = Vector3.Down });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, -s), Normal = Vector3.Down });

            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, s), Normal = Vector3.Down });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, -s), Normal = Vector3.Down });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, -s), Normal = Vector3.Down });

            // + Z
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, s), Normal = Vector3.ForwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, s), Normal = Vector3.ForwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, s), Normal = Vector3.ForwardLH });

            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, s), Normal = Vector3.ForwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, s), Normal = Vector3.ForwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, s), Normal = Vector3.ForwardLH });

            // - Z
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, -s), Normal = Vector3.BackwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, s, -s), Normal = Vector3.BackwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, -s), Normal = Vector3.BackwardLH });

            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(-s, -s, -s), Normal = Vector3.BackwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, s, -s), Normal = Vector3.BackwardLH });
            factory.AddVertex(new VertexPositionNormal { Position = new Vector3(s, -s, -s), Normal = Vector3.BackwardLH });

            return factory.Finalize(graphics);
        }

        public static Mesh MakeVertexPositionNormalUVCube(float edgeSize)
        {
            throw new NotImplementedException();
        }
    }
}
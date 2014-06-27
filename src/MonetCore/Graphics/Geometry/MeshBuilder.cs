using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore.Graphics.Geometry
{
    public class MeshBuilder<VertexType> where VertexType : struct
    {
        private List<VertexType> m_verts = new List<VertexType>();
        private List<int> m_indexes = new List<int>();

        public void AddVertex(VertexType vertex)
        {
            int index = m_verts.IndexOf(vertex);
            if(index > 0)
            {
                m_indexes.Add(index);
            }
            else
            {
                m_verts.Add(vertex);
                m_indexes.Add(m_verts.Count - 1);
            }
        }

        public Mesh Finalize(GraphicsDevice graphics)
        {
            if(m_indexes.Count % 3 != 0)
            {
                throw new InvalidOperationException("Cannot create mesh with invalid triangles.");
            }

            return new Mesh<VertexType>(graphics, m_verts.ToArray(), m_indexes.ToArray());
        }
    }
}

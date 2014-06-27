using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpDX;

namespace MonetCore
{
    public class SceneNode
    {
        private SceneNode m_parent;
        private Transform m_transform = new Transform();

        public Transform Transform{get{return m_transform;}}

        public SceneNode Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        public Matrix LocalTransform
        {
            get { return m_transform.TransformMatrix; }
        }

        public Matrix GlobalTransform
        {
            get
            {
                Matrix result;
                if(m_parent != null)
                {
                    result = LocalTransform * m_parent.GlobalTransform;
                }
                else
                {
                    result = LocalTransform;
                }

                return result;
            }
        }
    }
}

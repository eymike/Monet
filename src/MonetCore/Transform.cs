using System;
using System.Collections.Generic;

using SharpDX;

namespace MonetCore
{
    public class Transform
    {
        private Vector3 m_position = Vector3.Zero;
        private Quaternion m_orientation = Quaternion.Identity;
        private float m_scale = 1;

        public Vector3 Position
        {
            get { return m_position; }
            set { m_position = value; }
        }

        public Quaternion Orientation
        {
            get { return m_orientation; }
            set { m_orientation = value; }
        }

        public float Scaling
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        public Matrix TransformMatrix { get { return Matrix.Scaling(m_scale) * Matrix.RotationQuaternion(m_orientation) * Matrix.Translation(m_position); } }

        public void Translate(Vector3 translation)
        {
            m_position += translation;
        }

        public void Rotate(Quaternion rotation)
        {
            m_orientation = Quaternion.Normalize(m_orientation * rotation);
        }

        public void Scale(float scaling)
        {
            m_scale *= scaling;
        }
    }
}

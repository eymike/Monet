using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore
{
    public interface IDrawable
    {
        void Update();
        int UpdateOrder { get; set; }
        event Action<IDrawable> OnDrawOrderChanged;
    }

    public interface IUpdateable
    {
        void Draw();
        int DrawOrder { get; set; }
        event Action<IUpdateable> OnUpdateOrderChanged;
    }

    public class AppComponent : IDrawable, IUpdateable
    {
        private int m_drawOrder;
        public int DrawOrder
        {
            get { return m_drawOrder; }
            set
            {
                int v = value;
                if (v != m_drawOrder)
                {
                    m_drawOrder = v;
                    if (OnDrawOrderChanged != null)
                    {
                        var onDraw = OnDrawOrderChanged;
                        onDraw(this);
                    }
                }
            }
        }
        public event Action<IDrawable> OnDrawOrderChanged;

        private int m_updateOrder;
        public int UpdateOrder
        {
            get { return m_updateOrder; }
            set
            {
                int v = value;
                if (v != m_updateOrder)
                {
                    m_updateOrder = v;
                    if (OnUpdateOrderChanged != null)
                    {
                        var onUpdate = OnUpdateOrderChanged;
                        onUpdate(this);
                    }
                }
            }
        }
        public event Action<IUpdateable> OnUpdateOrderChanged;
       
        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }

    public class AppComponentCollection
    {
        private class DrawableComparer : IComparer<IDrawable>
        {
            public int Compare(IDrawable x, IDrawable y)
            {
                return x.UpdateOrder - y.UpdateOrder;
            }
        }

        private class UpdateableComparer : IComparer<IUpdateable>
        {
            public int Compare(IUpdateable x, IUpdateable y)
            {
                return x.DrawOrder - y.DrawOrder;
            }
        }

        private SortedSet<IDrawable> m_drawables = new SortedSet<IDrawable>(new DrawableComparer());
        private SortedSet<IUpdateable> m_updateables = new SortedSet<IUpdateable>(new UpdateableComparer());

        public IEnumerable<IDrawable> Drawables { get { return m_drawables; } }
        public IEnumerable<IUpdateable> Updateables { get { return m_updateables; } }

        public void Add(AppComponent component)
        {
            if(m_drawables.Add(component))
            {
                component.OnDrawOrderChanged += Component_OnDrawOrderChanged;
            }

            if(m_updateables.Add(component))
            {
                component.OnUpdateOrderChanged += Component_OnUpdateOrderChanged;
            }
        }

        private void Component_OnDrawOrderChanged(IDrawable component)
        {
            m_drawables.Remove(component);
            m_drawables.Add(component);
        }

        private void Component_OnUpdateOrderChanged(IUpdateable component)
        {
            m_updateables.Remove(component);
            m_updateables.Add(component);
        }

        public void Add(IDrawable component)
        {
            if(m_drawables.Add(component))
            {
                component.OnDrawOrderChanged += Component_OnDrawOrderChanged;
            }
        }

        public void Add(IUpdateable component)
        {
            if(m_updateables.Add(component))
            {
                component.OnUpdateOrderChanged += Component_OnUpdateOrderChanged;
            }
        }

        public void Remove(AppComponent component)
        {
            if(m_drawables.Remove(component))
            {
                component.OnDrawOrderChanged -= Component_OnDrawOrderChanged;
            }

            if(m_updateables.Remove(component))
            {
                component.OnUpdateOrderChanged -= Component_OnUpdateOrderChanged;
            }
        }

        public void Remove(IDrawable component)
        {
            if (m_drawables.Remove(component))
            {
                component.OnDrawOrderChanged -= Component_OnDrawOrderChanged;
            }
        }

        public void Remove(IUpdateable component)
        {
            if (m_updateables.Remove(component))
            {
                component.OnUpdateOrderChanged -= Component_OnUpdateOrderChanged;
            }
        }
    }
}

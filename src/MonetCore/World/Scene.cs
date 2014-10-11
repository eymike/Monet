using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonetCore.World
{
    public class Scene
    {
        public bool BlocksDraw { get; set; }
        public bool BlocksUpdate { get; set; }

        private EntityManager _entities = new EntityManager();

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }

    [ServiceAtribute(Name = "SceneManager")]
    public class SceneManager : AppComponent
    {
        private readonly MonetServiceProvider _services;
        private LinkedList<Scene> _sceneStack = new LinkedList<Scene>();

        public SceneManager(MonetServiceProvider services)
            : base(services)
        {
            _services = services;
        }

        public void PushScene(Scene scene)
        {
            _sceneStack.AddLast(scene);
        }

        public Scene PopScene()
        {
            var scene = _sceneStack.Last.Value;
            _sceneStack.RemoveLast();
            return scene;
        }

        public override void Update()
        {
            base.Update();
            var current = _sceneStack.Last;
            while(current != null)
            {
                current.Value.Update();
                current = current.Value.BlocksUpdate ? null : current.Previous;
            }
        }

        public override void Draw()
        {
            base.Draw();
            var current = _sceneStack.Last;
            while (current != null)
            {
                current.Value.Draw();
                current = current.Value.BlocksDraw ? null : current.Previous;
            }
        }
    }
}

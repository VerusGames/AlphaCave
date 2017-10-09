using System.Collections.Generic;
using System.Linq;

namespace AlphaCave.GameObjects.Components
{
    public class EntityComponentCollection : List<IEntityComponent>
    {
        public new void Add(IEntityComponent component)
        {
            base.Add(component);
        }

        public T Add<T>()
            where T : IEntityComponent, new()
        {
            var component = new T();
            Add(component);
            return component;
        }
        
        public IEnumerable<T> GetComponent<T>() 
            where T : IEntityComponent
        {
            return this.OfType<T>();
        }
    }
}
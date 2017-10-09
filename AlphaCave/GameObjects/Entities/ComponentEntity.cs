using System.Collections.Generic;
using System.Linq;
using AlphaCave.GameObjects.Components;
using EngeniousUI;

namespace AlphaCave.GameObjects.Entities
{
    public abstract class ComponentEntity
    {
        public EntityComponentCollection Components { get; private set; } = new EntityComponentCollection();

        public IEnumerable<T> GetComponent<T>() where T : IEntityComponent 
            => Components.GetComponent<T>();

    }
}
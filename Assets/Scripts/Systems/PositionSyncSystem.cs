using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class PositionSyncSystem: IEcsRunSystem 
    {
        private readonly EcsFilter<PositionComponent, UnityComponent> _filter = null;

        public void Run ()
        {
            foreach (var i in _filter) 
            {
                ref var position = ref _filter.Get1(i);
                ref var unity = ref _filter.Get2(i);

                unity.GameObject.transform.position = position.Position;
            }
        }
    }
}
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerInputSystem: IEcsRunSystem 
    {
        private readonly EcsFilter<PlayerComponent, PositionComponent, MoveSpeedComponent> _filter = null;
        
        public void Run () 
        {
            foreach (var i in _filter)
            {
                ref var position = ref _filter.Get2(i);
                ref var speed = ref _filter.Get3(i);

                Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                position.Position += input * speed.Speed * Time.deltaTime;
            }
        }
    }
}
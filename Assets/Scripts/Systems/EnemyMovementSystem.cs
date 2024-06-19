using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, PositionComponent, MoveSpeedComponent> _enemyFilter = null;
        private readonly EcsFilter<PlayerComponent, PositionComponent> _playerFilter = null;

        public void Run()
        {
            if (_playerFilter.IsEmpty()) return;

            ref var playerPosition = ref _playerFilter.Get2(0);

            foreach (var i in _enemyFilter)
            {
                ref var enemyPosition = ref _enemyFilter.Get2(i);
                ref var speed = ref _enemyFilter.Get3(i);

                var direction = (playerPosition.Position - enemyPosition.Position).normalized;
                enemyPosition.Position += direction * speed.Speed * Time.deltaTime;
            }
        }
    }
}
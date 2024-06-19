using Components;
using Leopotam.Ecs;
using UnityEngine.SceneManagement;

namespace Systems
{
    public class CollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, PositionComponent> _enemyFilter = null;
        private readonly EcsFilter<PlayerComponent, PositionComponent> _playerFilter = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerPosition = ref _playerFilter.Get2(i);

                foreach (var j in _enemyFilter)
                {
                    ref var enemyPosition = ref _enemyFilter.Get2(j);

                    if ((playerPosition.Position - enemyPosition.Position).sqrMagnitude < 0.5f) RestartGame();
                }
            }
        }

        private void RestartGame() => SceneManager.LoadScene(0);
    }
}
using Components;
using ConfigsSO;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, PositionComponent> _playerFilter = null;

        private readonly EcsWorld _world = null;
        private readonly EnemySettingsSO _settings;

        private float _timer;

        public EnemySpawnSystem(EnemySettingsSO settings)
        {
            _settings = settings;
        }

        public void Init()
        {
            _timer = _settings.spawnInterval;
        }

        public void Run()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = _settings.spawnInterval;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            foreach (var i in _playerFilter)
            {
                ref var playerPosition = ref _playerFilter.Get2(i);
                var spawnPosition = playerPosition.Position + Random.insideUnitCircle.normalized *
                    Random.Range(_settings.minSpawnDistance, _settings.maxSpawnDistance);

                var enemyEntity = _world.NewEntity();
                enemyEntity.Replace(new EnemyComponent());
                enemyEntity.Replace(new MoveSpeedComponent {Speed = _settings.enemySpeed});
                enemyEntity.Replace(new PositionComponent {Position = spawnPosition});

                var enemyGameObject = Object.Instantiate(_settings.enemyPrefab, spawnPosition, Quaternion.identity);
                enemyEntity.Replace(new UnityComponent {GameObject = enemyGameObject});
            }
        }
    }
}
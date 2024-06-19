using Systems;
using Components;
using ConfigsSO;
using Leopotam.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour
{
    public GameObject playerPrefab;
    public PlayerSettingsSO playerSettings;
    public EnemySettingsSO enemySettings;
    private EcsSystems _systems;
    private EcsWorld _world;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems
            .Add(new PlayerInputSystem())
            .Add(new PositionSyncSystem())
            .Add(new EnemySpawnSystem(enemySettings))
            .Add(new EnemyMovementSystem())
            .Add(new CollisionSystem())
            .Init();

        var playerEntity = _world.NewEntity();
        playerEntity.Replace(new PlayerComponent());

        var playerSpeed =  playerSettings.speed;

        playerEntity.Replace(new MoveSpeedComponent {Speed = playerSpeed});
        playerEntity.Replace(new PositionComponent {Position = Vector2.zero});

        var playerGameObject = Instantiate(playerPrefab);
        playerEntity.Replace(new UnityComponent {GameObject = playerGameObject});
    }

    private void Update()
    {
        _systems.Run();
    }

    private void OnDestroy()
    {
        _systems.Destroy();
        _world.Destroy();
    }
}
using UnityEngine;

namespace ConfigsSO
{
    [CreateAssetMenu()]
    public class EnemySettingsSO: ScriptableObject
    {
        public GameObject enemyPrefab;
        public float spawnInterval;
        public float minSpawnDistance;
        public float maxSpawnDistance;
        public float enemySpeed;
    }
}
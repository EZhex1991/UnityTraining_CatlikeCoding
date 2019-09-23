/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-26 15:30:25
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [CreateAssetMenu(menuName = "CatlikeCoding/TowerDefense/GameTileContentFactory")]
    public class GameTileContentFactory : GameObjectFactory
    {
        [SerializeField]
        private GameTileContent destinationPrefab = default;
        [SerializeField]
        private GameTileContent emptyPrefab = default;
        [SerializeField]
        private GameTileContent wallPrefab = default;
        [SerializeField]
        private GameTileContent spawnPointPrefab = default;
        [SerializeField]
        private Tower[] towerPrefabs = default;

        public void Reclaim(GameTileContent content)
        {
            Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
            Destroy(content.gameObject);
        }

        public GameTileContent Get(GameTileContentType type)
        {
            switch (type)
            {
                case GameTileContentType.Destination: return Get(destinationPrefab);
                case GameTileContentType.Empty: return Get(emptyPrefab);
                case GameTileContentType.Wall: return Get(wallPrefab);
                case GameTileContentType.SpawnPoint: return Get(spawnPointPrefab);
            }
            Debug.Assert(false, "Unsupported non-tower type: " + type);
            return null;
        }
        public Tower Get(TowerType type)
        {
            Debug.Assert((int)type < towerPrefabs.Length, "Unsupported tower type!");
            Tower prefab = towerPrefabs[(int)type];
            Debug.Assert(type == prefab.TowerType, "Tower prefab at wrong index!");
            return Get(prefab);
        }
        private T Get<T>(T prefab) where T : GameTileContent
        {
            T instance = CreateGameObjectInstance(prefab);
            instance.OriginFactory = this;
            return instance;
        }
    }
}

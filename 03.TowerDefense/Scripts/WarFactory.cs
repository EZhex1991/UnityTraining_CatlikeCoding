/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-12 15:19:35
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [CreateAssetMenu(menuName = "CatlikeCoding/TowerDefense/WarFactory")]
    public class WarFactory : GameObjectFactory
    {
        [SerializeField]
        private Explosion explosionPrefab = default;
        [SerializeField]
        private Shell shellPrefab = default;

        public Explosion Explosion => Get(explosionPrefab);
        public Shell Shell => Get(shellPrefab);

        private T Get<T>(T prefab) where T : WarEntity
        {
            T instance = CreateGameObjectInstance(prefab);
            instance.OriginFactory = this;
            return instance;
        }

        public void Reclaim(WarEntity entity)
        {
            Debug.Assert(entity.OriginFactory == this, "Wrong factory reclaimed!");
            Destroy(entity.gameObject);
        }
    }
}

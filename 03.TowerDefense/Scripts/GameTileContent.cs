/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-26 10:37:12
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public enum GameTileContentType { Empty, Destination, Wall, SpawnPoint, Tower, }

    [SelectionBase]
    public class GameTileContent : MonoBehaviour
    {
        [SerializeField]
        private GameTileContentType type = default;
        public GameTileContentType Type => type;

        public bool BlocksPath => Type == GameTileContentType.Wall || Type == GameTileContentType.Tower;

        GameTileContentFactory originFactory;
        public GameTileContentFactory OriginFactory
        {
            get => originFactory;
            set
            {
                Debug.Assert(originFactory == null, "Redefined origin factory!");
                originFactory = value;
            }
        }

        public virtual void GameUpdate() { }

        public void Recycle()
        {
            originFactory.Reclaim(this);
        }
    }
}

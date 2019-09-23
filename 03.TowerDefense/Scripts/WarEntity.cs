/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-12 15:15:46
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public abstract class WarEntity : GameBehaviour
    {
        private WarFactory originFactory;

        public WarFactory OriginFactory
        {
            get => originFactory;
            set
            {
                Debug.Assert(originFactory == null, "Redefined origin factory!");
                originFactory = value;
            }
        }

        public override void Recycle()
        {
            originFactory.Reclaim(this);
        }
    }
}

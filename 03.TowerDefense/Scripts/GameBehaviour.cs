/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-19 18:54:39
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        public virtual bool GameUpdate() => true;

        public abstract void Recycle();
    }
}

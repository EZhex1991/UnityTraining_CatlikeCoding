/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-28 16:19:13
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    [System.Serializable]
    public struct FloatRange
    {
        public float min, max;

        public float RandomValueInRange
        {
            get
            {
                return Random.Range(min, max);
            }
        }

        public FloatRange(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}

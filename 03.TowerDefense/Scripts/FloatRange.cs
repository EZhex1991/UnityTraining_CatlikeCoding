/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-28 16:19:13
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [System.Serializable]
    public struct FloatRange
    {
        [SerializeField]
        private float min, max;

        public float Min => min;
        public float Max => max;

        public float RandomValueInRange
        {
            get
            {
                return Random.Range(min, max);
            }
        }

        public FloatRange(float value)
        {
            min = max = value;
        }
    }
}

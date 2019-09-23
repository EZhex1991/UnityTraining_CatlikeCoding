/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-28 17:22:20
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class FloatRangeSliderAttribute : PropertyAttribute
    {
        public float Min { get; private set; }
        public float Max { get; private set; }

        public FloatRangeSliderAttribute(float min, float max)
        {
            if (max < min)
            {
                max = min;
            }
            Min = min;
            Max = max;
        }
    }
}

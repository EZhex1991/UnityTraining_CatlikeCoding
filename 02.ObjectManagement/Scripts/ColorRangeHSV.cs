/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-28 17:17:41
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    [System.Serializable]
    public struct ColorRangeHSV
    {
        [FloatRangeSlider(0f, 1f)]
        public FloatRange hue, saturation, value;

        public Color RandomInRange
        {
            get
            {
                return Random.ColorHSV(
                    hue.min, hue.max,
                    saturation.min, saturation.max,
                    value.min, value.max,
                    1f, 1f
                );
            }
        }

        public ColorRangeHSV(float minHue, float maxHue, float minSat, float maxSat, float minValue, float maxValue)
        {
            hue = new FloatRange(minHue, maxHue);
            saturation = new FloatRange(minSat, maxSat);
            value = new FloatRange(minValue, maxValue);
        }
    }
}

/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-28 17:25:07
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEditor;
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [CustomPropertyDrawer(typeof(FloatRangeSliderAttribute))]
    public class FloatRangeSliderDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            int originalIndentLevel = EditorGUI.indentLevel;

            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.indentLevel = 0;
            SerializedProperty minProperty = property.FindPropertyRelative("min");
            SerializedProperty maxProperty = property.FindPropertyRelative("max");
            float minValue = minProperty.floatValue;
            float maxValue = maxProperty.floatValue;
            float fieldWidth = position.width / 4f - 4f;
            float sliderWidth = position.width / 2f;
            position.width = fieldWidth;
            minValue = EditorGUI.FloatField(position, minValue);
            position.x += position.width + 4f;
            position.width = sliderWidth;
            FloatRangeSliderAttribute limit = attribute as FloatRangeSliderAttribute;
            EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, limit.Min, limit.Max);
            position.x += sliderWidth + 4f;
            position.width = fieldWidth;
            maxValue = EditorGUI.FloatField(position, maxValue);
            if (minValue < limit.Min)
            {
                minValue = limit.Min;
            }
            if (maxValue < minValue)
            {
                maxValue = minValue;
            }
            else if (maxValue > limit.Max)
            {
                maxValue = limit.Max;
            }
            minProperty.floatValue = minValue;
            maxProperty.floatValue = maxValue;
            EditorGUI.EndProperty();

            EditorGUI.indentLevel = originalIndentLevel;
        }
    }
}

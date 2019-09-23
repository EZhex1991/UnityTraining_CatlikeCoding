/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-10 16:55:12
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
    }

    public enum DirectionChange
    {
        None,
        TurnRight,
        TurnLeft,
        TurnAround
    }

    public static class DirectionExtensions
    {
        private static Quaternion[] rotations =
        {
            Quaternion.identity,
            Quaternion.Euler(0f, 90f, 0f),
            Quaternion.Euler(0f, 180f, 0f),
            Quaternion.Euler(0f, 270f, 0f)
        };
        private static Vector3[] halfVectors =
        {
            Vector3.forward * 0.5f,
            Vector3.right * 0.5f,
            Vector3.back * 0.5f,
            Vector3.left * 0.5f,
        };

        public static Quaternion GetRotation(this Direction direction)
        {
            return rotations[(int)direction];
        }
        public static float GetAngle(this Direction direction)
        {
            return (float)direction * 90;
        }
        public static DirectionChange GetDirectionChangeTo(this Direction current, Direction next)
        {
            if (current == next)
            {
                return DirectionChange.None;
            }
            else if (current + 1 == next || current - 3 == next)
            {
                return DirectionChange.TurnRight;
            }
            else if (current - 1 == next || current + 3 == next)
            {
                return DirectionChange.TurnLeft;
            }
            return DirectionChange.TurnAround;
        }
        public static Vector3 GetHalfVector(this Direction direction)
        {
            return halfVectors[(int)direction];
        }
    }
}

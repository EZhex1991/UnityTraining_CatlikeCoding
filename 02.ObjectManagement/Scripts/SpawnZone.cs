/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 19:29:58
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public abstract class SpawnZone : PersistableObject
    {
        [System.Serializable]
        public struct SpawnConfiguration
        {
            public enum MovementDirection
            {
                Forward,
                Upward,
                Outward,
                Random,
            }

            public MovementDirection movementDirection;
            public FloatRange speed;
            public FloatRange angularSpeed;
            public FloatRange scale;
            public ColorRangeHSV color;

            public static SpawnConfiguration defaultConfig
            {
                get
                {
                    return new SpawnConfiguration()
                    {
                        movementDirection = MovementDirection.Forward,
                        speed = new FloatRange(1.5f, 2.5f),
                        angularSpeed = new FloatRange(0f, 90f),
                        scale = new FloatRange(0.1f, 1f),
                        color = new ColorRangeHSV(0f, 1f, 0.5f, 1f, 0.25f, 1f)
                    };
                }
            }
        }

        [SerializeField]
        private SpawnConfiguration spawnConfig = SpawnConfiguration.defaultConfig;

        public abstract Vector3 SpawnPoint { get; }

        public virtual void ConfigureSpawn(Shape shape)
        {
            Transform t = shape.transform;
            t.localPosition = SpawnPoint;
            t.localRotation = Random.rotation;
            t.localScale = Vector3.one * spawnConfig.scale.RandomValueInRange;
            shape.SetColor(spawnConfig.color.RandomInRange);
            shape.AngularVelocity = Random.onUnitSphere * spawnConfig.angularSpeed.RandomValueInRange;
            Vector3 direction;
            switch (spawnConfig.movementDirection)
            {
                case SpawnConfiguration.MovementDirection.Upward:
                    direction = transform.up;
                    break;
                case SpawnConfiguration.MovementDirection.Outward:
                    direction = (t.localPosition - transform.position).normalized;
                    break;
                case SpawnConfiguration.MovementDirection.Random:
                    direction = Random.onUnitSphere;
                    break;
                default:
                    direction = transform.forward;
                    break;
            }
            shape.Velocity = direction * spawnConfig.speed.RandomValueInRange;
        }
    }
}

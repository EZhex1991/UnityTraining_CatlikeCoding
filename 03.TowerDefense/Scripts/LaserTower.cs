/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-11 16:17:19
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public class LaserTower : Tower
    {
        [SerializeField, Range(1f, 100f)]
        float damagePerSecond = 10f;
        [SerializeField]
        private Transform turret = default;
        [SerializeField]
        private Transform laserBeam = default;

        private TargetPoint target;
        private Vector3 laserBeamScale;

        public override TowerType TowerType => TowerType.Laser;

        private void Awake()
        {
            laserBeamScale = laserBeam.localScale;
        }

        public override void GameUpdate()
        {
            if (TrackTarget(ref target) || AcquireTarget(out target))
            {
                Shoot();
            }
            else
            {
                laserBeam.localScale = Vector3.zero;
            }
        }

        private void Shoot()
        {
            Vector3 point = target.Position;
            turret.LookAt(point);
            laserBeam.localRotation = turret.localRotation;

            float d = Vector3.Distance(turret.position, point);
            laserBeamScale.z = d;
            laserBeam.localScale = laserBeamScale;
            laserBeam.localPosition = turret.localPosition + 0.5f * d * laserBeam.forward;

            target.Enemy.ApplyDamage(damagePerSecond * Time.deltaTime);
        }
    }
}

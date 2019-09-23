/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-12 15:19:12
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public class Shell : WarEntity
    {
        private Vector3 launchPoint, targetPoint, launchVelocity;
        private float age, blastRadius, damage;

        public void Initialze(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVeloctiy,
            float blastRadius, float damage
        )
        {
            this.launchPoint = launchPoint;
            this.targetPoint = targetPoint;
            this.launchVelocity = launchVeloctiy;
            this.blastRadius = blastRadius;
            this.damage = damage;
        }

        public override bool GameUpdate()
        {
            age += Time.deltaTime;
            Vector3 p = launchPoint + launchVelocity * age;
            p.y -= 0.5f * 9.81f * age * age;
            if (p.y <= 0f)
            {
                Game.SpawnExplosion().Initialize(targetPoint, blastRadius, damage);
                OriginFactory.Reclaim(this);
                return false;
            }
            transform.localPosition = p;

            Vector3 d = launchVelocity;
            d.y -= 9.81f * age;
            transform.localRotation = Quaternion.LookRotation(d);

            Game.SpawnExplosion().Initialize(p, 0.1f);
            return true;
        }
    }
}

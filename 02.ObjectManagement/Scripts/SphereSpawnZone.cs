/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 20:27:13
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class SphereSpawnZone : SpawnZone
    {
        [SerializeField]
        private bool surfaceOnly;

        public override Vector3 SpawnPoint
        {
            get
            {
                return transform.TransformPoint(
                    surfaceOnly ? Random.onUnitSphere : Random.insideUnitSphere
                );
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, 1f);
        }
    }
}

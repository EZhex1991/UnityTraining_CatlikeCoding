/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 20:51:12
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class CubeSpawnZone : SpawnZone
    {
        [SerializeField]
        private bool surfaceOnly;

        public override Vector3 SpawnPoint
        {
            get
            {
                Vector3 p;
                p.x = Random.Range(-0.5f, 0.5f);
                p.y = Random.Range(-0.5f, 0.5f);
                p.z = Random.Range(-0.5f, 0.5f);
                if (surfaceOnly)
                {
                    int axis = Random.Range(0, 3);
                    p[axis] = p[axis] < 0f ? -0.5f : 0.5f;
                }
                return transform.TransformPoint(p);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
}

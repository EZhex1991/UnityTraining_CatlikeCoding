/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-28 13:38:08
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class RotatingObject : PersistableObject
    {
        [SerializeField]
        private Vector3 angularVelocity;

        private void FixedUpdate()
        {
            transform.Rotate(angularVelocity * Time.deltaTime);
        }
    }
}

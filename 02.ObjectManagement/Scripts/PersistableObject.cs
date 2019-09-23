/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 10:51:52
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    [DisallowMultipleComponent]
    public class PersistableObject : MonoBehaviour
    {
        public virtual void Save(GameDataWriter writer)
        {
            writer.Write(transform.localPosition);
            writer.Write(transform.localRotation);
            writer.Write(transform.localScale);
        }

        public virtual void Load(GameDataReader reader)
        {
            transform.localPosition = reader.ReadVector3();
            transform.localRotation = reader.ReadQuaternion();
            transform.localScale = reader.ReadVector3();
        }
    }
}

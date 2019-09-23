/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 10:47:21
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.IO;
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class GameDataReader : MonoBehaviour
    {
        private BinaryReader reader;
        public int Version { get; }

        public GameDataReader(BinaryReader reader, int version)
        {
            this.reader = reader;
            this.Version = version;
        }

        public float ReadFloat()
        {
            return reader.ReadSingle();
        }
        public int ReadInt()
        {
            return reader.ReadInt32();
        }
        public Quaternion ReadQuaternion()
        {
            Quaternion value;
            value.x = reader.ReadSingle();
            value.y = reader.ReadSingle();
            value.z = reader.ReadSingle();
            value.w = reader.ReadSingle();
            return value;
        }
        public Vector3 ReadVector3()
        {
            Vector3 value;
            value.x = reader.ReadSingle();
            value.y = reader.ReadSingle();
            value.z = reader.ReadSingle();
            return value;
        }
        public Color ReadColor()
        {
            Color value;
            value.r = reader.ReadSingle();
            value.g = reader.ReadSingle();
            value.b = reader.ReadSingle();
            value.a = reader.ReadSingle();
            return value;
        }
        public Random.State ReadRandomState()
        {
            return JsonUtility.FromJson<Random.State>(reader.ReadString());
        }
    }
}

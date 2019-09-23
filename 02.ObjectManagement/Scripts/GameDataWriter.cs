/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 10:32:28
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.IO;
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class GameDataWriter
    {
        private BinaryWriter writer;

        public GameDataWriter(BinaryWriter writer)
        {
            this.writer = writer;
        }

        public void Write(float value)
        {
            writer.Write(value);
        }
        public void Write(int value)
        {
            writer.Write(value);
        }
        public void Write(Quaternion value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
            writer.Write(value.w);
        }
        public void Write(Vector3 value)
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
        }
        public void Write(Color value)
        {
            writer.Write(value.r);
            writer.Write(value.g);
            writer.Write(value.b);
            writer.Write(value.a);
        }
        public void Write(Random.State value)
        {
            writer.Write(JsonUtility.ToJson(value));
        }
    }
}

/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 10:54:44
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.IO;
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class PersistentStorage : MonoBehaviour
    {
        private string savePath;

        private void Awake()
        {
            savePath = Path.Combine(Application.persistentDataPath, "saveFile");
        }

        public void Save(PersistableObject o, int version)
        {
            using (var writer = new BinaryWriter(File.Open(savePath, FileMode.Create)))
            {
                writer.Write(-version);
                o.Save(new GameDataWriter(writer));
            }
        }
        public void Load(PersistableObject o)
        {
            byte[] data = File.ReadAllBytes(savePath);
            var reader = new BinaryReader(new MemoryStream(data));
            o.Load(new GameDataReader(reader, -reader.ReadInt32()));
        }
    }
}

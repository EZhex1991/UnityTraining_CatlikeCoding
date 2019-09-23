/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 20:18:39
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class GameLevel : PersistableObject
    {
        public static GameLevel Current { get; private set; }

        [SerializeField]
        private SpawnZone spawnZone;
        [SerializeField]
        private PersistableObject[] persistentObjects;

        private void OnEnable()
        {
            Current = this;
            if (persistentObjects == null)
            {
                persistentObjects = new PersistableObject[0];
            }
        }

        public void ConfigureSpawn(Shape shape)
        {
            spawnZone.ConfigureSpawn(shape);
        }

        public override void Save(GameDataWriter writer)
        {
            writer.Write(persistentObjects.Length);
            for (int i = 0; i < persistentObjects.Length; i++)
            {
                persistentObjects[i].Save(writer);
            }
        }
        public override void Load(GameDataReader reader)
        {
            int savedCount = reader.ReadInt();
            for (int i = 0; i < savedCount; i++)
            {
                persistentObjects[i].Load(reader);
            }
        }
    }
}

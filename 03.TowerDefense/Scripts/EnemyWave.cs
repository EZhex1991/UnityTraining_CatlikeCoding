/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-19 20:36:42
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [CreateAssetMenu(menuName = "CatlikeCoding/TowerDefense/EnemyWave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField]
        private EnemySpawnSequence[] spawnSequence =
        {
            new EnemySpawnSequence()
        };

        public State Begin() => new State(this);

        [System.Serializable]
        public struct State
        {
            private EnemyWave wave;
            private int index;
            private EnemySpawnSequence.State sequence;

            public State(EnemyWave wave)
            {
                this.wave = wave;
                index = 0;
                Debug.Assert(wave.spawnSequence.Length > 0, "Empty wave!");
                sequence = wave.spawnSequence[0].Begin();
            }

            public float Progress(float deltaTime)
            {
                deltaTime = sequence.Progress(deltaTime);
                while (deltaTime >= 0f)
                {
                    if (++index >= wave.spawnSequence.Length)
                    {
                        return deltaTime;
                    }
                    sequence = wave.spawnSequence[index].Begin();
                    deltaTime = sequence.Progress(deltaTime);
                }
                return -1f;
            }
        }
    }
}

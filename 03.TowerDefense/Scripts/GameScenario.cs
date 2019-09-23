/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-19 21:02:12
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [CreateAssetMenu(menuName = "CatlikeCoding/TowerDefense/GameScenario")]
    public class GameScenario : ScriptableObject
    {
        [SerializeField, Range(0, 10)]
        private int cycles = 1;
        [SerializeField, Range(0f, 1f)]
        private float cycleSpeedUp = 0.5f;

        [SerializeField]
        private EnemyWave[] waves = { };

        public State Begin() => new State(this);

        [System.Serializable]
        public struct State
        {
            private GameScenario scenario;
            private int cycle, index;
            private float timeScale;
            private EnemyWave.State wave;

            public State(GameScenario scenario)
            {
                this.scenario = scenario;
                cycle = 0;
                index = 0;
                timeScale = 1f;
                Debug.Assert(scenario.waves.Length > 0, "Empty scenario!");
                wave = scenario.waves[0].Begin();
            }

            public bool Progress()
            {
                float deltaTime = wave.Progress(timeScale * Time.deltaTime);
                while (deltaTime >= 0f)
                {
                    if (++index >= scenario.waves.Length)
                    {
                        if (++cycle >= scenario.cycles && scenario.cycles > 0)
                        {
                            return false;
                        }
                        index = 0;
                        timeScale += scenario.cycleSpeedUp;
                    }
                    wave = scenario.waves[index].Begin();
                    deltaTime = wave.Progress(deltaTime);
                }
                return true;
            }
        }
    }
}

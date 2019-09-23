/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-10 16:30:40
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.Collections.Generic;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    [System.Serializable]
    public class GameBehaviourCollection
    {
        private List<GameBehaviour> behaviours = new List<GameBehaviour>();

        public bool IsEmpty => behaviours.Count == 0;

        public void Add(GameBehaviour behaviour)
        {
            behaviours.Add(behaviour);
        }
        public void Clear()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].Recycle();
            }
            behaviours.Clear();
        }

        public void GameUpdate()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (!behaviours[i].GameUpdate())
                {
                    int lastIndex = behaviours.Count - 1;
                    behaviours[i] = behaviours[lastIndex];
                    behaviours.RemoveAt(lastIndex);
                    i -= 1;
                }
            }
        }
    }
}

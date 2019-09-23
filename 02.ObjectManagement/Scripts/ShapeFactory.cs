/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 11:24:40
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    [CreateAssetMenu(menuName = "CatlikeCoding/ObjectManagement/ShapeFactory")]
    public class ShapeFactory : ScriptableObject
    {
        [SerializeField]
        private Shape[] prefabs;

        [SerializeField]
        private Material[] materials;

        [SerializeField]
        private bool recycle;

        private List<Shape>[] pools;
        private Scene poolScene;

        public Shape Get(int shapeId = 0, int materialId = 0)
        {
            Shape instance;
            if (recycle)
            {
                if (pools == null)
                {
                    CreatePools();
                }
                List<Shape> pool = pools[shapeId];
                int lastIndex = pool.Count - 1;
                if (lastIndex >= 0)
                {
                    instance = pool[lastIndex];
                    pool.RemoveAt(lastIndex);
                }
                else
                {
                    instance = Instantiate(prefabs[shapeId]);
                    instance.ShapeId = shapeId;
                    SceneManager.MoveGameObjectToScene(instance.gameObject, poolScene);
                }
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = Instantiate(prefabs[shapeId]);
                instance.ShapeId = shapeId;
            }
            instance.SetMaterial(materials[materialId], materialId);
            return instance;
        }
        public Shape GetRandom()
        {
            return Get(
                Random.Range(0, prefabs.Length),
                Random.Range(0, materials.Length)
            );
        }

        public void Reclaim(Shape shapeToRecycle)
        {
            if (recycle)
            {
                if (pools == null)
                {
                    CreatePools();
                }
                pools[shapeToRecycle.ShapeId].Add(shapeToRecycle);
                shapeToRecycle.gameObject.SetActive(false);
            }
            else
            {
                Destroy(shapeToRecycle.gameObject);
            }
        }

        private void CreatePools()
        {
            pools = new List<Shape>[prefabs.Length];
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i] = new List<Shape>();
            }
            if (Application.isEditor)
            {
                poolScene = SceneManager.GetSceneByName(name);
                if (poolScene.isLoaded)
                {
                    GameObject[] rootObjects = poolScene.GetRootGameObjects();
                    for (int i = 0; i < rootObjects.Length; i++)
                    {
                        Shape pooledShape = rootObjects[i].GetComponent<Shape>();
                        if (!pooledShape.gameObject.activeSelf)
                        {
                            pools[pooledShape.ShapeId].Add(pooledShape);
                        }
                    }
                    return;
                }
            }
            poolScene = SceneManager.CreateScene(name);
        }
    }
}

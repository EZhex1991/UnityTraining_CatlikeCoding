/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-26 19:21:10
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    [DisallowMultipleComponent]
    public class Game : PersistableObject
    {
        private const int saveVersion = 4;

        [SerializeField]
        private ShapeFactory shapeFactory;
        [SerializeField]
        private PersistentStorage storage;

        [SerializeField]
        private KeyCode createKey = KeyCode.C;
        [SerializeField]
        private KeyCode destroyKey = KeyCode.X;
        [SerializeField]
        private KeyCode newGameKey = KeyCode.N;
        [SerializeField]
        private KeyCode saveKey = KeyCode.S;
        [SerializeField]
        private KeyCode loadKey = KeyCode.L;

        public int levelCount;
        [SerializeField]
        private bool reseedOnLoad;

        [SerializeField]
        private Slider creationSpeedSlider;
        [SerializeField]
        private Slider destructionSpeedSlider;

        public float CreationSpeed { get; set; }
        public float DestructionSpeed { get; set; }

        private List<Shape> shapes;
        private float creationProgress, destructionProgress;
        private int loadedLevelBuildIndex;
        private Random.State mainRandomState;

        private void Start()
        {
            mainRandomState = Random.state;
            shapes = new List<Shape>();
            if (Application.isEditor)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene loadedScene = SceneManager.GetSceneAt(i);
                    if (loadedScene.name.Contains("Level "))
                    {
                        SceneManager.SetActiveScene(loadedScene);
                        loadedLevelBuildIndex = loadedScene.buildIndex;
                        return;
                    }
                }
            }
            BeginNewGame();
            StartCoroutine(LoadLevel(1));
        }
        private void Update()
        {
            if (Input.GetKeyDown(createKey))
            {
                CreateShape();
            }
            else if (Input.GetKeyDown(destroyKey))
            {
                DestroyShape();
            }
            else if (Input.GetKey(newGameKey))
            {
                BeginNewGame();
                StartCoroutine(LoadLevel(loadedLevelBuildIndex));
            }
            else if (Input.GetKeyDown(saveKey))
            {
                storage.Save(this, saveVersion);
            }
            else if (Input.GetKeyDown(loadKey))
            {
                BeginNewGame();
                storage.Load(this);
            }
            else
            {
                for (int i = 1; i <= levelCount; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                    {
                        BeginNewGame();
                        StartCoroutine(LoadLevel(i));
                        return;
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].GameUpdate();
            }

            creationProgress += Time.deltaTime * CreationSpeed;
            while (creationProgress >= 1f)
            {
                creationProgress -= 1f;
                CreateShape();
            }

            destructionProgress += Time.deltaTime * DestructionSpeed;
            while (destructionProgress >= 1f)
            {
                destructionProgress -= 1f;
                DestroyShape();
            }
        }

        private void CreateShape()
        {
            Shape instance = shapeFactory.GetRandom();
            GameLevel.Current.ConfigureSpawn(instance);
            shapes.Add(instance);
        }
        private void DestroyShape()
        {
            if (shapes.Count > 0)
            {
                int index = Random.Range(0, shapes.Count);
                shapeFactory.Reclaim(shapes[index]);
                int lastIndex = shapes.Count - 1;
                shapes[index] = shapes[lastIndex];
                shapes.RemoveAt(lastIndex);
            }
        }
        private void BeginNewGame()
        {
            Random.state = mainRandomState;
            int seed = Random.Range(0, int.MaxValue) ^ (int)Time.unscaledTime;
            mainRandomState = Random.state;
            Random.InitState(seed);
            creationSpeedSlider.value = CreationSpeed = 0;
            destructionSpeedSlider.value = DestructionSpeed = 0;
            for (int i = 0; i < shapes.Count; i++)
            {
                shapeFactory.Reclaim(shapes[i]);
            }
            shapes.Clear();
        }

        private IEnumerator LoadLevel(int levelBuildIndex)
        {
            enabled = false;
            if (loadedLevelBuildIndex > 0)
            {
                yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
            }
            yield return SceneManager.LoadSceneAsync(levelBuildIndex, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelBuildIndex));
            loadedLevelBuildIndex = levelBuildIndex;
            enabled = true;
        }

        public override void Save(GameDataWriter writer)
        {
            writer.Write(shapes.Count);
            writer.Write(Random.state);
            writer.Write(CreationSpeed);
            writer.Write(creationProgress);
            writer.Write(DestructionSpeed);
            writer.Write(destructionProgress);
            writer.Write(loadedLevelBuildIndex);
            GameLevel.Current.Save(writer);
            for (int i = 0; i < shapes.Count; i++)
            {
                writer.Write(shapes[i].ShapeId);
                writer.Write(shapes[i].MaterialId);
                shapes[i].Save(writer);
            }
        }
        public override void Load(GameDataReader reader)
        {
            int version = reader.Version;
            if (version > saveVersion)
            {
                Debug.LogError("Unsupported future save version " + version);
                return;
            }
            StartCoroutine(LoadGame(reader));
        }
        private IEnumerator LoadGame(GameDataReader reader)
        {
            int version = reader.Version;
            int count = version <= 0 ? -version : reader.ReadInt();
            if (version >= 3)
            {
                Random.State state = reader.ReadRandomState();
                if (!reseedOnLoad)
                {
                    Random.state = state;
                }
                creationSpeedSlider.value = CreationSpeed = reader.ReadFloat();
                creationProgress = reader.ReadFloat();
                destructionSpeedSlider.value = DestructionSpeed = reader.ReadFloat();
                destructionProgress = reader.ReadFloat();
            }
            yield return LoadLevel(version < 2 ? 1 : reader.ReadInt());
            if (version >= 3)
            {
                GameLevel.Current.Load(reader);
            }
            for (int i = 0; i < count; i++)
            {
                int shapeId = version > 0 ? reader.ReadInt() : 0;
                int materialId = version > 0 ? reader.ReadInt() : 0;
                Shape instance = shapeFactory.Get(shapeId, materialId);
                instance.Load(reader);
                shapes.Add(instance);
            }
        }
    }
}

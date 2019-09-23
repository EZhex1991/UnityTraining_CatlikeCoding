/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-24 15:40:40
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public class Game : MonoBehaviour
    {
        private const float pausedTimeScale = 0f;

        [SerializeField, Range(0, 100)]
        private int startingPlayerHealth = 10;
        [SerializeField, Range(1f, 10f)]
        private float playSpeed = 1f;

        [SerializeField]
        GameTileContentFactory tileContentFactory = default;
        public Vector2Int boardSize = new Vector2Int(11, 11);
        public GameBoard board;

        [SerializeField]
        private WarFactory warFactory = default;

        [SerializeField]
        private GameScenario scenario = default;
        private GameScenario.State activeScenario;

        private GameBehaviourCollection enemies = new GameBehaviourCollection();
        private GameBehaviourCollection nonEnemies = new GameBehaviourCollection();

        private static Game instance;
        public static Shell SpawnShell()
        {
            Shell shell = instance.warFactory.Shell;
            instance.nonEnemies.Add(shell);
            return shell;
        }
        public static Explosion SpawnExplosion()
        {
            Explosion explosion = instance.warFactory.Explosion;
            instance.nonEnemies.Add(explosion);
            return explosion;
        }

        private Ray TouchRay => Camera.main.ScreenPointToRay(Input.mousePosition);

        private int playerHealth;
        private TowerType selectedTowerType;

        private void HandleTouch()
        {
            GameTile tile = board.GetTile(TouchRay);
            if (tile != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    board.ToggleTower(tile, selectedTowerType);
                }
                else
                {
                    board.ToggleWall(tile);
                }
            }
        }
        private void HandleAlternativeTouch()
        {
            GameTile tile = board.GetTile(TouchRay);
            if (tile != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    board.ToggleDestination(tile);
                }
                else
                {
                    board.ToggleSpawnPoint(tile);
                }
            }
        }

        private void BeginNewGame()
        {
            playerHealth = startingPlayerHealth;
            enemies.Clear();
            nonEnemies.Clear();
            board.Clear();
            activeScenario = scenario.Begin();
        }
        public static void SpawnEnemy(EnemyFactory factory, EnemyType type)
        {
            GameTile spawnPoint = instance.board.GetSpawnPoint(Random.Range(0, instance.board.SpawnPointCount));
            Enemy enemy = factory.Get(type);
            enemy.SpawnOn(spawnPoint);
            instance.enemies.Add(enemy);
        }
        public static void EnemyReachedDestination()
        {
            instance.playerHealth -= 1;
        }

        private void Awake()
        {
            playerHealth = startingPlayerHealth;
            board.Initialize(boardSize, tileContentFactory);
            board.ShowGrid = true;
            activeScenario = scenario.Begin();
        }
        private void OnEnable()
        {
            instance = this;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                HandleAlternativeTouch();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                board.ShowPaths = !board.ShowPaths;
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                board.ShowGrid = !board.ShowGrid;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedTowerType = TowerType.Laser;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedTowerType = TowerType.Mortar;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                BeginNewGame();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = Time.timeScale > pausedTimeScale ? pausedTimeScale : 1f;
            }
            else if (Time.timeScale > pausedTimeScale)
            {
                Time.timeScale = playSpeed;
            }

            if (playerHealth <= 0 && startingPlayerHealth > 0)
            {
                Debug.Log("Defeat!");
                BeginNewGame();
            }
            if (!activeScenario.Progress() && enemies.IsEmpty)
            {
                Debug.Log("Victory!");
                BeginNewGame();
                activeScenario.Progress();
            }

            enemies.GameUpdate();
            Physics.SyncTransforms();
            board.GameUpdate();
            nonEnemies.GameUpdate();
        }

        public void OnValidate()
        {
            if (boardSize.x < 2)
            {
                boardSize.x = 2;
            }
            if (boardSize.y < 2)
            {
                boardSize.y = 2;
            }
        }
    }
}

/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-10 15:50:47
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public enum EnemyType { Small, Medium, Large }

    public class Enemy : GameBehaviour
    {
        [SerializeField]
        private Transform model = default;

        private EnemyFactory originFactory;
        public EnemyFactory OriginFactory
        {
            get => originFactory;
            set
            {
                Debug.Assert(originFactory == null, "Redefined origin factory!");
                originFactory = value;
            }
        }
        public float Scale { get; private set; }
        private float Health { get; set; }

        private GameTile tileFrom, tileTo;
        private Vector3 positionFrom, positionTo;
        private float progress, progressFactor;

        private float speed;
        private float pathOffset;
        private Direction direction;
        private DirectionChange directionChange;
        private float directionAngleFrom, directionAngleTo;

        public void SpawnOn(GameTile tile)
        {
            Debug.Assert(tile.NextTileOnPath != null, "Nowhere to go!", this);
            tileFrom = tile;
            tileTo = tile.NextTileOnPath;
            progress = 0f;
            PrepareIntro();
        }
        public void Initialize(float scale, float speed, float pathOffset, float health)
        {
            Scale = scale;
            model.localScale = new Vector3(scale, scale, scale);
            this.speed = speed;
            this.pathOffset = pathOffset;
            Health = health;
        }
        public override void Recycle()
        {
            OriginFactory.Reclaim(this);
        }

        public override bool GameUpdate()
        {
            if (Health <= 0f)
            {
                Recycle();
                return false;
            }
            progress += Time.deltaTime * progressFactor;
            while (progress >= 1f)
            {
                if (tileTo == null)
                {
                    Game.EnemyReachedDestination();
                    Recycle();
                    return false;
                }
                progress = (progress - 1f) / progressFactor;
                PrepareNextState();
                progress *= progressFactor;
            }
            if (directionChange == DirectionChange.None)
            {
                transform.localPosition = Vector3.LerpUnclamped(positionFrom, positionTo, progress);
            }
            else
            {
                float angle = Mathf.LerpUnclamped(directionAngleFrom, directionAngleTo, progress);
                transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            }
            return true;
        }

        public void ApplyDamage(float damage)
        {
            Debug.Assert(damage >= 0f, "Negative damage applied.");
            Health -= damage;
        }

        private void PrepareNextState()
        {
            tileFrom = tileTo;
            tileTo = tileTo.NextTileOnPath;
            positionFrom = positionTo;
            if (tileTo == null)
            {
                PrepareOutro();
                return;
            }
            positionTo = tileFrom.ExitPoint;
            directionChange = direction.GetDirectionChangeTo(tileFrom.PathDirection);
            direction = tileFrom.PathDirection;
            directionAngleFrom = directionAngleTo;
            switch (directionChange)
            {
                case DirectionChange.None: PrepareForward(); break;
                case DirectionChange.TurnRight: PrepareTurnRight(); break;
                case DirectionChange.TurnLeft: PrepareTurnLeft(); break;
                default: PrepareTurnAround(); break;
            }
        }
        private void PrepareForward()
        {
            transform.localRotation = direction.GetRotation();
            directionAngleTo = direction.GetAngle();
            model.localPosition = new Vector3(pathOffset, 0f);
            progressFactor = speed;
        }
        private void PrepareTurnRight()
        {
            directionAngleTo = directionAngleFrom + 90f;
            model.localPosition = new Vector3(pathOffset - 0.5f, 0f);
            transform.localPosition = positionFrom + direction.GetHalfVector();
            progressFactor = speed / (Mathf.PI * 0.5f * (0.5f - pathOffset));
        }
        private void PrepareTurnLeft()
        {
            directionAngleTo = directionAngleFrom - 90f;
            model.localPosition = new Vector3(pathOffset + 0.5f, 0f);
            transform.localPosition = positionFrom + direction.GetHalfVector();
            progressFactor = speed / (Mathf.PI * 0.5f * (0.5f + pathOffset));
        }
        private void PrepareTurnAround()
        {
            directionAngleTo = directionAngleFrom + (pathOffset < 0f ? 180f : -180f);
            model.localPosition = new Vector3(pathOffset, 0f);
            transform.localPosition = positionFrom;
            progressFactor = speed / (Mathf.PI * Mathf.Max(Mathf.Abs(pathOffset), 0.2f));
        }
        private void PrepareIntro()
        {
            positionFrom = tileFrom.transform.localPosition;
            positionTo = tileFrom.ExitPoint;
            direction = tileFrom.PathDirection;
            directionChange = DirectionChange.None;
            directionAngleFrom = directionAngleTo = direction.GetAngle();
            model.localPosition = new Vector3(pathOffset, 0f);
            transform.localRotation = direction.GetRotation();
            progressFactor = 2f * speed;
        }
        private void PrepareOutro()
        {
            positionTo = tileFrom.transform.localPosition;
            directionChange = DirectionChange.None;
            directionAngleTo = direction.GetAngle();
            model.localPosition = new Vector3(pathOffset, 0f);
            transform.localRotation = direction.GetRotation();
            progressFactor = 2f * speed;
        }
    }
}

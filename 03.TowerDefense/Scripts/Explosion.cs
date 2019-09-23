/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-09-19 19:24:46
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.TowerDefense
{
    public class Explosion : WarEntity
    {
        private static int colorPropertyID = Shader.PropertyToID("_Color");
        private static MaterialPropertyBlock propertyBlock;

        [SerializeField, Range(0f, 1f)]
        private float duration = 0.5f;

        [SerializeField]
        private AnimationCurve opacityCurve = default;
        [SerializeField]
        private AnimationCurve scaleCurve = default;

        private float age;
        private float scale;
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            Debug.Assert(meshRenderer != null, "Explosion without renderer!");
        }

        public void Initialize(Vector3 position, float blastRadius, float damage = 0)
        {
            if (damage > 0f)
            {
                TargetPoint.FillBuffer(position, blastRadius);
                for (int i = 0; i < TargetPoint.BufferedCount; i++)
                {
                    TargetPoint.GetBuffered(i).Enemy.ApplyDamage(damage);
                }
            }
            transform.localPosition = position;
            scale = 2f * blastRadius;
        }

        public override bool GameUpdate()
        {
            age += Time.deltaTime;
            if (age >= duration)
            {
                OriginFactory.Reclaim(this);
                return false;
            }
            if (propertyBlock == null)
            {
                propertyBlock = new MaterialPropertyBlock();
            }
            float t = age / duration;
            Color c = Color.clear;
            c.a = opacityCurve.Evaluate(t);
            propertyBlock.SetColor(colorPropertyID, c);
            meshRenderer.SetPropertyBlock(propertyBlock);
            transform.localScale = Vector3.one * (scale * scaleCurve.Evaluate(t));
            return true;
        }
    }
}

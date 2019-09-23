/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-27 11:19:33
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.ObjectManagement
{
    public class Shape : PersistableObject
    {
        [SerializeField]
        private MeshRenderer[] meshRenderers;

        private static int colorPropertyId = Shader.PropertyToID("_Color");
        private static MaterialPropertyBlock sharedPropertyBlock;

        private int shapeId = int.MinValue;
        public int ShapeId
        {
            get { return shapeId; }
            set
            {
                if (shapeId == int.MinValue && value != int.MinValue)
                {
                    shapeId = value;
                }
            }
        }

        public int MaterialId { get; private set; }

        private Color color;

        public Vector3 AngularVelocity { get; set; }
        public Vector3 Velocity { get; set; }

        public void GameUpdate()
        {
            transform.Rotate(AngularVelocity * Time.deltaTime);
            transform.localPosition += Velocity * Time.deltaTime;
        }

        public void SetMaterial(Material material, int materialId)
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = material;
            }
            MaterialId = materialId;
        }
        public void SetColor(Color color)
        {
            this.color = color;
            if (sharedPropertyBlock == null)
            {
                sharedPropertyBlock = new MaterialPropertyBlock();
            }
            sharedPropertyBlock.SetColor(colorPropertyId, color);
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].SetPropertyBlock(sharedPropertyBlock);
            }
        }

        public override void Save(GameDataWriter writer)
        {
            base.Save(writer);
            writer.Write(color);
            writer.Write(AngularVelocity);
            writer.Write(Velocity);
        }
        public override void Load(GameDataReader reader)
        {
            base.Load(reader);
            SetColor(reader.Version > 0 ? reader.ReadColor() : Color.white);
            AngularVelocity = reader.Version >= 4 ? reader.ReadVector3() : Vector3.zero;
            Velocity = reader.Version >= 4 ? reader.ReadVector3() : Vector3.zero;
        }
    }
}

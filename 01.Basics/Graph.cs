/* Author:          ezhex1991@outlook.com
 * CreateTime:      2019-06-20 19:48:15
 * Organization:    #ORGANIZATION#
 * Description:     
 */
using UnityEngine;

namespace EZhex1991.CatlikeCoding.Basics
{
    public class Graph : MonoBehaviour
    {
        private const float pi = Mathf.PI;
        private static GraphFunction[] functions =
        {
            SineFunction,
            Sine2DFunction,
            MultiSineFunction,
            MultiSine2DFunction,
            Ripple,
            Cylinder,
            Sphere,
            Torus,
        };

        public Transform pointPrefab;
        [Range(10, 100)]
        public int resolution = 10;
        public GraphFunctionName function;

        private Transform[] points;
        private GraphFunction func;

        private void Awake()
        {
            points = new Transform[resolution * resolution];
            float step = 2f / resolution;
            Vector3 scale = Vector3.one * step;
            for (int i = 0; i < points.Length; i++)
            {
                Transform point = Instantiate(pointPrefab);
                point.gameObject.hideFlags = HideFlags.HideInHierarchy;
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }

        private void Update()
        {
            float t = Time.time;
            func = functions[(int)function];
            float step = 2f / resolution;
            for (int i = 0, z = 0; z < resolution; z++)
            {
                float v = (z + 0.5f) * step - 1f;
                for (int x = 0; x < resolution; x++, i++)
                {
                    float u = (x + 0.5f) * step - 1f;
                    points[i].localPosition = func(u, v, t);
                }
            }
        }

        public static Vector3 SineFunction(float x, float z, float t)
        {
            Vector3 position;
            position.x = x;
            position.y = Mathf.Sin(pi * (x + t));
            position.z = z;
            return position;
        }
        public static Vector3 Sine2DFunction(float x, float z, float t)
        {
            Vector3 position;
            position.x = x;
            position.y = Mathf.Sin(pi * (x + t));
            position.y += Mathf.Sin(pi * (z + t));
            position.y *= 0.5f;
            position.z = z;
            return position;
        }

        public static Vector3 MultiSineFunction(float x, float z, float t)
        {
            Vector3 position;
            position.x = x;
            position.y = Mathf.Sin(pi * (x + t));
            position.y += Mathf.Sin(2f * pi * (x + 2f * t)) * 0.5f;
            position.y *= 2f / 3f;
            position.z = z;
            return position;
        }
        public static Vector3 MultiSine2DFunction(float x, float z, float t)
        {
            Vector3 position;
            position.x = x;
            position.y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
            position.y += Mathf.Sin(pi * (x + t));
            position.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
            position.y *= 1f / 5.5f;
            position.z = z;
            return position;
        }

        public static Vector3 Ripple(float x, float z, float t)
        {
            Vector3 position;
            float d = Mathf.Sqrt(x * x + z * z);
            position.x = x;
            position.y = Mathf.Sin((4f * d - t) * pi);
            position.y /= 1f + 10f * d;
            position.z = z;
            return position;
        }

        public static Vector3 Cylinder(float u, float v, float t)
        {
            Vector3 position;
            float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;
            position.x = r * Mathf.Sin(pi * u);
            position.y = v;
            position.z = r * Mathf.Cos(pi * u);
            return position;
        }
        public static Vector3 Sphere(float u, float v, float t)
        {
            Vector3 position;
            float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
            r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
            float s = r * Mathf.Cos(pi * 0.5f * v);
            position.x = s * Mathf.Sin(pi * u);
            position.y = r * Mathf.Sin(pi * 0.5f * v);
            position.z = s * Mathf.Cos(pi * u);
            return position;
        }
        public static Vector3 Torus(float u, float v, float t)
        {
            Vector3 position;
            float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
            float r2 = 0.2f + Mathf.Sin(pi * (6f * u + 4f * v + t)) * 0.05f;
            float s = r2 * Mathf.Cos(pi * v) + r1;
            position.x = s * Mathf.Sin(pi * u);
            position.y = r2 * Mathf.Sin(pi * v);
            position.z = s * Mathf.Cos(pi * u);
            return position;
        }
    }
}

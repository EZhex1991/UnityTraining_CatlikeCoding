// Author:			ezhex1991@outlook.com
// CreateTime:		2019-06-20 20:39:45
// Organization:	#ORGANIZATION#
// Description:		

Shader "EZUnity/CatlikeCoding/ColoredPoint" {
	Properties {
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf Standard
		#pragma target 3.0

		half _Glossiness;
		half _Metallic;

		struct Input {
			float3 worldPos;
		};

		UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo.rgb = IN.worldPos.xyz * 0.5 + 0.5;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

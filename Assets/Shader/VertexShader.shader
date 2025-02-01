Shader "CP3D/VertexColor" {
	Properties {
    _MainTex("Albedo (RGB)", 2D) = "white" {}
	
	
    // add selector inside properties:
    [Enum(UnityEngine.Rendering.CullMode)] _CullMode("Cull Mode", Int) = 0
}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CULL [_CullMode]
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert fullforwardshadows addshadow
		#pragma target 3.0

		#include "UnityCG.cginc"
	    #include "Lighting.cginc"
		#include "AutoLight.cginc"

		struct Input {
			float2 uv_MainTex;
			float4 vertColor;
		};

	    UNITY_DECLARE_TEX2D(_MainTex);

		struct v2f {
              float4 pos : SV_POSITION;
        };

		void vert(inout appdata_full v, out Input o){
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.vertColor = v.color;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			half4 tex = UNITY_SAMPLE_TEX2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tex * GammaToLinearSpace(IN.vertColor.rgb);
		}
		ENDCG
	}

	Fallback "Diffuse"//, 2
}
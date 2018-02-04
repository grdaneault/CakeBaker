Shader "Custom/Blobby" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0

		_TipCtrl("Tip Control", Vector) = (0, 1, 0)
		_HeadCtrl("Head Control", Vector) = (0, .7, 0)
		_TummyCtrl("Tummy Control", Vector) = (0, .4, 0)
		_GroundCtrl("Ground Control", Vector) = (0, 0, 0)

		_TipRadius("Tip Radius", Float) = 0
		_HeadRadius("Head Radius", Float) = .8
		_TummyRadius("Tummy Radius", Float) = .6
		_GroundRadius("Ground Radius", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BlinnPhong fullforwardshadows vertex:disp nolightmap

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};

		struct Input {
			float2 uv_MainTex;
		};


		float _HeadRadius, _TipRadius, _TummyRadius, _GroundRadius;
		float4 _HeadCtrl, _TipCtrl, _TummyCtrl, _GroundCtrl;

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void disp(inout appdata v)
		{
			float y = v.texcoord.y;


			float angle = atan2(v.vertex.z, v.vertex.x);


			float radius = 1 * pow(1 - y, 3) * pow(y, 0) * _GroundRadius
				+ 3 * pow(1 - y, 2) * pow(y, 1) * _TummyRadius
				+ 3 * pow(1 - y, 1) * pow(y, 2) * _HeadRadius
				+ pow(y, 3) * _TipRadius;

			float4 anchor = 1 * pow(1 - y, 3) * _GroundCtrl
				+ 3 * pow(1 - y, 2) * y * _TummyCtrl
				+ 3 * pow(1 - y, 1) * pow(y, 2) * _HeadCtrl
				+ pow(y, 3) * _TipCtrl;

			float4 tangent = -3 * pow(1 - y, 2) * _GroundCtrl
				+ 3 * pow(1 - y, 2) * _TummyCtrl
				- 6 * y * (1 - y) * _TummyCtrl
				- 3 * pow(y, 2) * _HeadCtrl
				+ 6 * y * (1 - y) * _HeadCtrl
				+ 3 * pow(y, 2) * _TipCtrl;

			tangent = normalize(tangent);
			float4 normal = float4(cross(float4(0, 0, 1, 1), tangent), 1);
			normal = normalize(normal);

			float4 bitangent = normalize(float4(cross(normal, tangent), 1));

			float c = cos(angle) * radius;
			float d = sin(angle) * radius;

			v.vertex = anchor
				+ c * normal
				+ d * bitangent;
		}

		void surf (Input IN, inout SurfaceOutput  o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Specular = 0.2;
			o.Gloss = 1.0;
		}
		ENDCG
	}
}

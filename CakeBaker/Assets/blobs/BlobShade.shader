Shader "Unlit/BlobShade"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TipCtrl ("Tip Control", Vector) = (0, 1, 0)
		_HeadCtrl ("Head Control", Vector) = (0, .7, 0)
		_TummyCtrl ("Tummy Control", Vector) = (0, .4, 0)
		_GroundCtrl ("Ground Control", Vector) = (0, 0, 0)

		_TipRadius ("Tip Radius", Float) = 0
		_HeadRadius ("Head Radius", Float) = .8
		_TummyRadius ("Tummy Radius", Float) = .6
		_GroundRadius ("Ground Radius", Float) = 1
	}
		
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _HeadRadius, _TipRadius, _TummyRadius, _GroundRadius;
			float4 _HeadCtrl, _TipCtrl, _TummyCtrl, _GroundCtrl;
			
			//float lerp()

			v2f vert (appdata v)
			{
				v2f o;

				float y = v.uv.y;


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

				float c = cos(angle) * 1;
				float d = sin(angle) * 1;

				v.vertex = anchor
					+ c * normal
					+ d * bitangent;

				/*v.vertex.x = anchor.x + cos(angle) * radius;
				v.vertex.z = anchor.z + sin(angle) * radius;
*/

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return fixed4(1, 1, 1, 1);
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
				//// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				//return col;
			}
			ENDCG
		}

	}
}

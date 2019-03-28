Shader "FX/BeamShader"
{

	Properties
	{
			_MainTex("Maske", 2D) = "white" {}
	_Detail("Details", 2D) = "white" {}
	[HDR] _Primary("Primary Color", Color) = (1,1,1,1)
		// Normal: 0700BF
		// Tilt: BF00AD
		// HDR: 8,2
		[HDR] _Secondary("Secondary Color", Color) = (.7,.7,.7,1)
		_Panning("Panning", Vector) = (0,0,1,0)
		_Opacity("Opacity", Range(0,1)) = 1.0
	}
		SubShader
	{
			Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 100
	ZWrite Off
	Blend SrcAlpha DstAlpha

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
		sampler2D _Detail;
		float4 _Detail_ST;

		float4 _Panning;
		float4 _Primary;
		float4 _Secondary;
		float _Opacity;

					v2f vert(appdata v)
					{
							v2f o;
			v.vertex.z = v.vertex.z + 0.1 * cos(v.uv.y + _Time.y);
			v.vertex.y = v.vertex.y + 0.1 * sin(v.uv.y + _Time.y);

							o.vertex = UnityObjectToClipPos(v.vertex);
							o.uv = TRANSFORM_TEX(v.uv, _MainTex);



							UNITY_TRANSFER_FOG(o,o.vertex);
							return o;
					}

					fixed4 frag(v2f i) : SV_Target
					{

			i.uv *= float2(3,1);
			float2 upUV = i.uv + _Panning.xy * _Time.y * _Panning.z * _Panning.w;
			float2 downUV = i.uv - _Panning.xy * _Time.y * _Panning.z;
			fixed4 detailSampleMicro = tex2D(_Detail, downUV * float2(1, 4));
			fixed4 detailSampleMacro = tex2D(_Detail, downUV);
			// sample the texture
			fixed4 col = tex2D(_MainTex, upUV);

			fixed4 detailSample = (detailSampleMicro + detailSampleMacro) * 0.5;

			fixed4 finalColor = lerp(_Primary, _Secondary, detailSample.r);
			finalColor.a = (col.r + detailSample.r) * (finalColor.a);

			float G = (i.uv.y);
			finalColor.a = -(G * (1 - G)) * finalColor.a;
			finalColor.a *= _Opacity;

				return finalColor;
		}
		ENDCG
}
	}
}

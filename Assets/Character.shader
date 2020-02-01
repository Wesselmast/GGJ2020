// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DefaultShader"
{	

	Properties{
		
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
		_Lut("Lookup Tex", 2D) = "white"{}

	}
	
	SubShader{

		Pass{		
			
			Tags {
				"RenderType"="Opaque"
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM	

			#pragma vertex VertexProgram
			#pragma fragment FragmentProgram
			#pragma multi_compile_fwdbase

			#include "UnityStandardBRDF.cginc"
			#include "AutoLight.cginc"

			float4 _Color;
			sampler2D _Lut;
			sampler2D _MainTex;


			struct VertInput{

				float2 uv : TEXCOORD0;
				float4 position : POSITION;
				float3 normal : NORMAL;
				
				
			};

			struct VertOutput{

				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;

				SHADOW_COORDS(2)

			};

			VertOutput VertexProgram(VertInput input){

				VertOutput output;

				output.pos = UnityObjectToClipPos(input.position);
				output.normal = UnityObjectToWorldNormal(input.normal);
				output.uv = input.uv;

				TRANSFER_VERTEX_TO_FRAGMENT(output)

				return output;

			}

			float4 FragmentProgram(VertOutput data) : SV_TARGET {

				data.normal = normalize(data.normal);				
				
				float3 lightDir = _WorldSpaceLightPos0.xyz;
				
				float shadow = SHADOW_ATTENUATION(data);
				float3 lightColor = _LightColor0.rgb;
				
				float nDotL = DotClamped(data.normal, lightDir);

				float4 lightValue = clamp(smoothstep(0, .35, nDotL * shadow),float4(0.87, 0.82, 0.87, 1), float4(0.9, 0.9, 0.9, 1));

				float3 lightInitensity = lightValue  * lightColor;
				float3 indirectDiffuse = unity_AmbientSky;

				float3 diffuse = lightInitensity + indirectDiffuse;

				fixed4 color = _Color * tex2D(_MainTex, data.uv);
				color.rgb *= diffuse;

				return color;

			}

			ENDCG
		}

		Pass {

			Tags {
				"LightMode" = "ShadowCaster"
			}

			CGPROGRAM

			#pragma target 3.0

			#pragma vertex MyShadowVertexProgram
			#pragma fragment MyShadowFragmentProgram

			#include "UnityCG.cginc"

			struct VertexData {
				float4 position : POSITION;
			};

			float4 MyShadowVertexProgram (VertexData v) : SV_POSITION {
				return UnityObjectToClipPos(v.position);
			}

			half4 MyShadowFragmentProgram () : SV_TARGET {
				return 0;
			}


			ENDCG
		}

	}

}

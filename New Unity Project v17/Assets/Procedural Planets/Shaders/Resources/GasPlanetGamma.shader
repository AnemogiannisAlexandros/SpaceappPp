Shader "ProceduralPlanets/GasPlanetGamma"
{
	Properties
	{
		_BodyTexture("BodyTexture", 2D) = "white" {}
		_CapTexture("CapTexture", 2D) = "white" {}
		_BodyNormal("BodyNormal", 2D) = "white" {}
		_CapNormal("CapNormal", 2D) = "white" {}
		_PaletteLookup("PaletteLookup", 2D) = "white" {}
		_LocalStarPosition("LocalStarPosition", Vector) = (-20,-10,-10,0)
		_Solidness("Solidness", Range( 0 , 1)) = 0.1
		_Roughness("Roughness", Range( 0 , 2)) = 0.5
		_Banding("Banding", Range( 0 , 1)) = 0.7721043
		_LocalStarColor("LocalStarColor", Color) = (1,1,1,1)
		_LocalStarAmbientIntensity("LocalStarAmbientIntensity", Range( 0 , 1)) = 0.005
		_LocalStarIntensity("LocalStarIntensity", Range( 0 , 20)) = 1
		_VTiling("VTiling", Int) = 4
		_HTiling("HTiling", Int) = 4
		_Faintness("Faintness", Range( 0 , 1)) = 0
		_StormMask("StormMask", 2D) = "white" {}
		_StormColor("StormColor", Color) = (0,0,0,0)
		_StormTint("StormTint", Float) = 0
		_AtmosphereFalloff("AtmosphereFalloff", Range( 0 , 20)) = 0
		_AtmosphereColor("AtmosphereColor", Color) = (0.7279412,0.6752282,0.4549632,0)
		_ColorTwilight("ColorTwilight", Color) = (0,0,0,0)
		_FaintnessColor("FaintnessColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
			float2 uv2_texcoord2;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _PaletteLookup;
		uniform float _Banding;
		uniform sampler2D _BodyTexture;
		uniform int _HTiling;
		uniform int _VTiling;
		uniform sampler2D _StormMask;
		uniform sampler2D _CapTexture;
		uniform float4 _StormColor;
		uniform float _StormTint;
		uniform float4 _FaintnessColor;
		uniform float _Faintness;
		uniform float4 _AtmosphereColor;
		uniform float _AtmosphereFalloff;
		uniform sampler2D _BodyNormal;
		uniform float _Roughness;
		uniform sampler2D _CapNormal;
		uniform float3 _LocalStarPosition;
		uniform float4 _LocalStarColor;
		uniform float _LocalStarAmbientIntensity;
		uniform float _LocalStarIntensity;
		uniform float4 _ColorTwilight;
		uniform float _Solidness;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float4 appendResult114 = (float4((float)_HTiling , (float)_VTiling , 0.0 , 0.0));
			float2 uv_TexCoord2 = i.uv_texcoord * appendResult114.xy;
			float4 tex2DNode39 = tex2D( _BodyTexture, uv_TexCoord2 );
			float4 tex2DNode125 = tex2D( _StormMask, i.uv_texcoord );
			float lerpResult126 = lerp( tex2DNode39.r , tex2DNode39.b , tex2DNode125.r);
			float clampResult21 = clamp( pow( ( abs( ase_vertex3Pos.y ) / 4.5 ) , 50.0 ) , 0.0 , 1.0 );
			float lerpResult12 = lerp( lerpResult126 , tex2D( _CapTexture, i.uv2_texcoord2 ).g , clampResult21);
			float blendOpSrc24 = ( ( ( ase_vertex3Pos.y + 5.0 ) / 10.0 ) * _Banding );
			float blendOpDest24 = lerpResult12;
			float clampResult31 = clamp( ( saturate( ( 1.0 - ( 1.0 - blendOpSrc24 ) * ( 1.0 - blendOpDest24 ) ) )) , 0.0 , 1.0 );
			float2 appendResult30 = (float2(0.5 , clampResult31));
			float4 tex2DNode38 = tex2D( _PaletteLookup, appendResult30 );
			float4 lerpResult144 = lerp( tex2DNode38 , _StormColor , _StormTint);
			float lerpResult140 = lerp( 0.0 , tex2DNode39.a , tex2DNode125.r);
			float4 lerpResult137 = lerp( tex2DNode38 , lerpResult144 , lerpResult140);
			float4 lerpResult121 = lerp( lerpResult137 , _FaintnessColor , _Faintness);
			float4 temp_cast_3 = (0.0).xxxx;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float dotResult69 = dot( ase_normWorldNormal , ase_worldViewDir );
			float temp_output_71_0 = saturate( dotResult69 );
			float4 lerpResult153 = lerp( temp_cast_3 , _AtmosphereColor , pow( ( 1.0 - temp_output_71_0 ) , _AtmosphereFalloff ));
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3 normalizeResult85 = normalize( mul( UnpackScaleNormal( tex2D( _BodyNormal, uv_TexCoord2 ) ,_Roughness ), float3x3(ase_worldTangent, ase_worldBitangent, ase_worldNormal) ) );
			float3 normalizeResult97 = normalize( mul( UnpackScaleNormal( tex2D( _CapNormal, i.uv2_texcoord2 ) ,_Roughness ), float3x3(ase_worldTangent, ase_worldBitangent, ase_worldNormal) ) );
			float3 lerpResult99 = lerp( normalizeResult85 , normalizeResult97 , clampResult21);
			float4 transform66 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float4 normalizeResult64 = normalize( ( float4( _LocalStarPosition , 0.0 ) - transform66 ) );
			float dotResult51 = dot( float4( lerpResult99 , 0.0 ) , normalizeResult64 );
			float dotResult157 = dot( float4( ase_normWorldNormal , 0.0 ) , normalizeResult64 );
			o.Emission = ( ( lerpResult121 + lerpResult153 ) * ( ( ( saturate( ( saturate( dotResult51 ) + ( _LocalStarColor * _LocalStarAmbientIntensity ) ) ) * _LocalStarColor ) * _LocalStarIntensity ) + ( _ColorTwilight * saturate( pow( ( 1.0 - pow( dotResult157 , 2.0 ) ) , 80.0 ) ) ) ) ).rgb;
			o.Alpha = saturate( ( pow( temp_output_71_0 , 4.0 ) * (10.0 + (_Solidness - 0.0) * (10000.0 - 10.0) / (1.0 - 0.0)) ) );
		}

		ENDCG
	}
}

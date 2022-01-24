Shader "Soar/color_camera_depth_pass"
{
   
	Properties
    {
		_gray("Gray Scale", Float) = 0
	}

	SubShader
    {

		Tags { "RenderType"="Opaque" }

		Pass
        {
            Lighting Off
            Blend Off
            Cull Back
            ZWrite On
            ZTest Greater
            ColorMask R
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			#define f_05 float3(.5, .5, .5)
			#define f_10 float3(1., 1., 1.)

            uniform float4x4 _SecondCameraMatrices;

            uniform float cameraDepthNear;
            uniform float cameraDepthFar;


			struct v2f
            {
			   float4 pos : SV_POSITION;
			   float scrPos:TEXCOORD0;
			};

            struct appdata 
            {
                float4 vertex : POSITION;
            };

            float linearize_depth( float depth )
            {
	            return ( 2.0 * cameraDepthNear * cameraDepthFar ) / ( cameraDepthNear + cameraDepthFar - (depth * 2.0 - 1.0) * ( cameraDepthFar - cameraDepthNear ) );
            }

			v2f vert (appdata v)
            {
			   v2f o;
			   o.pos = UnityObjectToClipPos( float4( v.vertex.xyz, 1.0f ) );
               float4 clipSpace = mul( float4( v.vertex.xyz, 1.0f ), _SecondCameraMatrices);
               o.scrPos = linearize_depth(clipSpace.z / clipSpace.w);
			   return o;
			}

			float4 frag (v2f i) : SV_Target
            {
                
				return float4(i.scrPos, 0.0, 0.0, 1.0); 
				
			}
			ENDCG
		}
	}
	//FallBack "Diffuse"
}
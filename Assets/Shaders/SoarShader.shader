// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Soar/SoarShader"
{
    Properties
    {
        _CameraRGB("Tex", 2DArray) = "" {}
        _CameraDepth("Depth Tex", 2DArray) = ""{}
        _FadeDistance("Fade", Range(0.0005,0.45)) = 0.025

        [Header(Ambient)]
        _Ambient("Intensity", Range(0., 1.)) = 0.1
        _AmbColor("Color", color) = (1., 1., 1., 1.)

        [Header(Diffuse)]
        _Diffuse("Val", Range(0., 1.)) = 1.
        _DifColor("Color", color) = (1., 1., 1., 1.)

    }
        SubShader
        {
            Tags
            {
                "RenderPipeline" = "UniversalPipeline"
            }

            LOD 100
            Pass
            {
                CGPROGRAM
                #pragma target 5.0
                #pragma vertex vert
                #pragma fragment frag
                #pragma require 2darray

                #include "UnityCG.cginc"

                #define MAXIMUM_COLOR_CAMERA_COUNT 6

                UNITY_DECLARE_TEX2DARRAY(_CameraRGB);
                UNITY_DECLARE_TEX2DARRAY(_CameraDepth);

                uniform float _FadeDistance;
                float projectionDepth;
                centroid float2 colorDistorted;
                float confidence;
                float depthValue;
                float2 dx;
                float2 projected;
                float4 clipSpace;
                float difference;

                //uniform int _CameraCount;

         //       uniform                   float          cameraDepthNear[ MAXIMUM_COLOR_CAMERA_COUNT ];
         //       uniform                   float          cameraDepthFar[ MAXIMUM_COLOR_CAMERA_COUNT ];
         //       uniform                   float4         cameraIntrinsics[ MAXIMUM_COLOR_CAMERA_COUNT * 6 ];
         //       uniform                   bool           useModifiedDedistortion[ MAXIMUM_COLOR_CAMERA_COUNT ];
                uniform                   float          transitionStrength;
                //      uniform                   float          nearCamera;
                //      uniform                   float          farCamera;

                      UNITY_INSTANCING_BUFFER_START(Props)
                              UNITY_DEFINE_INSTANCED_PROP(float4x4, _CameraMatrices[MAXIMUM_COLOR_CAMERA_COUNT])
                              UNITY_DEFINE_INSTANCED_PROP(uniform float, nearCamera)
                              UNITY_DEFINE_INSTANCED_PROP(uniform float, farCamera)
                              UNITY_DEFINE_INSTANCED_PROP(uniform int, _CameraCount)
                              UNITY_DEFINE_INSTANCED_PROP(uniform float4, cameraIntrinsics[MAXIMUM_COLOR_CAMERA_COUNT * 6])
                              UNITY_DEFINE_INSTANCED_PROP(uniform float, cameraDepthNear[MAXIMUM_COLOR_CAMERA_COUNT])
                              UNITY_DEFINE_INSTANCED_PROP(uniform float, cameraDepthFar[MAXIMUM_COLOR_CAMERA_COUNT])
                              UNITY_DEFINE_INSTANCED_PROP(uniform float4, chromaKey);
                      UNITY_INSTANCING_BUFFER_END(Props)

                      struct VertexShaderOutput
                      {
                          float4 position      : SV_POSITION;
                          centroid float3 capturePosition : TEXCOORD0;
                          centroid float4 screenPosition : TEXCOORD1;
                          float3 normal : NORMAL;
                          float4 light : COLOR0;
                      };

                      struct appdata
                      {
                          float4 vertex : POSITION;
                          float3 normal : NORMAL;
                      };

                      float sampleDepth(int cameraIndex, float2 offsetTexel) {

                          return UNITY_SAMPLE_TEX2DARRAY(_CameraDepth, float3(offsetTexel, cameraIndex)).x;

                      }

                      float2 rgbToUV(float3 rgb)
                      {
                          return float2(dot(rgb.rgb, float3(-0.169f, -0.331f, 0.5f)), dot(rgb.rgb, float3(0.5f, -0.419f, -0.081f))) + 0.5f;
                      }

                      float chromaWeight(float3 rgb)
                      {
                          float similarity = distance(rgbToUV(rgb), chromaKey.xy);
                          float whiteCos = chromaKey.w * dot(normalize(rgb), float3(0.577f, 0.577f, 0.577f)); // use a chroma space colour hemisphere (which is the equivalent to hue + saturation) to evaluate similarity to "white" (i.e. perfect desaturation). 

                          return pow(clamp(((whiteCos + 1) * similarity - chromaKey.w) / (chromaKey.z), 0.0f, 1.0f), chromaKey.z);
                      }

                      float linearize_depth(int cameraIndex, float depth)
                      {
                          nearCamera = cameraDepthNear[cameraIndex];
                          farCamera = cameraDepthFar[cameraIndex];

                          return (2.0 * nearCamera * farCamera) / (nearCamera + farCamera - (depth * 2.0 - 1.0) * (farCamera - nearCamera));
                      }

                      // START OF DISTORT FUNCTION //

                      float2 distort(float2 undistorted, float2 rgbSize, int intrinsicsIndex)
                      {
                          int baseIndex = intrinsicsIndex * 6;

                          float4 cf = cameraIntrinsics[baseIndex];
                          float4 codp21 = cameraIntrinsics[baseIndex + 1];
                          float4 k123x = cameraIntrinsics[baseIndex + 2];
                          float4 k456x = cameraIntrinsics[baseIndex + 3];
                          float4 pinholeiFP = cameraIntrinsics[baseIndex + 4];
                          float2 cameraResolution = cameraIntrinsics[baseIndex + 5].xy;

                          undistorted *= cameraResolution;
                          undistorted -= pinholeiFP.zw;
                          undistorted *= pinholeiFP.xy;

                          float2   p = undistorted - codp21.xy;
                          float    rs = dot(p, p);

                          /* note, max radius check here deliberately removed, it'll just hit the texture borders */

                          float rss = rs * rs;

                          float3 rFactor = float3(rs, rss, rss * rs);

                          float a = 1.0f + dot(rFactor, k123x.xyz);
                          float b = 1.0f + dot(rFactor, k456x.xyz);
                          float bi = (b != 0.0f) ? (1.0f / b) : 1.0f;

                          float d = a * bi;

                          float offset = 0;

                          float2  fp = p * d;
                          float2  fp2 = fp * fp;
                          float fxyp = fp.x * fp.y;

                          float2  rs2fp2 = 2.0f * fp2 + rs;
                          float2  fpd = fp + rs2fp2 * codp21.zw + 2.0f * fxyp * codp21.wz;

                          return ((float2(0.5f, offset + 0.5f) + min((fpd + codp21.xy) * cf.zw + cf.xy, cameraResolution)) / rgbSize) * float2(1, -1) + float2(0, 1);
                      }

                      // END OF DISTORT FUNCTION //

                      float _Diffuse;
                      float4 _DifColor;

                      float _Ambient;
                      float _TempPropOne;
                      float _TempPropTwo;
                      float4 _AmbColor;
                      float4 _LightColor0;

                      VertexShaderOutput vert(appdata inputVertex)
                      {
                          VertexShaderOutput outputVertex;
                          fixed4 amb = _Ambient * _AmbColor;
                          float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                          outputVertex.position = UnityObjectToClipPos(inputVertex.vertex);
                          outputVertex.capturePosition.xyz = inputVertex.vertex.xyzw;
                          outputVertex.screenPosition = ComputeScreenPos(outputVertex.position);
                          outputVertex.normal = normalize(mul(inputVertex.normal, unity_WorldToObject).xyz);
                          float4 NdotL = max(0., dot(outputVertex.normal, lightDir) * _LightColor0);
                          float4 dif = NdotL * _Diffuse * _LightColor0 * _DifColor;
                          outputVertex.light = dif + amb;
                          return outputVertex;
                      }

                      static const float samples = 3.0f;
                      static const float bounds = (samples / 2.0f) - 0.5f;

                      static const float n = samples * samples;
                      static const float vfactor = (1.0f / n) / (n - 1.0f);
                      static const float precisionFactor = 1024.0f;

                      float IsNan_float(float In)
                      {
                          return (In < 0.0 || In > 0.0 || In == 0.0) ? 0 : 1;
                      }

                      float4 frag(VertexShaderOutput inputVertex, uniform float4x4 cameraViewProjection[MAXIMUM_COLOR_CAMERA_COUNT]) : SV_TARGET
                      {

                          float4 sumColors = 0.0;
                          float4 fragmentColor = 0.0;
                          float sumConfidence = 0.0;
                          float fadeDistance = _FadeDistance * precisionFactor;

                          float3 textureDimensions;
                          _CameraRGB.GetDimensions(textureDimensions.x, textureDimensions.y, textureDimensions.z);

                          float2 depthMapTexelOffset = 1.0f / textureDimensions.xy;

                          float4 outputs[MAXIMUM_COLOR_CAMERA_COUNT];

                          [unroll]
                          for (int cameraIndex = 0; cameraIndex < MAXIMUM_COLOR_CAMERA_COUNT; cameraIndex++)
                          {
                              // allows unrolling but early termination.

                              if (cameraIndex >= _CameraCount)
                              {
                                  break;
                              }

                              else
                              {
                                  // TODO - we should make this per vertex to save some shader power - will just require some rejig - CS

                                  clipSpace = mul(float4(inputVertex.capturePosition, 1.0f), _CameraMatrices[cameraIndex]);
                                  projected = clipSpace.xy / clipSpace.w;

                                  confidence = 0.0f;

                                  projectionDepth = linearize_depth(cameraIndex, clipSpace.z / clipSpace.w);

                                  colorDistorted = distort(projected, textureDimensions.xy, cameraIndex);

                                  dx = ddx_fine(colorDistorted * precisionFactor);
                                  float2 dy = ddy_fine(colorDistorted * precisionFactor);

                                  for (float y = -bounds; y <= bounds; y++)
                                  {
                                      for (float x = -bounds; x <= bounds; x++)
                                      {
                                          float2 offsetTexel = float2(x, y) * depthMapTexelOffset + projected.xy;

                                          offsetTexel.y = 1 - offsetTexel.y;

                                          depthValue = UNITY_SAMPLE_TEX2DARRAY(_CameraDepth, float3(offsetTexel, cameraIndex)).x;
                                          difference = abs(depthValue - projectionDepth) * precisionFactor;

                                          if (difference < fadeDistance)
                                          {
                                              confidence += precisionFactor * saturate(1.0f - (difference / fadeDistance)) / pow(depthValue, 2.0f);
                                          }
                                      }
                                  }

                                  float dArea = abs(dx.x * dy.y - dy.x * dx.y);
                                  float areaWeight = 0.125f + dArea;

                                  confidence *= areaWeight;

                                  float4 colorRead = UNITY_SAMPLE_TEX2DARRAY(_CameraRGB, float3(colorDistorted,  cameraIndex));

                                  confidence *= colorRead.w;

                                  if (chromaKey.z != 0)
                                  {
                                      float chromaWeighting = chromaWeight(colorRead);
                                      float colorY = min(dot(colorRead, float3(0.2126f, 0.7152f, 0.0722f)), 1);

                                      colorRead = lerp(float4(colorY, colorY, colorY, 0.0f), colorRead, clamp(pow(chromaWeighting / chromaKey.w, 1.0f / chromaKey.z), 0, 1));

                                      confidence *= chromaWeighting;
                                  }

                                  outputs[cameraIndex] = float4(colorRead.xyz, confidence);
                                  sumConfidence += confidence;

                                 }
                          }

                          if (sumConfidence > 0.0005f)
                          {
                              float invSumConfidence = 1.0f / sumConfidence;

                              for (int cameraIndex = 0; cameraIndex < MAXIMUM_COLOR_CAMERA_COUNT; ++cameraIndex)
                              {
                                  if (cameraIndex >= _CameraCount)
                                  {
                                      break;
                                  }

                                  float reweightedConfidence = exp(transitionStrength * outputs[cameraIndex].w * invSumConfidence) - 1.0f;

                                  sumColors += float4(outputs[cameraIndex].xyz * reweightedConfidence, reweightedConfidence);
                              }
                          }

                          if (sumConfidence > 0.0f)
                          {
                              return float4((sumColors.xyz / sumColors.w) * inputVertex.light.rgb, 1.0f);
                          }
                          else
                          {
                              return float4(0, 0, 0, 1.0f);
                          }

                      }

                      ENDCG
                  }
        }
            Fallback "Diffuse"
}
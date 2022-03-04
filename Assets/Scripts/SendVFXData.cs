using System.Collections;
using System.Collections.Generic;
using SoarSDK;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.VFX;

public class SendVFXData : MonoBehaviour
{

    public bool meshSet;
    [SerializeField]
    public VisualEffect visualEffect;
    [SerializeField]
    public Mesh mesh;
    [SerializeField]
    [HideInInspector] public RenderTexture actualArray;
    [SerializeField]
    [HideInInspector] public RenderTexture depthArray;
    [SerializeField]
    [HideInInspector] public float4x4 testMatrix;
    [SerializeField]
    [HideInInspector] public float depthNear;
    [SerializeField]
    [HideInInspector] public float depthFar;

    [HideInInspector] public Vector4[] camIntrinsics;
    [HideInInspector] public Matrix4x4[] matrixArray;
    [HideInInspector] public float[] depthNearArray;
    [HideInInspector] public float[] depthFarArray;
    [HideInInspector] public int cameraCount;
    [HideInInspector] public int index;

    public bool checkPos;

    public bool testBool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MeshFilter>().mesh != null)
        {
            mesh = GetComponent<MeshFilter>().mesh;

            if (!meshSet)
            {
                visualEffect.SetMesh("New Mesh", mesh);
                actualArray = GetComponent<MeshRenderer>().material.GetTexture("_CameraRGB") as RenderTexture;
                depthArray = GetComponent<MeshRenderer>().material.GetTexture("_CameraDepth") as RenderTexture;
                if (!checkPos)
                {
                    visualEffect.gameObject.transform.localRotation = gameObject.transform.localRotation;
                    if(gameObject.transform.localPosition.y != 0.0f)
                    {
                        visualEffect.gameObject.transform.localPosition = gameObject.transform.localPosition;
                        cameraCount = GetComponent<PlaybackInstance>().props.GetInt("_CameraCount");
                        checkPos = true;
                    }
                }
                for (index = 0; index < cameraCount; index++)
                {
                    if (GetComponent<MeshRenderer>().material.GetTexture("_CameraRGB") != null)
                    {

                        if (index >= cameraCount)
                        {
                            break;
                        }
                        if (index == 0)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentOne", testMatrix.c0);
                                visualEffect.SetVector4("c1SentOne", testMatrix.c1);
                                visualEffect.SetVector4("c2SentOne", testMatrix.c2);
                                visualEffect.SetVector4("c3SentOne", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near One", depthNear);
                                visualEffect.SetFloat("Depth Far One", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentOne", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentOne", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentOne", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentOne", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentOne", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentOne", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 1)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentTwo", testMatrix.c0);
                                visualEffect.SetVector4("c1SentTwo", testMatrix.c1);
                                visualEffect.SetVector4("c2SentTwo", testMatrix.c2);
                                visualEffect.SetVector4("c3SentTwo", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Two", depthNear);
                                visualEffect.SetFloat("Depth Far Two", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentTwo", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentTwo", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentTwo", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentTwo", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentTwo", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentTwo", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 2)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentThree", testMatrix.c0);
                                visualEffect.SetVector4("c1SentThree", testMatrix.c1);
                                visualEffect.SetVector4("c2SentThree", testMatrix.c2);
                                visualEffect.SetVector4("c3SentThree", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Three", depthNear);
                                visualEffect.SetFloat("Depth Far Three", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentThree", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentThree", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentThree", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentThree", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentThree", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentThree", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 3)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentFour", testMatrix.c0);
                                visualEffect.SetVector4("c1SentFour", testMatrix.c1);
                                visualEffect.SetVector4("c2SentFour", testMatrix.c2);
                                visualEffect.SetVector4("c3SentFour", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Four", depthNear);
                                visualEffect.SetFloat("Depth Far Four", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentFour", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentFour", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentFour", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentFour", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentFour", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentFour", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 4)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentFive", testMatrix.c0);
                                visualEffect.SetVector4("c1SentFive", testMatrix.c1);
                                visualEffect.SetVector4("c2SentFive", testMatrix.c2);
                                visualEffect.SetVector4("c3SentFive", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Five", depthNear);
                                visualEffect.SetFloat("Depth Far Five", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentFive", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentFive", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentFive", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentFive", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentFive", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentFive", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 5)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentSix", testMatrix.c0);
                                visualEffect.SetVector4("c1SentSix", testMatrix.c1);
                                visualEffect.SetVector4("c2SentSix", testMatrix.c2);
                                visualEffect.SetVector4("c3SentSix", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Six", depthNear);
                                visualEffect.SetFloat("Depth Far Six", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentSix", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentSix", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentSix", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentSix", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentSix", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentSix", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 6)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentSeven", testMatrix.c0);
                                visualEffect.SetVector4("c1SentSeven", testMatrix.c1);
                                visualEffect.SetVector4("c2SentSeven", testMatrix.c2);
                                visualEffect.SetVector4("c3SentSeven", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Seven", depthNear);
                                visualEffect.SetFloat("Depth Far Seven", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentSeven", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentSeven", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentSeven", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentSeven", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentSeven", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentSeven", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }

                        if (index == 7)
                        {
                            testMatrix = GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index];
                            depthNear = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthNear")[index];
                            camIntrinsics = GetComponent<PlaybackInstance>().props.GetVectorArray("cameraIntrinsics");
                            depthFar = GetComponent<PlaybackInstance>().props.GetFloatArray("cameraDepthFar")[index];
                            visualEffect.SetTexture("New Texture2DArray", actualArray);
                            visualEffect.SetTexture("New Depth Texture2DArray", depthArray);
                            if (GetComponent<PlaybackInstance>().props.GetMatrixArray("_CameraMatrices")[index] != null)
                            {
                                visualEffect.SetVector4("c0SentEight", testMatrix.c0);
                                visualEffect.SetVector4("c1SentEight", testMatrix.c1);
                                visualEffect.SetVector4("c2SentEight", testMatrix.c2);
                                visualEffect.SetVector4("c3SentEight", testMatrix.c3);

                                visualEffect.SetFloat("Depth Near Eight", depthNear);
                                visualEffect.SetFloat("Depth Far Eight", depthFar);

                            }
                            for (int j = 0; j < 6; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        visualEffect.SetVector4("cfSentEight", camIntrinsics[j]);
                                        break;
                                    case 1:
                                        visualEffect.SetVector4("cod21pSentEight", camIntrinsics[j]);
                                        break;
                                    case 2:
                                        visualEffect.SetVector4("k123xSentEight", camIntrinsics[j]);
                                        break;
                                    case 3:
                                        visualEffect.SetVector4("k456xSentEight", camIntrinsics[j]);
                                        break;
                                    case 4:
                                        visualEffect.SetVector4("pinholeiFPSentEight", camIntrinsics[j]);
                                        break;
                                    case 5:
                                        visualEffect.SetVector4("cameraResolutionSentEight", camIntrinsics[j]);
                                        break;
                                    default:
                                        break;

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

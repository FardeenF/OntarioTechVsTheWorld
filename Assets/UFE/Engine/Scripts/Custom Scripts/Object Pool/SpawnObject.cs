using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    //public float projectileForce = 20.0f;
    public GameObject objectPrefab;
    GameObject spawnedObject;
    GameObject lastObject;
    public float spawnTime = 0.5f;
    private float lastTime;
    float torque1 = 100.0f;
    float torque2 = 65.0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTime > spawnTime)
        {
            lastTime = Time.time;
            spawnedObject = Instantiate(objectPrefab, RandomPointInBox(transform.position), Quaternion.identity);
        }
    }



    public Vector3 RandomPointInBox(Vector3 center)
    {
        return center + new Vector3(
            //Random.Range(-4.9f, 4.9f),
            Random.Range(-24.9f, 24.9f),
            0.0f,
            Random.Range(-4.82f, 4.82f)
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectFromPool : MonoBehaviour
{
    //public float projectileForce = 20.0f;
    GameObject spawnedObject;
    public float spawnTime = 0.5f;
    private float lastTime;
    float torque1 = 100.0f;
    float torque2 = 65.0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastTime > spawnTime)
        {
            lastTime = Time.time;
            spawnedObject = ObjectPool.SharedInstance.GetPooledObject();

            if (spawnedObject != null)
            {
                Vector3 position = RandomPointInBox(this.transform.position);
                spawnedObject.transform.position = position;
                spawnedObject.transform.rotation = Quaternion.Euler(this.transform.forward);
                spawnedObject.SetActive(true);

                //spawnedObject.GetComponent<Rigidbody>().AddTorque(transform.up * torque1 * torque2);
            }
        }
    }



    public Vector3 RandomPointInBox(Vector3 center)
    {

        return center + new Vector3(
            Random.Range(-4.9f, 4.9f),
            0.0f,
            Random.Range(-4.82f, 4.82f)
        );

    }


}

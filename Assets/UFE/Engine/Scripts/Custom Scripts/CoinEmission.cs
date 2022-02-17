using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEmission : MonoBehaviour
{
    public GameObject coin;
    public Transform emissionPos;

    public float emissionRate = 100f;
    public int emissionAmount = 1;
    public Vector3 range;
    public bool doEmission = true;
    public bool haveLifetime = true;
    public float lifetime = 3f;
    public float push = 10f;
    public Vector3 pushDir;
    public Vector3 angularVel;
    public Vector3 coinScale;

    float cooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        cooldown += emissionRate * Time.deltaTime;
        if (cooldown >= 1f)
        {
            if (doEmission)
                Emit();
            else if (Input.GetKeyDown(KeyCode.Mouse0))
                Emit();
        }
    }

    public void Emit()
    {
        for (int i = 0; i <= emissionAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(emissionPos.position.x - range.x / 2, emissionPos.position.x + range.x / 2),
                                      Random.Range(emissionPos.position.y - range.y / 2, emissionPos.position.y + range.y / 2),
                                      Random.Range(emissionPos.position.z - range.z / 2, emissionPos.position.z + range.z / 2));
            GameObject c = Instantiate(coin, pos, Random.rotation);
            c.transform.localScale = new Vector3(c.transform.localScale.x * coinScale.x,
                                                 c.transform.localScale.y * coinScale.y,
                                                 c.transform.localScale.z * coinScale.z);
            c.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(pushDir.x * push, pushDir.y * push, pushDir.z * push), ForceMode.Impulse);
            c.gameObject.GetComponent<Rigidbody>().angularVelocity = angularVel;
            if (haveLifetime)
                Destroy(c, lifetime);
            cooldown = 0f;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOnDeath : MonoBehaviour
{
    public Animator controller;
    public GameObject emitter;
    public SkinnedMeshRenderer r1;
    public SkinnedMeshRenderer r2;

    public float delay;
    float countdown;
    bool doDestroy = true;

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;
        if (countdown >= controller.GetCurrentAnimatorStateInfo(0).length + delay && doDestroy)
        {
            emitter.GetComponent<CoinEmission>().Emit();
            //controller.enabled = false;
            r1.enabled = false;
            r2.enabled = false;
            countdown = 0f;
            doDestroy = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //controller.enabled = true;
            controller.SetTrigger("die");
            r1.enabled = true;
            r2.enabled = true;
            countdown = 0f;
            doDestroy = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;

public class HitReaction : MonoBehaviour
{
    float BPM = 160;
    float timer = 0.0f;
    bool hasHit = false;
    float beatTempo;
    int counter = 0;
    private bool isDirty;


    // Start is called before the first frame update
    void Start()
    {
        UFE.OnHit += this.HitChecker;
        beatTempo = BPM / 60.0f;
        isDirty = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(UFE.isPaused())
        {
            isDirty = false;
        }
        else
        {
            isDirty = true;
        }



       if(isDirty)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);

            //if (timer % beatTempo > 0.0f && timer % beatTempo < 0.5f)
            //{
            //    timer = 0.0f;
            //    counter++;
            //    Debug.Log("RHYTHYM ATTACK!  " + counter);
            //}


            if (timer > 2.6)
            {
                timer = 0.0f;
            }
        }

    }



    void HitChecker(HitBox strokeHitbox, MoveInfo move, ControlsScript hitter)
    {
        if(isDirty)
        {
            hasHit = true;

            if (timer % beatTempo > 0.0f && timer % beatTempo < 0.5f)
            {
                timer = 0.0f;
                Debug.Log("RHYTHYM ATTACK!");
                //hitter.currentHit.
            }

            Debug.Log(UFE.GetStage().stageName);
        }
    }
}

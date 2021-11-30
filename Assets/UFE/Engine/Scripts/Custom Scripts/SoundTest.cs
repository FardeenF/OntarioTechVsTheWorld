using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE3D;

public class SoundTest : MonoBehaviour
{
    [Tooltip("Seconds to wait before triggering the explosion particles and the trauma effect")]
    public float Delay = 1;
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    public float MaximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    public float Range = 45;

    bool rhythmHit = false;

    StressReceiver receiver;
    float cameraStress;

    private void Start()
    {
        UFE.OnHit += this.HitChecker;
        UFE.OnLifePointsChange += this.LifeChange;

        var targets = UnityEngine.Object.FindObjectsOfType<GameObject>();
        for (int i = 0; i < targets.Length; ++i)
        {
            receiver = targets[i].GetComponent<StressReceiver>();
            if (receiver == null) continue;
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            /* Apply stress to the object, adjusted for the distance */
            if (distance > Range) continue;
            float distance01 = Mathf.Clamp01(distance / Range);
            cameraStress = (1 - Mathf.Pow(distance01, 2)) * MaximumStress;
            receiver.InduceStress(cameraStress);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Song Beats: " + Conductor.instance.songPositionInBeatsInteger);


        //if (Mathf.Round(Conductor.instance.songPositionInBeatsInteger) % 2 == 0)
        //{
        //    //Debug.Log("Beat Rhythm!");
        //    rhythmHit = true;
        //}
        //else if(Mathf.Round(Conductor.instance.songPositionInBeatsInteger) % 2 == 1)
        //{
        //    Debug.Log("Wait!");
        //    rhythmHit = false;
        //}

        if (Conductor.instance.songPositionInBeatsInteger % 2 == 0)
        {
            //Debug.Log("Beat Rhythm!");
            rhythmHit = true;
        }
        else
        {
            rhythmHit = false;
        }
        //else if (Conductor.instance.songPositionInBeatsInteger % 2 == 1)
        //{
        //    //Debug.Log("Wait!");
        //    rhythmHit = false;
        //}
    }

    void HitChecker(HitBox strokeHitbox, MoveInfo move, ControlsScript hitter)
    {
        if (rhythmHit)
        {
            Debug.Log("Beat Rhythm!");

            receiver.InduceStress(cameraStress);

            //UFE.config.player1Character.moves[0].attackMoves[0].hits[0]._damageOnHit = 100.0f;
        }
    }

    void LifeChange(float newLifePoints, ControlsScript player)
    {
        Debug.Log(newLifePoints);
    }
}

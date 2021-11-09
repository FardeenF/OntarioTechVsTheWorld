using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryDialogue : MonoBehaviour
{
    public GameObject textPrefab;
    Text message;

    string message1 = "Old Hunter ridgeback is taking over the school.";
    string message2 = "Jealously is an inner consciousness of ones own inferiority.";
    string message3 = "He is brainwashing the teachers.";
    string message4 = "You must save the school.";
    string message5 = "New Hunter has ruined old Hunter.";

    // Start is called before the first frame update
    void Start()
    {
        message = textPrefab.GetComponent<Text>();

        int randNum = Random.Range(0, 4);

        if(randNum == 0)
        {
            message.text = message1;
        }
        else if(randNum == 1)
        {
            message.text = message2;
        }
        else if (randNum == 2)
        {
            message.text = message3;
        }
        else if (randNum == 3)
        {
            message.text = message4;
        }
        else if (randNum == 4)
        {
            message.text = message5;
        }
    }

    
}

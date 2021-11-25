using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MessagePanelBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageDisplay;
    [SerializeField] private float tweenTime;

    private Queue<string> messages = new Queue<string>();

    private void OnEnable()
    {
        DefaultMainMenuScreen.onlineClick += OnlineDisabledDisplayMessage;
    }

    private void OnDisable()
    {
        DefaultMainMenuScreen.onlineClick -= OnlineDisabledDisplayMessage;
    }



    private void OnlineDisabledDisplayMessage(string obj)
    {
        messageDisplay.text = obj;
        messageDisplay.rectTransform.localScale = new Vector3(1.7f, 1.7f, 1.7f);

        messages.Enqueue(obj);

        if(DOTween.IsTweening(messageDisplay.rectTransform))
        {
            messageDisplay.rectTransform.DORestart();
            messages.Dequeue();
        }
    }

    private void Update()
    {
        CheckQueue();
    }

    private void CheckQueue()
    {
        if (!DOTween.IsTweening(messageDisplay.rectTransform))
        {
            if (messages.Count > 0)
            {
                DisplayMessageAnimate(messages.Dequeue());
            }
        }
    }

    private void DisplayMessageAnimate(string obj)
    {
        messageDisplay.text = obj;
        messageDisplay.rectTransform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
        messageDisplay.rectTransform.DOScale(0, tweenTime);
    }
}

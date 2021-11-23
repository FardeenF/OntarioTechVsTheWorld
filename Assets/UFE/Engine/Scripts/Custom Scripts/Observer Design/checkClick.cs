using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class checkClick : MonoBehaviour
{
    public static event Action userClick;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            #region observer
            userClick?.Invoke();
            #endregion
        }
    }
}

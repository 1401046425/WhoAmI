using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseClick2D : MonoBehaviour
{
    public UnityEvent OnClickEvent = new UnityEvent();

    [SerializeField]
    private bool JustOnes;

    private bool IsClick;
    private void OnMouseDown()
    {
        if (JustOnes)
        {
            if (JustOnes)
            {
                if(IsClick)
                    return;
                IsClick = true;
            }
        }

        OnClickEvent.Invoke();
    }
}

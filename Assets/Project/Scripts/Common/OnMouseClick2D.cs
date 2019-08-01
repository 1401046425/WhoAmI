using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnMouseClick2D : MonoBehaviour
{
    public UnityEvent OnClickEvent = new UnityEvent();

    private void OnMouseDown()
    {
        OnClickEvent.Invoke();
    }
}

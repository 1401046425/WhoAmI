using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LevelOpener : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private string LevelName;
    [SerializeField] private UnityEvent OnOpen;
    [SerializeField] private float waittime;
    private bool IsClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(IsClick)
            return;
        if(!MainBookManager._S.interactable)
            return;
        OnOpen?.Invoke();
        BaseLevelManager.INS.StopBGM();
        StartCoroutine(WaitStartLevel());
        IsClick = true;
    }

    IEnumerator WaitStartLevel()
    {
        yield return new WaitForSecondsRealtime(waittime);
        GameManager.INS.StartLevel(LevelName);
    }
}

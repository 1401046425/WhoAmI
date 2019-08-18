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
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!MainBookManager.INS.interactable)
            return;
        OnOpen?.Invoke();
        BaseLevelManager.INS.StopBGM();
        StartCoroutine(WaitStartLevel());
    }

    IEnumerator WaitStartLevel()
    {
        yield return new WaitForSecondsRealtime(waittime);
        GameManager.INS.StartLevel(LevelName);
    }
}

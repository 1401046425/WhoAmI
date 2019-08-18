using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackMenu : MonoBehaviour,IPointerClickHandler
{
    private bool IsQuit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(IsQuit)
            return;
        GameManager.INS.QuitLevel();
        IsQuit = true;
    }
}

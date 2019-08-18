using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ReStart : MonoBehaviour,IPointerClickHandler
{
    private bool IsReStart;
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
        if(IsReStart)
            return;
        GameManager.INS.RestartLastLevel();
        IsReStart = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UIFrameWork;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackMenu : MonoBehaviour,IPointerClickHandler
{
    private bool IsQuit;

    [SerializeField] private string AddLevelName;
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
        UIManager.ShowPanel("FadeInPanel");
        StartCoroutine(WaitToBack());
        IsQuit = true;
    }

    IEnumerator WaitToBack()
    {
        GameManager.INS.UnPauseGame();
        yield return new  WaitForSecondsRealtime(2f);
        if(!string.IsNullOrWhiteSpace(AddLevelName))
            GameManager.INS.AddLevel(AddLevelName);
        GameManager.INS.QuitLevel();
    }
}

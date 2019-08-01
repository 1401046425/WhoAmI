using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIFrameWork.BasePanel;
public class UI_Level : BaseUIPanel
{
    BookPro Book;
    internal override void OnShow()
    {
        UpdateLevelIcon();
    }
    public void UpdateLevelIcon()
    {
        if (GameManager.INS == null)
            return;
        var LevelNumber = GameManager.INS.GetAllUnLockLevelName().Count;
        Book.CurrentPaper = LevelNumber;
        Book.EndFlippingPaper = LevelNumber-1;
    }
    private void Awake()
    {
        Book = GetComponent<BookPro>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

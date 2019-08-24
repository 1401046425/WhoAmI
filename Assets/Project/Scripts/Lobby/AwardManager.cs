using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardManager : MonoBehaviour
{ 
   [SerializeField] private List<Award> AwardsList=new List<Award>();
   public static AwardManager INS;
    [SerializeField] private Transform BackPos;

    private void Awake()
    {
        INS = this;
        UpdateAllAwards();
        OnNewLevelAdd(GameManager.INS.TakeNewAddLevel());
    }

    private Award GetAward(string Name)
    {
        foreach (var VARIABLE in AwardsList)
        {
            if (VARIABLE.LevelName == Name)
                return VARIABLE;
        }

        return null;
    }

    private void OnNewLevelAdd(string LevelName)
    {
        var award = GetAward(LevelName);
        if (award != null)
        {
            award.gameObject.SetActive(true);
            award.transform.position = BackPos.position;
            StartCoroutine(Tween(award.gameObject, award.OrinPos,6f,3f));
        }
    }

    IEnumerator Tween(GameObject Obj,Vector3 TargetPoint,float time,float WaitSecond)
    {
        yield return new WaitForSecondsRealtime(WaitSecond);
        iTween.MoveTo(Obj,TargetPoint,time);
    }
    
    private void UpdateAllAwards()
    {
        var AllUnlockLevelName = GameManager.INS.GetAllUnLockLevelName();
        foreach (var item in AwardsList)
        {
            item.gameObject.SetActive(false);
        }
        foreach (var VARIABLE in AllUnlockLevelName)
        {
           var Award= GetAward(VARIABLE);
           if (Award != null)
           {
               Award.gameObject.SetActive(true);  
           }
        }
    }
}

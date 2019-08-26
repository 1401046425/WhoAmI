using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaderGroup : MonoBehaviour
{
    [SerializeField] List<Fader> Faders=new List<Fader>();

    [SerializeField] private UnityEvent OnFadersFinish;
    // Start is called before the first frame update
    private uint Index;
    void Start()
    {
        if (Faders.Count <= 0)
        {
            foreach (var VARIABLE in transform.GetComponentsInChildren<Fader>())
            {
                Faders.Add(VARIABLE);
            }
        }

        foreach (var VARIABLE in Faders)
        {
            VARIABLE.OnFadeInFinishAction += OnFinishFader;
            VARIABLE.OnFadeOutFinishAction += OnFinishFader;
        }
    }

    public void FadeInAction()
    {
        foreach (var VARIABLE in Faders)
        {
            VARIABLE.FadeInAction();
        }
    }

    public void FadeOutAction()
    {
        foreach (var VARIABLE in Faders)
        {
            VARIABLE.FadeOutAction();
        }
    }

    private void OnFinishFader()
    {
        Index++;
        CheckFaderFinish();
    }

    private void CheckFaderFinish()
    {
        if (Index >= Faders.Count)
        {
            OnFadersFinish?.Invoke();
            Index = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

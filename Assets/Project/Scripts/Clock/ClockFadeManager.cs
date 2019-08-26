using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockFadeManager : MonoBehaviour
{
    [SerializeField] private Clock m_clock;
   [SerializeField] List<Fader> Faders =new List<Fader>();
   [SerializeField] List<int> TargetLenths=new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        m_clock.OnLenthChange += CheckFade;
    }

    private void CheckFade(int obj)
    {
        for (int i = 0; i < Faders.Count; i++)
        {
            if (obj >= TargetLenths[i])
            {
                if(!Faders[i].IsShow)
                    Faders[i].FadeIn();
            }
            else
            {
                if(Faders[i].IsShow)
                    Faders[i].FadeOut();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

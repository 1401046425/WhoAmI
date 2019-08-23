using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckProgress : MonoBehaviour
{
    [SerializeField]
    private EraseProgress Erase_Progress;

    [SerializeField]private int Value;
    private bool IsFinish;
    [SerializeField]private UnityEvent OnProgressFinish;
    // Start is called before the first frame update
    void Start()
    {
        Erase_Progress.OnProgress += OnEraseProgress;
    }

    private void OnEraseProgress(float progress)
    {
        if (Mathf.Round(progress * 100f) >= Value)
        {
            if (!IsFinish)
            {
                OnProgressFinish?.Invoke();
                IsFinish = true;
            }

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

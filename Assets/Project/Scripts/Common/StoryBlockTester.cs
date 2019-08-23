using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryBlockTester : MonoBehaviour
{
    [SerializeField] private TMP_InputField Input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void JumpBlock()
    {
        StoryBlockManager.INS.JumpBlock(int.Parse(Input.text));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

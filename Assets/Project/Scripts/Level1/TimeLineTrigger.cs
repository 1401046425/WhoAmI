using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineTrigger : MonoBehaviour
{
    bool istrigger = false;
    [SerializeField] private PlayableDirector m_playableDirector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (istrigger)
            return;
        if (!m_playableDirector)
            return;
        if (!collision.CompareTag("Player"))
            return;
        m_playableDirector.Play();
        istrigger = true;
    }
}

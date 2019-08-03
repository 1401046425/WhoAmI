using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosChangerTrigger : MonoBehaviour
{
    [SerializeField] private int PlayerIndex;
    [SerializeField]  private Vector2 Pos;
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
        if(collision.transform.CompareTag("Player"))
        PlayerManager.INS.ChangePlayerPos(PlayerIndex, Pos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeTrigger : MonoBehaviour
{
    [SerializeField] private int Index;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.INS.NowPlayerindex == Index)
            return;
        PlayerManager.INS.ChangePlayer(Index);

    }
}

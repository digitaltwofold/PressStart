using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public bool trigger = false;
    public bool back = false;
    PlayerController player;
    private void Start()
    {
        player = GameObject.Find("player").GetComponent<PlayerController>();
        back = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    { //set trigger true if player exits collider
        trigger = true;
        Invoke("SetBack",1);

    }
    
    void SetBack()
    {
        //set trigger bool to false again
        trigger = false;
    }
}

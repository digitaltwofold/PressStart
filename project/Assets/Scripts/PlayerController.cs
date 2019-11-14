using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
   

    // Start is called before the first frame update
    void Start()
    {
       
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float movement = Input.GetAxis("Horizontal") * Time.deltaTime * 10;
        float jump = 0;
        
        if (vertical > 0)
        {
            jump = 5*Time.deltaTime;
            player.GetComponent<Rigidbody2D>().gravityScale = .5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
           
            player.GetComponent<Transform>().transform.localScale = new Vector3(1, .5f, 1);
            player.GetComponent<Rigidbody2D>().gravityScale = 9.81f;

        }
        if(!Input.GetKey(KeyCode.S))
        {

            player.GetComponent<Transform>().transform.localScale = new Vector3(1, 1, 1);
            player.GetComponent<Rigidbody2D>().gravityScale = 3;

        }


        transform.Translate(new Vector2(movement,jump));
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector2.down, -0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //disable render for objects that are outside of camera vision
    void OnBecameInvisible()
    {
        this.gameObject.GetComponent<Renderer>().enabled = false;
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        this.gameObject.GetComponent<Renderer>().enabled = true;
    }
}

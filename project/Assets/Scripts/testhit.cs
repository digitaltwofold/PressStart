using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class testhit : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float camVal = 3f;
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime/2;
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            camVal = Mathf.Lerp(3f, 5f, time);
            
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            camVal = Mathf.Lerp(5f, 3f, time);
            if (camVal == 3f)
            {
                time = 0;

            }
        }
            vcam.m_Lens.OrthographicSize = camVal;
    }
   
}

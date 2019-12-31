using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSound : MonoBehaviour
{
    private void Awake()
    {
        //Do not Destroy sound instance if scene is reloaded, make sure sound stays played until told otherwise
        DontDestroyOnLoad(transform.gameObject);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumCulling : MonoBehaviour
{

    

    // Disable the behaviour when it becomes invisible...
    void OnBecameInvisible()
    {
        enabled = false;
    }

    // ...and enable it again when it becomes visible.
    void OnBecameVisible()
    {
        enabled = true;
    }

}

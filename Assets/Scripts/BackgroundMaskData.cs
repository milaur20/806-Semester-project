using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMaskData : MonoBehaviour
{
    public string backgroundMaskName;

    // Start is called before the first frame update
    void Start()
    {
        //get the name of the parent of this game object
        backgroundMaskName = transform.parent.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

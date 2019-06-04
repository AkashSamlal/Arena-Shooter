﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour {
    public float weaponRange = 50f;
    private Camera fpsCam; 
	
	void Start () {
        fpsCam = GetComponent<Camera>(); 
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.red); 
	}
}

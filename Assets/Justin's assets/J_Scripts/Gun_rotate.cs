﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_rotate : MonoBehaviour
{
    private float offset = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = worldMousePos - this.transform.position;
        direction.Normalize();
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log(rotation_z);
        if (rotation_z > -90 && rotation_z < 90)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, -1, 1);
        }
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z + offset);
    }
}

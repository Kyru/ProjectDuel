﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLeft : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject _ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown("d"))
        {
            _ball = Instantiate(ballPrefab) as GameObject;
            _ball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            _ball.transform.rotation = transform.rotation;

        }
    }
}
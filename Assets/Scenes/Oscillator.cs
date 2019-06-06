﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movmentVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    float movmentFactor;
    Vector3 startingPoint;

    // Use this for initialization
    void Start () {

        startingPoint = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (period<=Mathf.Epsilon) { return; }
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movmentFactor = rawSinWave / 2f+0.5f;
        Vector3 offset = movmentVector * movmentFactor;
        transform.position = startingPoint + offset;
		
	}
}

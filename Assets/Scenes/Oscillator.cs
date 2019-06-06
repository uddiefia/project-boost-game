using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour {
    [SerializeField] Vector3 movmentVector;
    [Range(0, 1)][SerializeField]float movmentFactor;
    Vector3 startingPoint;

    // Use this for initialization
    void Start () {

        startingPoint = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = movmentVector * movmentFactor;
        transform.position = startingPoint + offset;
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidbody;
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}
    private void Rotate()
    {
 
        float rotaionSpeed = rcsThrust * Time.deltaTime;
        rigidbody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
           
            transform.Rotate(Vector3.forward * rotaionSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward *rotaionSpeed);
        }
        rigidbody.freezeRotation = false;

    }

    private void Thrust()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * (mainThrust*Time.deltaTime));
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

        else
        {
            audioSource.Stop();
        }
    }

     void OnCollisionEnter(Collision collision) {

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("friend");
                break;
            case "Fuel":
                print("fuel");
                break;
            default:
                print("dead");
                break;
        }

    }
}

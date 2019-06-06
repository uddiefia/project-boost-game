using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;

    Rigidbody rigidbody;
    AudioSource audioSource;
    enum State{Alive,Dying,Transcending }
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (state== State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        
	}
    private void RespondToRotateInput()
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

    private void RespondToThrustInput()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }

        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }
    private void ApplyThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticle.Play();
    }

     void OnCollisionEnter(Collision collision) {

        if (state != State.Alive)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                
                break;
            case "Finish":
                state = State.Transcending;
                successParticle.Play();
                audioSource.Stop();
                audioSource.PlayOneShot(success);
                Invoke("LoadNesxtScene",1f);
                break;
            default:
                state = State.Dying;
                deathParticle.Play();
                audioSource.Stop();
                audioSource.PlayOneShot(death);
                Invoke("LoadFirstLevel", 1f);
                break;
        }

    }

   private  void LoadNesxtScene()
    {
        SceneManager.LoadScene(1);

    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);

    }
}

using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private GameObject _camera;
    private bool shake = true;
    private const float Walkspeed = 20.0f;
    private const float RotateSpeed = 5f;

    // Use this for initialization
	void Start ()
	{
	    _camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Space))// && shake == true)
        //{
        //    //shake = false;
        //    _camera.SendMessage("CameraShake", new Vector2(1f, 0.5f));
        //}

	    if (Input.GetKey(KeyCode.W))
	    {
	        rigidbody.AddForce(transform.forward * Walkspeed);
	    }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(transform.forward * -Walkspeed);
        }

	    if (Input.GetKey(KeyCode.A))
	    {
	        transform.Rotate(transform.up, -RotateSpeed);
	    }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up, RotateSpeed);
        }

	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Blocker"))
            _camera.SendMessage("CameraShake", new Vector2(1f, 0.5f));
    }
}

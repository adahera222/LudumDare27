using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {
    
    private float DriveSpeed = 20.0f;
    private GameObject _targetGameObject;
    private bool reverse = false;
    private float countdown = 0.5f;
    

    // Use this for initialization
	void Start ()
	{
	    _targetGameObject = GameObject.FindGameObjectWithTag("Player");
	}

    
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(_targetGameObject.transform);

	    if (reverse)
	    {
	        countdown -= Time.deltaTime;
            rigidbody.AddForce(transform.forward * DriveSpeed * -0.5f);

	        if (countdown <= 0)
	        {
	            reverse = false;
	            countdown = 2;
	        }

	    }
	    else
	    {
            rigidbody.AddForce(transform.forward * DriveSpeed);    
	    }
        
        

	}

    public void GameOver()
    {
        DriveSpeed = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
            reverse = true;
    }
}

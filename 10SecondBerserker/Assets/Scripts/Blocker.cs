using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class Blocker : MonoBehaviour {
    
    private float Walkspeed = 20.0f;
    public GameObject TargetGameObject;
    private bool reverse = false;
    private float countdown = 0.5f;
    

    // Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(TargetGameObject.transform);

	    if (reverse)
	    {
	        countdown -= Time.deltaTime;
            rigidbody.AddForce(transform.forward * Walkspeed * -0.5f);

	        if (countdown <= 0)
	        {
	            reverse = false;
	            countdown = 2;
	        }

	    }
	    else
	    {
            rigidbody.AddForce(transform.forward * Walkspeed);    
	    }
        
        

	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Player"))
            reverse = true;
    }
}

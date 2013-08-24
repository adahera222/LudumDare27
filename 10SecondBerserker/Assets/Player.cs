using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private GameObject _camera;
    private bool shake = true;

	// Use this for initialization
	void Start ()
	{
	    _camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.A))// && shake == true)
	    {
	        //shake = false;
            _camera.SendMessage("CameraShake", new Vector2(1f, 0.5f));
	    }
	}
}

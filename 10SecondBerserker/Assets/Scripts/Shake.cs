using System;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Shake : MonoBehaviour
{
    public GameObject Target;
    private float _duration;
    private float _magnitude;
    private Transform _transform;
    private bool _shaking = false;
    private float _shakeTimer = 0;
    private Vector3 _shakeOffset;
    public Transform _cached;


	// Use this for initialization
	void Start ()
	{
	    _transform = this.transform;
        _cached = transform;
	}

    public void CameraShake(Vector2 magdur)
    {
        _magnitude = magdur.x;
        _duration = magdur.y;
        _shaking = true;
        
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (_shaking)
	    {
	        _shakeTimer += Time.deltaTime;

	        if (_shakeTimer >= _duration)
	        {
	            _shakeTimer = 0;
	            _shaking = false;
	        }
	        else
	        {
	            float prog = _shakeTimer/_duration;

	            float magnitude = _magnitude*(1f - (prog*prog));

	            _shakeOffset = new Vector3(NextFloat(), NextFloat(), NextFloat())*magnitude;

	            _transform.position += _shakeOffset;
	            
	        }
	    }

	    _transform.position = Vector3.Slerp(transform.localPosition, new Vector3(0, 35, 0), 10f*Time.deltaTime);

	}

    private float NextFloat()
    {
        var ran = new Random();
        return (float) ran.NextDouble()*2f - 1f;
    }
}

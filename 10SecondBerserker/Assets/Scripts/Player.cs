using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Camera _camera;
    private GameObject _manager;
    private bool shake = true;
    private float driveSpeed = 20.0f;
    private const float RotateSpeed = 5f;
    public Texture2D _healthTexture2D, _backTexture2D;
    private const float MaxHealth = 100;
    private float health = 100;

    // Use this for initialization
	void Start ()
	{
	    _camera = GameObject.FindGameObjectWithTag("MainCamera").camera;
	    _manager = GameObject.FindGameObjectWithTag("manager");
        SetUpTextures();
	}

    void OnGUI()
    {
        var posRelToScreen = _camera.WorldToScreenPoint(transform.position);
        var screenH = Screen.height;
        int offsetx = 20;
        int offsety = 40;
        
        GUI.DrawTexture(new Rect(posRelToScreen.x - offsetx, screenH - (posRelToScreen.y + offsety), _backTexture2D.width, _backTexture2D.height), _backTexture2D);
        if (health >= 0)
        {
            GUI.DrawTexture(
                new Rect(posRelToScreen.x - offsetx, screenH - (posRelToScreen.y + offsety),
                    (health/(float) MaxHealth*_healthTexture2D.width), _healthTexture2D.height), _healthTexture2D);
        }
    }
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(KeyCode.W))
	    {
	        rigidbody.AddForce(transform.forward * driveSpeed);
	    }

        if (Input.GetKey(KeyCode.S))
        {
            rigidbody.AddForce(transform.forward * -driveSpeed);
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

    public void GameOver()
    {
        Debug.Log("Player Game Over");
        driveSpeed = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Blocker"))
        {
            var mag = Vector3.Magnitude(collision.gameObject.rigidbody.velocity);
            _camera.SendMessage("CameraShake", new Vector2(mag*0.5f, 0.5f));

            health -= mag*3;

            if (health <= 0)
                _manager.SendMessage("GameOver");

        }

    }

    void SetUpTextures()
    {
        _healthTexture2D = new Texture2D(50, 5);
        _backTexture2D = new Texture2D(50, 5);

        Color[] fillColorArray = _healthTexture2D.GetPixels();


        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = new Color(0, 255, 0);
        }

        _healthTexture2D.SetPixels(fillColorArray);

        _healthTexture2D.Apply();

        fillColorArray = _healthTexture2D.GetPixels();


        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = new Color(255, 0, 0);
        }

        _backTexture2D.SetPixels(fillColorArray);
        _backTexture2D.Apply();
    }
}

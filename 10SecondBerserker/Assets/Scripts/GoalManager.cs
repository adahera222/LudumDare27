using System.Globalization;
using System.Threading;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour
{

    private int _score;
    public GUIStyle Style;
    private bool gameOver = false;
    public AudioClip clip;
    private float timer = 2;
    private bool scored = false;
    public GUIStyle ScoreStyle;
    private int width, height;

	// Use this for initialization
	void Start ()
	{

	    width = 50;
	    height = 50;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10 , 100, 100), "Score : " +  _score.ToString(CultureInfo.InvariantCulture) , Style);
        if (scored)
            GUI.Label(new Rect(Screen.width / 2 - width, Screen.height / 2 - height, width, height), "G O A L !!!", ScoreStyle);
    }
	
	// Update is called once per frame
	void Update ()
	{

	    if (scored)
	        timer -= Time.deltaTime;

	    if (timer <= 0)
	    {
	        scored = false;
	        timer = 2;
	    }

	}

    public void GameOver()
    {
        gameOver = true;
    }

    public void ResetScore()
    {
        _score = 0;
        gameOver = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag.Equals("ball") && gameOver == false)
        {
            //Debug.Log("Score");
            scored = true;
            audio.PlayOneShot(clip);
            _score++;
            GameObject.FindGameObjectWithTag("manager").SendMessage("Reset", (object)Sceanario.Case.Score);
        }
    }
}

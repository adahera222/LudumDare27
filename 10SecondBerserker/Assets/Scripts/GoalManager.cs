using System.Globalization;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour
{

    private int _score;
    public GUIStyle Style;
    private bool gameOver = false;

	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10 , 100, 100), "Score : " +  _score.ToString(CultureInfo.InvariantCulture) , Style);
    }
	
	// Update is called once per frame
	void Update () {
	
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
            Debug.Log("Score");
            _score++;
            GameObject.FindGameObjectWithTag("manager").SendMessage("Reset", (object)Sceanario.Case.Score);
        }
    }
}

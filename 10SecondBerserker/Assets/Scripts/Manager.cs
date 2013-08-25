using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public GUITexture retryBtn;
    public Texture2D PlayAgainTexture2D, PlayAgainTexture2DRollover;
    public GameObject ball, blocker, player, instBlocker, instPlayer, instBall;
    public GUIStyle textStyle;
    private float start = 10;
    private int width = 36, height = 50;
    private List<GameObject> ballSpawnPoints, blockerSpawnPoints;
    private GameObject playerSpawn;
    private bool gameover = false;
    
    // Use this for initialization
	
    void Start () {
       
        retryBtn.enabled = false;

        DontDestroyOnLoad(this);
	    ballSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("ballspawn"));
        blockerSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("blockerspawn"));
	    playerSpawn = GameObject.FindGameObjectWithTag("playerSpawn");
        
        var ran = new System.Random();
	    int loc = ran.Next(3);
	    instBall = Instantiate(ball, ballSpawnPoints[loc].transform.position, ballSpawnPoints[loc].transform.rotation) as GameObject;
	    loc = ran.Next(3);
        instBlocker = Instantiate(blocker, blockerSpawnPoints[loc].transform.position, blockerSpawnPoints[loc].transform.rotation) as GameObject;
	    instPlayer = Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation) as GameObject;
	}

    void OnGUI()
    {
        
        GUI.Label(new Rect(Screen.width/2 - width/2, Screen.height/2 - height/2, 50, 100), Mathf.RoundToInt(start).ToString(), textStyle );

        
    }

    public void Reset(object sceanario)
    {
        var fix = (Sceanario.Case) sceanario;
        
        Debug.Log("Retry");
        retryBtn.enabled = false;
        
        var ran = new System.Random();
        int loc = ran.Next(3);

        instBall.rigidbody.velocity = new Vector3();
        instBall.transform.position = ballSpawnPoints[loc].transform.position;

        loc = ran.Next(3);

        instBlocker.rigidbody.velocity = new Vector3();
        instBlocker.transform.position = blockerSpawnPoints[loc].transform.position;

        instPlayer.rigidbody.velocity = new Vector3();
        instPlayer.transform.position = playerSpawn.transform.position;

        // Reset Score ???

        // Reset Clock
        start = 10;
        gameover = false;

        // Reset player
        instPlayer.SendMessage("Reset");
        instBlocker.SendMessage("Reset");

        if (fix.Equals(Sceanario.Case.HardReset))
            GameObject.FindGameObjectWithTag("goal").SendMessage("ResetScore");
    }

    public void GameOver()
    {
        gameover = true;
        retryBtn.enabled = true;
        instBlocker.SendMessage("GameOver", SendMessageOptions.RequireReceiver);
        instPlayer.SendMessage("GameOver", SendMessageOptions.RequireReceiver);
        GameObject.FindGameObjectWithTag("goal").SendMessage("GameOver");
    }
	
	// Update is called once per frame
    private void Update()
    {
        if (start >= 0 && gameover != true)
        {
            start -= Time.deltaTime;
        }
        else
        {
            //start = 10;
            GameOver();
        }

        textStyle.normal.textColor = start > 5 ? Color.green : Color.red;
    }
    
}

using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    public GameObject ball, blocker, player, instBlocker, instPlayer;
    public GUIStyle textStyle;
    private float start = 10;
    private int width = 36, height = 50;
    private List<GameObject> ballSpawnPoints, blockerSpawnPoints;
    private GameObject playerSpawn;
    // Use this for initialization
	
    void Start () {
	    ballSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("ballspawn"));
        blockerSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("blockerspawn"));
	    playerSpawn = GameObject.FindGameObjectWithTag("playerSpawn");
        
        var ran = new System.Random();
	    int loc = ran.Next(3);
	    Instantiate(ball, ballSpawnPoints[loc].transform.position, ballSpawnPoints[loc].transform.rotation);
	    loc = ran.Next(3);
        instBlocker = Instantiate(blocker, blockerSpawnPoints[loc].transform.position, blockerSpawnPoints[loc].transform.rotation) as GameObject;
	    instPlayer = Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation) as GameObject;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/2 - width/2, Screen.height/2 - height/2, 50, 100), Mathf.RoundToInt(start).ToString(), textStyle );
    }

    public void GameOver()
    {
        instBlocker.SendMessage("GameOver", SendMessageOptions.RequireReceiver);
        instPlayer.SendMessage("GameOver", SendMessageOptions.RequireReceiver);
    }
	
	// Update is called once per frame
    private void Update()
    {
        if (start >= 0)
        {
            start -= Time.deltaTime;
        }
        else
        {
            start = 10;
        }

        textStyle.normal.textColor = start > 5 ? Color.green : Color.red;
    }
}

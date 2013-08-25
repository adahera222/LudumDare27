using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{

    public Texture2D NormalTexture2D;
    public Texture2D MouseOverTexture2D;
    private Texture2D backgroundTexture2D;
    public bool IsQuitBtn = false;
    public bool IsInstructions = false;
    public bool IsPlayBtn = false;
    public bool IsRetry = false;
    private bool displayInstructions;
    public string LevelToLoad;
    private Rect _instructionMenu = new Rect(10, 10, 400, 400);
    private bool _contains;
    public GUIStyle Style;

	// Use this for initialization
	void Start () {
#if UNITY_WEBPLAYER
	    if (IsQuitBtn)
	        guiTexture.enabled = false;
#endif
        backgroundTexture2D = new Texture2D(400, 400);
	    Style.wordWrap = true;

        Color[] fillColorArray = backgroundTexture2D.GetPixels();


        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = new Color(158, 158, 158, 220);
        }

        backgroundTexture2D.SetPixels(fillColorArray);

        backgroundTexture2D.Apply();

	    Style.normal.background = backgroundTexture2D;
	    Style.normal.textColor = Color.black;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    void OnGUI()
    {
        if (IsInstructions)
        {
            if (displayInstructions)
            {
                
                GUI.Box(_instructionMenu, "Simple: Score a goal in 10 seconds or less, just watch out for the other guy he is bit of a jerk.  \n" +
                                          "see what you can do, be warned though, the pitch is a bit damp.\n\n\n" +
                                          "\n\n\nControls:\nW - Accelerate\nA - Steer Left\nD - Steer Right\nS - Slow\\Reverse\n\n\nClick anywhere in this box to close these instructions.\n\n\nGood Luck!", Style);
                _contains = _instructionMenu.Contains(Event.current.mousePosition);
                //Debug.Log(_contains);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (displayInstructions && _contains)
                    displayInstructions = false;
            }
        }
    }

    void OnMouseEnter()
    {
        guiTexture.texture = MouseOverTexture2D;
    }

    void OnMouseExit()
    {
        guiTexture.texture = NormalTexture2D;
    }

    void OnMouseUp()
    {
        if (IsQuitBtn)
            Application.Quit();
        
        if (IsPlayBtn)
            Application.LoadLevel(LevelToLoad);


        if (IsInstructions)
        {
            displayInstructions = true;
        }

        if (IsRetry)
            GameObject.FindGameObjectWithTag("manager").SendMessage("Reset", (object)Sceanario.Case.HardReset);
        
    }
}

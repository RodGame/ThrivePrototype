using UnityEngine;
using System.Collections;
using System;						//added to access the enum class

//TODO: Make const variable 
//TODO: Make HUD Parameters fonction of the screen.width/height
//TODO: Update Floating function to take a string as input. Would make it more flexible
public class CellHUD : MonoBehaviour {
	public Texture textureATP;
	public Texture textureATPBackground;
	public Texture WhiteTexture;
	
	public int barLength;
	public bool showStatsHUD = true;
	public int leftCol;
	public int topRow;
	public int STATS_LINE_TOP = 40;
	public const int LINE_HEIGHT = 20;
	public const int LABEL_SIZE = 80;
	public const int NUMBER_SIZE = 100;
	public const int STATS_LINE_HEIGHT = 25;
	public const int COL_OFFSET = 150;
	public const int TOGGLE_OFFSET = 125;
	public const int FLOATING_RECT_WIDTH = 200;
	public const int FLOATING_RECT_HEIGHT = 150;	
	public const float STATS_RECT_OPACITY = 0.80f;
	public const float FLOATING_RECT_OPACITY = 0.95f;
	
	private string  _mouseOver;
	private Vector2 _mousePos;
	private Rect    _objRect;
	private int  _curProcessHover;
	private int  _curOrganelleHover;
	
	// Use this for initialization
	void Start () {
		_mousePos = new Vector2(0, 0);
		_objRect = new Rect(0, 0, FLOATING_RECT_WIDTH, FLOATING_RECT_HEIGHT);
		
		barLength   = 200;
		leftCol     = 40;
		topRow      = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Test if a Process is being hovered
		_curProcessHover = -1;
		for(int i = 0; i < Enum.GetValues(typeof(ProcessName)).Length ; i++)
		{
			if(_mouseOver == ("MouseOverOnLabel" + ((ProcessName)i).ToString()))
			{
				_curProcessHover = i;
				//Debug.Log (((ProcessName)i));
			}
		}
		
		// Test if a Process is being hovered
		_curOrganelleHover = -1;
		for(int i = 0; i < Enum.GetValues(typeof(OrganelleName)).Length ; i++)
		{
			if(_mouseOver == ("MouseOverOnLabel" + ((OrganelleName)i).ToString()))
			{
				_curOrganelleHover = i;
			}
		}
	}

	// Display the GUI on screen
	void OnGUI()
	{
		showStatsHUD = GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuHUD>().ShowStatsHUD;
		displayATPBar();
		if(showStatsHUD)
		{
			displayStatsHUD();	
		}
		
		if(_curProcessHover != -1)
		{
			displayFloatingProcess(_curProcessHover);
		}
		
		if(_curOrganelleHover != -1)
		{
			displayFloatingOrganelle(_curOrganelleHover);
		}
	}
	
	// Display a Floating Box at mouse position
	private void displayFloatingProcess(int _ProcessToShow)
	{

		_mousePos = Input.mousePosition;
    	_objRect.x = _mousePos.x;
    	_objRect.y = Mathf.Abs(_mousePos.y - Camera.main.pixelHeight) + LINE_HEIGHT;
			
		// Display semi-transparent background for processes and set color back to white
		GUI.color        = new Color(0.0f, 0.0f, 0.0f, FLOATING_RECT_OPACITY);
		GUI.DrawTexture(_objRect, WhiteTexture);
		GUI.color = Color.white;
		
		// Display Input in the floating Rect
		GUI.Label (new Rect(_objRect.x, _objRect.y                , _objRect.width, _objRect.height), "Inputs : ");
		GUI.Label (new Rect(_objRect.x, _objRect.y + 1*LINE_HEIGHT, _objRect.width, _objRect.height), transform.GetComponent<CellParam>()._Process[_ProcessToShow].CompInput);
		
		// Display Output in the float Rect
		GUI.Label (new Rect(_objRect.x, _objRect.y + 3*LINE_HEIGHT, _objRect.width, _objRect.height), "Outputs : ");
		GUI.Label (new Rect(_objRect.x, _objRect.y + 4*LINE_HEIGHT, _objRect.width, _objRect.height), transform.GetComponent<CellParam>()._Process[_ProcessToShow].CompOutput);
		}
	
	// Display a Floating Box at mouse position
	private void displayFloatingOrganelle(int _OrganelleToShow)
	{

		_mousePos = Input.mousePosition;
    	_objRect.x = _mousePos.x;
    	_objRect.y = Mathf.Abs(_mousePos.y - Camera.main.pixelHeight) + LINE_HEIGHT;
			
		// Display semi-transparent background for processes and set color back to white
		GUI.color        = new Color(0.0f, 0.0f, 0.0f, FLOATING_RECT_OPACITY);
		GUI.DrawTexture(_objRect, WhiteTexture);
		GUI.color = Color.white;
		
		// Display Input in the floating Rect
		GUI.Label (new Rect(_objRect.x, _objRect.y                , _objRect.width, _objRect.height), "This Organelle enable : ");
		GUI.Label (new Rect(_objRect.x, _objRect.y + 1*LINE_HEIGHT, _objRect.width, _objRect.height), "-> " + transform.GetComponent<CellParam>()._Organelle[_OrganelleToShow].EnabledProcess);
		}
	
	// Display ATP Bar on screen
	private void displayATPBar()
	{
		float _ratioATP;
		int _curATP;
		int _maxATP;
		_curATP = transform.GetComponent<CellParam>()._Compound[(int)CompoundName.ATP].CurValue;
		_maxATP = transform.GetComponent<CellParam>()._Compound[(int)CompoundName.ATP].MaxValue;
		_ratioATP = ((float)_curATP / (float)_maxATP);
		GUI.DrawTexture(new Rect(leftCol, 15, barLength, LINE_HEIGHT), textureATPBackground);	
		GUI.DrawTexture(new Rect(leftCol, 17, _ratioATP*barLength, 0.8f*LINE_HEIGHT), textureATP,ScaleMode.ScaleAndCrop);	
		GUI.Label 	   (new Rect(5, 15 , 30 , LINE_HEIGHT),"ATP" );
	}
	
	// Display HUD containing Compound and Process
	private void displayStatsHUD()
	{
		//TODO: Merge toggle and label displays (Need to make sure it works with the floatingBox)
		int _leftStatsHUD = (int)(Screen.width*0.05f);
		bool __toggleBool = false;
		
		// Display semi-transparent background and set color back to white
		#region Display Background
		GUI.color        = new Color(0.0f, 0.0f, 0.0f, STATS_RECT_OPACITY);
		GUI.DrawTexture(new Rect(_leftStatsHUD, 40, Screen.width*0.9f, Screen.height*0.85f), WhiteTexture);
		GUI.color = Color.white;
		#endregion
		
		// Display all Compoud and their values
		#region Display Compound
		GUI.contentColor = Color.white;			// Display the text in white
		for(int i = 0; i < Enum.GetValues(typeof(CompoundName)).Length ; i++)
		{
			GUI.Label (new Rect(_leftStatsHUD                 , 40+(i*STATS_LINE_HEIGHT), LABEL_SIZE, STATS_LINE_HEIGHT) , ((CompoundName)i).ToString () );
			GUI.Label (new Rect(_leftStatsHUD + LABEL_SIZE + 5, 40+(i*STATS_LINE_HEIGHT), NUMBER_SIZE, STATS_LINE_HEIGHT), transform.GetComponent<CellParam>()._Compound[i].CurValue + "/" + transform.GetComponent<CellParam>()._Compound[i].MaxValue  );
		}
		#endregion
		
		// Display Process and grey them out if they are not Available. Add a button to enable or disable the process
		#region Display Process
		GUI.contentColor = Color.white;			// Display the text in white
		for(int i = 0; i < Enum.GetValues(typeof(ProcessName)).Length ; i++)
		{
			
			// Add toggle to activate the prcoesses
			__toggleBool = GUI.Toggle(new Rect(_leftStatsHUD + LABEL_SIZE               + 5 + TOGGLE_OFFSET, 40+(i*STATS_LINE_HEIGHT), STATS_LINE_HEIGHT, STATS_LINE_HEIGHT),transform.GetComponent<CellParam>()._Process[i].Activated,"");
			if((__toggleBool != transform.GetComponent<CellParam>()._Process[i].Activated) && (transform.GetComponent<CellParam>()._Process[i].Available == true))
			{
				transform.GetComponent<CellParam>()._Process[i].Activated = !transform.GetComponent<CellParam>()._Process[i].Activated;	
			}
			
			
			// Change the text to grey if it's not available
			if(transform.GetComponent<CellParam>()._Process[i].Available == false)
			{
				GUI.contentColor = Color.grey;	
			}
			
			//Display the process Name
			GUI.Label (new Rect(_leftStatsHUD + LABEL_SIZE               + 5 + COL_OFFSET, 40+(i*STATS_LINE_HEIGHT), LABEL_SIZE*2, STATS_LINE_HEIGHT), new GUIContent(((ProcessName)i).ToString(),"MouseOverOnLabel" + ((ProcessName)i).ToString()));
	
			_mouseOver = GUI.tooltip;
			GUI.contentColor = Color.white;
		}	
		#endregion
		
		// Display Process and grey them out if they are not Available. Add a button to enable or disable the process
		#region Display Organelle
		GUI.contentColor = Color.white;			// Display the text in white
		for(int i = 0; i < Enum.GetValues(typeof(OrganelleName)).Length ; i++)
		{
			// Change the text to grey if it's not available
			if(transform.GetComponent<CellParam>()._Organelle[i].CurValue < 1)
			{
				GUI.contentColor = Color.grey;	
			}
			
			GUI.Label (new Rect(_leftStatsHUD + 5*LABEL_SIZE + 20    , 40+(i*STATS_LINE_HEIGHT), LABEL_SIZE, STATS_LINE_HEIGHT), new GUIContent(((OrganelleName)i).ToString(),"MouseOverOnLabel" + ((OrganelleName)i).ToString()));
			GUI.Label (new Rect(_leftStatsHUD + 6*LABEL_SIZE + 20 + 5, 40+(i*STATS_LINE_HEIGHT), NUMBER_SIZE, STATS_LINE_HEIGHT), transform.GetComponent<CellParam>()._Organelle[i].CurValue + "/" + transform.GetComponent<CellParam>()._Organelle[i].MaxValue  );
			
			_mouseOver = GUI.tooltip;
			GUI.contentColor = Color.white;			// Display the text in white
		}
		#endregion
	}
}


using UnityEngine;
using System.Collections;

public class MenuHUD : MonoBehaviour {
	
	
	public Texture WhiteTexture;
	
	private GameObject _Cell;
	private float _buttonWidth = 135;
	private float _buttonHeight = 20;
	private float _buttonOffset = 5;
	
	private bool _showEditorHUD  = false;
	private bool _showGodModeHUD = false;
	private bool _showHelpHUD    = false;
	private bool _showStatsHUD   = false;
	
	public const float STATS_RECT_OPACITY = 0.80f;
	
	public bool ShowStatsHUD
	{
		get {return _showStatsHUD; }
		set {_showStatsHUD = value; }
	}
	
	// Use this for initialization
	void Start () 
	{
		_Cell = GameObject.FindGameObjectWithTag("Player") as GameObject;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.R))
		{
			_Cell.GetComponent<CellParam>().IniCompounds();	
		}
		
		if(Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log ("F1 Pressed");
			if(_showStatsHUD == false){ShowNoHUD();}
			_showStatsHUD    = !_showStatsHUD;
		}
		
		if(Input.GetKeyDown(KeyCode.F2))
		{
			if(_showEditorHUD == false){ShowNoHUD();}
			_showEditorHUD    = !_showEditorHUD;
		}
		
		if(Input.GetKeyDown(KeyCode.F3))
		{
			if(_showGodModeHUD == false){ShowNoHUD();}
			_showGodModeHUD    = !_showGodModeHUD;
		}
		
		if(Input.GetKeyDown(KeyCode.F4))
		{
			if(_showHelpHUD == false){ShowNoHUD();}
			ShowNoHUD(); _showHelpHUD    = !_showHelpHUD;
		}
		
	}
	
	void OnGUI()
	{
		
		// Display Top Button
	
		if(GUI.Button (new Rect(Screen.width - 4*(_buttonWidth + _buttonOffset), _buttonOffset, _buttonWidth, _buttonHeight), "F1-Cell Info"      )) {ShowNoHUD(); _showStatsHUD    = !_showStatsHUD;  }
		if(GUI.Button (new Rect(Screen.width - 3*(_buttonWidth + _buttonOffset), _buttonOffset, _buttonWidth, _buttonHeight), "F2-Editor(TODO)"   )) {ShowNoHUD(); _showEditorHUD   = !_showEditorHUD; }
		if(GUI.Button (new Rect(Screen.width - 2*(_buttonWidth + _buttonOffset), _buttonOffset, _buttonWidth, _buttonHeight), "F3-God Mode(TODO)" )) {ShowNoHUD(); _showGodModeHUD  = !_showGodModeHUD;}
		if(GUI.Button (new Rect(Screen.width - 1*(_buttonWidth + _buttonOffset), _buttonOffset, _buttonWidth, _buttonHeight), "F4-HELP!(TODO)"    )) {ShowNoHUD(); _showHelpHUD     = !_showHelpHUD;   }
		
		if(_showEditorHUD)
		{
			DisplayEditorHUD();	
		}
		
		if(_showGodModeHUD)
		{
			DisplayGodModeHUD();	
		}
		
		if(_showHelpHUD)
		{
			DisplayHelpHUD();	
		}
	}
	
	public void ShowNoHUD()
	{
		_showEditorHUD  = false;
		_showGodModeHUD = false;
		_showHelpHUD    = false;
		_showStatsHUD   = false;
	}
	
	public void DisplayEditorHUD()
	{
		_HUDtool_Background();
	}
	
	public void DisplayGodModeHUD()
	{
		_HUDtool_Background();
	}
	
	public void DisplayHelpHUD()
	{
		_HUDtool_Background();
	}
	
	private void _HUDtool_Background()
	{
		int _leftStatsHUD = (int)(Screen.width*0.05f);
		// Display semi-transparent background and set color back to white
		#region Display Background
		GUI.color        = new Color(0.0f, 0.0f, 0.0f, STATS_RECT_OPACITY);
		GUI.DrawTexture(new Rect(_leftStatsHUD, 40, Screen.width*0.9f, Screen.height*0.85f), WhiteTexture);
		GUI.color = Color.white;
		#endregion
	
	}
	
	
	
	
}















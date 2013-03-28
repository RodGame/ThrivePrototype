using UnityEngine;
using System.Collections;

public class Organelle {
	
	private string _name;
	private string _enabledProcess = "";
	private int    _curValue = 1;
	private int    _maxValue = 5;
	
	public string Name
	{
		get {return _name; }
		set {_name = value; }
	}
	
	public string EnabledProcess
	{
		get {return _enabledProcess; }
		set {_enabledProcess = value; }
	}
	
	public int CurValue
	{
		get {return _curValue; }
		set {_curValue = value; }
	}
	
	
	public int MaxValue
	{
		get {return _maxValue; }
		set {_maxValue = value; }
	}
	
}

// Enumeration of all Organelle
public enum OrganelleName {
	Mitochondria,
	Chloroplast,
	Ribosome,
	Vacuole,
	Flagellum,
	Cilia,
	Wall,
}
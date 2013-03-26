using UnityEngine;
using System.Collections;

public class Compound {
	
	private string _name;
	private int _curValue = 0;
	private int _maxValue = 100;
	private bool _limValue = true;
	
	public string Name
	{
		get {return _name; }
		set {_name = value; }
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
	
	public bool LimValue
	{
		get {return _limValue; }
		set {_limValue = value; }
	}
	
}

// Enumeration of all Compound
public enum CompoundName {
	Oxygen,
	Sugar,
	AminoAcids,
	Protein,
	Fat,
	ATP,
	Water,
	CO2,
	Ammonia,
}
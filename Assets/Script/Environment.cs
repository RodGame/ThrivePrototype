using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {
	
	private float _waterOxygenLevel = 0.05f;
	private float _waterCO2Level = 0.05f;
	
	public float WaterOxygenLevel
	{
		get {return _waterOxygenLevel; }
		set {_waterOxygenLevel = value; }
	}
	
	public float WaterCO2Level
	{
		get {return _waterCO2Level; }
		set {_waterCO2Level = value; }
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

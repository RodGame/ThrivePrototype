using UnityEngine;
using System;					  // For Enum class
using System.Collections;
using System.Collections.Generic; // For List class;

public class ProcessFunctions : MonoBehaviour {
	

    private float _elapsedTime;		// Elapsed time since last tick
	private float _timeProcessTick;	// Time of Process Ticks
	
	
	private Process[] _Process; 
	private Compound[] _Compound;

	private struct _CoumpoundsIO
    {
        public int nbr;
        public string type;
    };	
	
	List<_CoumpoundsIO> _curInputs  = new List<_CoumpoundsIO>();
	List<_CoumpoundsIO> _curOutputs = new List<_CoumpoundsIO>();
	
	// Use this for initialization
	void Start () {
		_Process = transform.GetComponent<CellParam>()._Process;
		_Compound = transform.GetComponent<CellParam>()._Compound;
		
		_elapsedTime     = 0.0f; 
		_timeProcessTick = 1.0f;
		
	}
	
	//TODO: Account for the possibility of multiple ticks over a frame
	//TODO: Evaluate if FixedUpdate or something similar would be a better approach. Probably an easier and faster way to do it.
	// Update is called once per frame
	void Update () 
	{
		
		// Update every _timeProcessTick time
		_elapsedTime += Time.deltaTime;			// Add time since last frame to _elapsedTime
		if(_elapsedTime >= _timeProcessTick)	// If _elapsed Time is bigger than a tick
		{
			_elapsedTime -= _timeProcessTick;	// Remove a tick time from _elapsed time to account for time overshoot
			
			// Loop trough process list to evaluate Input/Output equations
			for(int i = 0; i < _Process.Length; i++)
			{
				ProcessFunction(_Process[i]);
			}
		}
	}
	
	//TODO: Implement a better priority function than hardcoding Anerobic Respiration vs Aerobic Respiration
	void ProcessFunction(Process __CurProcess)
	{
		
	if(__CurProcess.Activated == true)
		{
			bool __inputAvailable;
			bool __outputAvailable;
			if(__CurProcess.Name == "AnaerobicResp")
			{
				
				// Anaerobic Respiration is only to be processed if AerobicResp isn't activated OR if oxygen level is under the Need of AerobicRespiration
				if((_Process[(int)ProcessName.AerobicResp].Activated == false) || (_Compound[(int)CompoundName.Oxygen].CurValue < 6)) //TODO: This '6' is a hardcoded value of the Aerobic oxygen input. It shouldn't be hardcoded.
				{
					_curInputs  = parseStringIO(_Process[(int)ProcessName.AnaerobicResp].CompInput);  //PROCESS ANAEROBIC RESPIRATION
					_curOutputs = parseStringIO(_Process[(int)ProcessName.AnaerobicResp].CompOutput);
					__inputAvailable = VerifyInputs(_curInputs);
					__outputAvailable = VerifyOutputs(_curOutputs);
					if(__inputAvailable && __outputAvailable)
					{
						ExecuteProcess (_curInputs, _curOutputs);
					}	
				}
			}
			
			// Execute the Process if it isn't Anerobic Respiration
			if(__CurProcess.Name != "AnaerobicResp")
			{
				ProcessName processIndex = (ProcessName) Enum.Parse(typeof(ProcessName), __CurProcess.Name);  
				_curInputs  = parseStringIO(_Process[(int)processIndex].CompInput);  //PROCESS ANAEROBIC RESPIRATION
				_curOutputs = parseStringIO(_Process[(int)processIndex].CompOutput);
				__inputAvailable = VerifyInputs(_curInputs);
				__outputAvailable = VerifyOutputs(_curOutputs);
				if(__inputAvailable && __outputAvailable)
				{
					ExecuteProcess (_curInputs, _curOutputs);
				}	
			}
		}	
	}
	
	void ExecuteProcess(List<_CoumpoundsIO> __curInputs, List<_CoumpoundsIO> __curOutputs)
	{
		//Loop Inputs and remove them from compounds list
		for(int i = 0; i < __curInputs.Count;i++)
		{
			Debug.Log ("Inputs updated");
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curInputs[i].type);  
			_Compound[(int)compoundIndex].CurValue -= __curInputs[i].nbr;
		}
		
		//Loop Outputs and add them to compounds list
		for(int i = 0; i < __curOutputs.Count;i++)
		{
			Debug.Log ("Outputs updated");
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curOutputs[i].type);  
			_Compound[(int)compoundIndex].CurValue += __curOutputs[i].nbr;
		}
		
	}
	
	bool VerifyInputs(List<_CoumpoundsIO> __curInputs)
	{
		bool   __compoundAvailable = true;
		
		// Verify if each input are available from the compounds list
		for(int i = 0; i < __curInputs.Count;i++)
		{
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curInputs[i].type);  
			
			if(_Compound[(int)compoundIndex].CurValue < __curInputs[i].nbr)
			{
				__compoundAvailable = false;
			}
		}
		return __compoundAvailable;
	}
	
	bool VerifyOutputs(List<_CoumpoundsIO> __curOutputs)
	{
		bool   __compoundAvailable = true;
		// Verify if each input are available from the compounds list
		for(int i = 0; i < __curOutputs.Count;i++)
		{
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curOutputs[i].type);  
			
			//Debug.Log ("Condition 1 : " + (_Compound[(int)compoundIndex].LimValue == true));
			//Debug.Log ("Condition 2 : " + (_Compound[(int)compoundIndex].CurValue + __curOutputs[i].nbr > _Compound[(int)compoundIndex].MaxValue));
			
			if((_Compound[(int)compoundIndex].LimValue == true) && (_Compound[(int)compoundIndex].CurValue + __curOutputs[i].nbr > _Compound[(int)compoundIndex].MaxValue))
			{
				Debug.Log ("Compound Unavailable");
				__compoundAvailable = false;
			}
		}
		return __compoundAvailable;
	}
	
	// This function parse the Compounds Input/Output for the processes. They are delimited by "x" and "+". (i.e : "6xOxygen+1xSugar" and "38xATP+6xWater+6xCO2")
	List<_CoumpoundsIO> parseStringIO(string __string)
	{
		List<_CoumpoundsIO> __curList = new List<_CoumpoundsIO>();
		char __delimiter = '+';
		char __delimiter2 = '*';
		_CoumpoundsIO __ParsedString;
		
		string[] __compoundsString = __string.Split (__delimiter); //Parse the input __string with the __delimiter char
		
			for(int i = 0; i < __compoundsString.Length; i++)
			{
				string[] __nbrAndCompound = __compoundsString[i].Split (__delimiter2); //Parse the input __string with the __delimiter char
				__ParsedString.nbr = int.Parse(__nbrAndCompound[0]);
				__ParsedString.type = __nbrAndCompound[1];
				__curList.Add(__ParsedString);
			}
	return __curList;
	}
	
}

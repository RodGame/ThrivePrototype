using UnityEngine;
using System;					  // For Enum class
using System.Collections;
using System.Collections.Generic; // For List class;

public class ProcessFunctions : MonoBehaviour {
	

    private float _elapsedTime;			// Elapsed time since last tick
	private float _timeProcessTick;		// Time of Process Ticks
	//private float _waterOxygenLevel;	// Level of oxygen in water;
	//private float _waterCO2Level;		// Level of CO2 in water
	
	private Process[] _Process; 
	private Compound[] _Compound;

	private struct _CoumpoundsIO
    {
        public int nbr;
        public string type;
    };	
	
	List<_CoumpoundsIO> _curInputs  = new List<_CoumpoundsIO>();
	List<_CoumpoundsIO> _curOutputs = new List<_CoumpoundsIO>();
	
	
	private GameObject _MicrobeStageManager;
	
	// Use this for initialization
	void Start () {
		
		_MicrobeStageManager = GameObject.FindGameObjectWithTag ("StageManager") as GameObject;
		//_waterOxygenLevel = _MicrobeStageManager.GetComponent<Environment>().WaterOxygenLevel; 
		//_waterCO2Level = _MicrobeStageManager.GetComponent<Environment>().WaterCO2Level;
				
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
			_tickUpdate();						// Do all the function needed on update
			
		}
	}
	
	private void _tickUpdate()
	{
		// Loop trough process list to evaluate Input/Output equations
		for(int i = 0; i < _Process.Length; i++)
		{
			ProcessFunction(_Process[i]);
		}
		
		// Loop trough Compounds list to find the ones to exchange compounds with environment
		for(int i = 0; i < _Compound.Length; i++)
		{
			_ExchangeCompound(_Compound[i]);
		}
	}
	
	//TODO: Prevent exchange when the compound hit his desired value
	// Exhange Compounds with environment at a rate between the MinIntake and the MaxIntake function of the Compound level in the water
	private void _ExchangeCompound(Compound __CurCompound)
	{
		double __randomValue;
			if(__CurCompound.MinIntake != 0.0f || __CurCompound.MaxIntake != 0.0f)
			{
				__randomValue = UnityEngine.Random.Range(__CurCompound.MinIntake, __CurCompound.MaxIntake);
				__CurCompound.CurValue += (int)Math.Round(__randomValue);
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
					_curInputs  = parseStringIO(_Process[(int)ProcessName.AnaerobicResp].CompInput);
					_curOutputs = parseStringIO(_Process[(int)ProcessName.AnaerobicResp].CompOutput);
					__inputAvailable = VerifyInputs(_curInputs);
					__outputAvailable = VerifyOutputs(_curOutputs);
					if(__inputAvailable && __outputAvailable)
					{
						ExecuteProcess (_curInputs, _curOutputs);
					}	
				}
			}
			
			// Execute process that doesn't have priorities management
			if(__CurProcess.Name != "AnaerobicResp")
			{
				ProcessName processIndex = (ProcessName) Enum.Parse(typeof(ProcessName), __CurProcess.Name);  
				_curInputs  = parseStringIO(_Process[(int)processIndex].CompInput);
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
	
	// Execute the process by adding/removing compounds to the compounds list
	void ExecuteProcess(List<_CoumpoundsIO> __curInputs, List<_CoumpoundsIO> __curOutputs)
	{
		//Loop Inputs and remove them from compounds list
		for(int i = 0; i < __curInputs.Count;i++)
		{
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curInputs[i].type);  
			_Compound[(int)compoundIndex].CurValue -= __curInputs[i].nbr;
		}
		
		//Loop Outputs and add them to compounds list
		for(int i = 0; i < __curOutputs.Count;i++)
		{
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curOutputs[i].type);  
			_Compound[(int)compoundIndex].CurValue += __curOutputs[i].nbr;
		}
		
	}
	
	// Verifiy that all Inputs are available for the process to take place
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
	
	// Verify that there are enough space available for the process to take place
	bool VerifyOutputs(List<_CoumpoundsIO> __curOutputs)
	{
		bool   __compoundAvailable = true;
		// Verify if each input are available from the compounds list
		for(int i = 0; i < __curOutputs.Count;i++)
		{
			CompoundName compoundIndex = (CompoundName) Enum.Parse(typeof(CompoundName), __curOutputs[i].type);  
			
			if((_Compound[(int)compoundIndex].LimValue == true) && (_Compound[(int)compoundIndex].CurValue + __curOutputs[i].nbr > _Compound[(int)compoundIndex].MaxValue))
			{
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

/// <summary>
/// RodGame
/// 25Mar13
/// 
/// This script contain the Initialization and Setup of the Organelle, Process and Compounds
/// </summary>

using UnityEngine;
using System.Collections;
using System;						//added to access the enum class

//-->Priority
//TODO: Make compounds slowly move towards their OptValue
//TODO: Inport Coumpound and Process from XML

//-->After
//TODO: Add Organel class
public class CellParam : MonoBehaviour {
	
	
	public Compound[] _Compound;		//Compound class
	public Process[] _Process;			//Process class
	public Organelle[] _Organelle;		//Organelle class
	
	private int _moveCostATP;				//ATP cost whenever a key is pressed and force is added to the cell
	
	// Initialize Compounds Max and Current Values
	private int _atpMaxValueINI;
	private int _atpCurValueINI;	
	private int _oxygenMaxValueINI;
	private int _oxygenCurValueINI;
	private int _sugarMaxValueINI;
	private int _sugarCurValueINI;
	private int _aminoAcidsMaxValueINI;
	private int _aminoAcidsCurValueINI;
	private int _proteinMaxValueINI;
	private int _proteinCurValueINI;
	private int _fatMaxValueINI;
	private int _fatCurValueINI;
	private int _waterMaxValueINI;
	private int _waterCurValueINI;
	private int _co2MaxValueINI;
	private int _co2CurValueINI;	
	
	void Awake()
	{
		
		_moveCostATP = 1;
		
		// Initialize Compound values
		_atpCurValueINI = 500;		
		_atpMaxValueINI = 1000;
		_oxygenCurValueINI = 300;		
		_oxygenMaxValueINI = 500;
		_sugarCurValueINI = 15;
		_sugarMaxValueINI = 50;
		_aminoAcidsCurValueINI = 50;
		_aminoAcidsMaxValueINI = 100;
		_proteinCurValueINI = 200;
		_proteinMaxValueINI = 500;
		_fatCurValueINI = 200;
		_fatMaxValueINI = 500;
		_waterCurValueINI = 200;
		_waterMaxValueINI = 500;
		_co2CurValueINI = 50;	
		_co2MaxValueINI = 100;
		
		// Initialize and setup of classes
		_Compound   = new Compound [Enum.GetValues(typeof(CompoundName )).Length];	
		_Process    = new Process  [Enum.GetValues(typeof(ProcessName  )).Length];
		_Organelle  = new Organelle[Enum.GetValues(typeof(OrganelleName)).Length];
		_SetupOrganelle();
		_SetupProcess();
		_SetupCompounds();
	}
	
	
	// Initliaze the Organelle with Initial Values
	public void IniOrganelle()
	{
		for(int i = 0; i < _Organelle.Length; i++)
		{
			_Organelle[i] = new Organelle();
			_Organelle[i].Name = ((OrganelleName)i).ToString();
			if(_Organelle[i].Name == "Mitochondria")
			{ 
				_Organelle[i].EnabledProcess  = "AerobicResp";
				_Organelle[i].CurValue  = 1;
			}
			
			else if(_Organelle[i].Name == "Chloroplast")
			{ 
				_Organelle[i].EnabledProcess  = "Photosynthesis";
				_Organelle[i].CurValue  = 0;
			}
			
			else if(_Organelle[i].Name == "Ribosome")
			{ 
				_Organelle[i].EnabledProcess  = "";
				_Organelle[i].CurValue  = 0;
			}
			
			else if(_Organelle[i].Name == "Ribosome")
			{ 
				_Organelle[i].EnabledProcess  = "";
				_Organelle[i].CurValue  = 0;
			}
			
			else if(_Organelle[i].Name == "Vacuole")
			{ 
				_Organelle[i].EnabledProcess  = "";
				_Organelle[i].CurValue  = 0;
			}
			
			else if(_Organelle[i].Name == "Flagellum")
			{ 
				_Organelle[i].EnabledProcess  = "";
				_Organelle[i].CurValue  = 0;
			}
			
			else if(_Organelle[i].Name == "Cilia")
			{ 
				_Organelle[i].EnabledProcess  = "";
				_Organelle[i].CurValue  = 0;
			}
			
			else if(_Organelle[i].Name == "Wall")
			{ 
				_Organelle[i].EnabledProcess  = "";
				_Organelle[i].CurValue  = 0;
			}
			
		}	
	}
	
	// Initliaze the Process with Initial Values
	public void IniProcess()
	{
		for(int i = 0; i < _Process.Length; i++)
		{
			_Process[i] = new Process();
			_Process[i].Name = ((ProcessName)i).ToString();
			if(_Process[i].Name == "AnaerobicResp")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = true;
				_Process[i].CompInput  = "1*Sugar";
				_Process[i].CompOutput = "18*ATP+3*CO2";
			}
			else if(_Process[i].Name == "AerobicResp")
			{
				_Process[i].Available  = false;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "6*Oxygen+1*Sugar";
				_Process[i].CompOutput = "38*ATP+6*Water+6*CO2";
			}
			
			else if(_Process[i].Name == "FattyAcidResp")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "6*Oxygen+1*Fat";
				_Process[i].CompOutput = "50*ATP+6*Water+6*CO2";
			}
			
			else if(_Process[i].Name == "AminoAcidResp")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "6*Oxygen+1*AminoAcids";
				_Process[i].CompOutput = "60*ATP+6*Water+6*CO2+6*Ammonia";
			}
			else if(_Process[i].Name == "Photosynthesis")
			{
				_Process[i].Available  = false;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "4*CO2+6*Water+6*Light(Not implemented)";
				_Process[i].CompOutput = "5*Sugar+6*Oxygen";
			}
			else if(_Process[i].Name == "ProteinCatab")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "6*Protein";
				_Process[i].CompOutput = "4*AminoAcids";
			}
			else if(_Process[i].Name == "AminoAcidBiosynth")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "5*Sugar+2*Ammonia";
				_Process[i].CompOutput = "5*CO2+2*ATP+2*AminoAcids";
			}
			else if(_Process[i].Name == "FattyAcidSynth")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "5*AminoAcids";
				_Process[i].CompOutput = "6*Ammonia+2*Fat";
			}
			else if(_Process[i].Name == "ProteinSynth")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = false;
				_Process[i].CompInput  = "6*AminoAcids";
				_Process[i].CompOutput = "4*Protein";
			}	
		}
	}
	
	// Initliaze the Compounds with Initial Values
	public void IniCompounds()
	{
	for(int i = 0; i < _Compound.Length; i++)
		{
			_Compound[i] = new Compound();
			_Compound[i].Name = ((CompoundName)i).ToString();
			
			// Assign Max and Current values to the coumpound array
			if(_Compound[i].Name == "ATP")
			{
				_Compound[i].CurValue = _atpCurValueINI;	
				_Compound[i].MaxValue = _atpMaxValueINI;
			}
			else if(_Compound[i].Name == "Oxygen")
			{
				_Compound[i].CurValue = _oxygenCurValueINI;	
				_Compound[i].MaxValue = _oxygenMaxValueINI;
				_Compound[i].LimValue = false;
				_Compound[i].MinIntake = 0.5f;
				_Compound[i].MaxIntake = 1.5f;
			}
			else if(_Compound[i].Name == "Sugar")
			{
				_Compound[i].CurValue = _sugarCurValueINI;	
				_Compound[i].MaxValue = _sugarMaxValueINI;
			}
			else if(_Compound[i].Name == "AminoAcids")
			{
				_Compound[i].CurValue = _aminoAcidsCurValueINI;	
				_Compound[i].MaxValue = _aminoAcidsMaxValueINI;
			}
			else if(_Compound[i].Name == "Protein")
			{
				_Compound[i].CurValue = _proteinCurValueINI;	
				_Compound[i].MaxValue = _proteinMaxValueINI;
			}
			else if(_Compound[i].Name == "Fat")
			{
				_Compound[i].CurValue = _fatCurValueINI;	
				_Compound[i].MaxValue = _fatMaxValueINI;
			}
			else if(_Compound[i].Name == "Water")
			{
				_Compound[i].CurValue = _waterCurValueINI;	
				_Compound[i].MaxValue = _waterMaxValueINI;
				_Compound[i].LimValue = false;
				_Compound[i].MinIntake = 0.0f;
				_Compound[i].MaxIntake = 1.0f;
			}
			else if(_Compound[i].Name == "CO2")
			{
				_Compound[i].CurValue = _co2CurValueINI;	
				_Compound[i].MaxValue = _co2MaxValueINI;
				_Compound[i].LimValue = false;
				_Compound[i].MinIntake = -1.0f;
				_Compound[i].MaxIntake = -2.0f;
			}		
			else if(_Compound[i].Name == "Ammonia")
			{
				_Compound[i].CurValue = _co2CurValueINI;	//TO update
				_Compound[i].MaxValue = _co2MaxValueINI;	//TO update
				_Compound[i].LimValue = false;
			}
		}	
	}
	

	// Setup the Organelle system
	private void _SetupOrganelle()
	{
		IniOrganelle();
	}	
	
	// Setup the Organelle system
	private void _SetupProcess()
	{
		IniProcess ();
		UpdateProcess();
	}	
	
	// Setup the Organelle system
	private void _SetupCompounds()
	{
		IniCompounds();
	}
	
	
	// Update the process list according to the organelle list
	public void UpdateProcess()
	{
		for(int i = 0; i < _Organelle.Length; i++)
		{
			if(_Organelle[i].EnabledProcess != "" && _Organelle[i].CurValue > 0)
			{
				//ProcessName processIndex = (ProcessName) Enum.Parse(typeof(ProcessName), __CurProcess.Name);  
				Debug.Log (_Organelle[i].EnabledProcess);
				ProcessName __processToEnableIndex = (ProcessName) Enum.Parse(typeof(ProcessName), _Organelle[i].EnabledProcess);  
				_Process[(int)__processToEnableIndex].Available = true;
			}
		}
	}
	
	// Reduce ATP ressource whenever cell has moved
	public void cellMoved()
	{
		_Compound[(int)CompoundName.ATP].CurValue -= _moveCostATP;
	}

}

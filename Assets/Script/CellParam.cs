/// <summary>
/// RodGame
/// 25Mar13
/// 
/// This script contains initialize the Process and Compounds class for the cell.
/// It also give initial values to each of them
/// </summary>

using UnityEngine;
using System.Collections;
using System;						//added to access the enum class

//-->Priority
//TODO: Make compounds slowly move towards their OptValue
//TODO: Use process to do conversion to ATP
//TODO: Inport Coumpound and Process from XML

//-->After
//TODO: Add Organel class
public class CellParam : MonoBehaviour {
	
	public int moveCostATP;				//ATP cost whenever a key is pressed and force is added to the cell
	
	public Compound[] _Compound;		//Compound class
	public Process[] _Process;			//Process class
	
	// Initialize Compounds Max and Current Values
	private int _atpMaxValue;
	private int _atpCurValue;	
	private int _oxygenMaxValue;
	private int _oxygenCurValue;
	private int _sugarMaxValue;
	private int _sugarCurValue;
	private int _aminoAcidsMaxValue;
	private int _aminoAcidsCurValue;
	private int _proteinMaxValue;
	private int _proteinCurValue;
	private int _fatMaxValue;
	private int _fatCurValue;
	private int _waterMaxValue;
	private int _waterCurValue;
	private int _co2MaxValue;
	private int _co2CurValue;	
	
	void Awake()
	{
		// Initialize Compound values
		 _atpCurValue = 1000;		
		 _atpMaxValue = 1000;
		 _oxygenCurValue = 350;		
		 _oxygenMaxValue = 500;
		 _sugarCurValue = 15;
		 _sugarMaxValue = 50;
		 _aminoAcidsCurValue = 0;
		 _aminoAcidsMaxValue = 100;
		 _proteinCurValue = 500;
		 _proteinMaxValue = 500;
		 _fatCurValue = 500;
		 _fatMaxValue = 500;
		 _waterCurValue = 250;
		 _waterMaxValue = 500;
		 _co2CurValue = 0;	
		 _co2MaxValue = 100;
		
		// Initialize classes and setup them
		_Compound = new Compound[Enum.GetValues(typeof(CompoundName)).Length];	
		_Process  = new Process[Enum.GetValues(typeof(ProcessName)).Length];
		SetupCompounds();
		SetupProcess();

		

		
	}
	
	//Initialize the process array and give values to Compounds parameters
	public void SetupCompounds()
	{
		for(int i = 0; i < _Compound.Length; i++)
		{
			_Compound[i] = new Compound();
			_Compound[i].Name = ((CompoundName)i).ToString();
			
			// Assign Max and Current values to the coumpound array
			if(_Compound[i].Name == "ATP")
			{
				_Compound[i].CurValue = _atpCurValue;	
				_Compound[i].MaxValue = _atpMaxValue;
			}
			else if(_Compound[i].Name == "Oxygen")
			{
				_Compound[i].CurValue = _oxygenCurValue;	
				_Compound[i].MaxValue = _oxygenMaxValue;
			}
			else if(_Compound[i].Name == "Sugar")
			{
				_Compound[i].CurValue = _sugarCurValue;	
				_Compound[i].MaxValue = _sugarMaxValue;
			}
			else if(_Compound[i].Name == "AminoAcids")
			{
				_Compound[i].CurValue = _aminoAcidsCurValue;	
				_Compound[i].MaxValue = _aminoAcidsMaxValue;
			}
			else if(_Compound[i].Name == "Protein")
			{
				_Compound[i].CurValue = _proteinCurValue;	
				_Compound[i].MaxValue = _proteinMaxValue;
			}
			else if(_Compound[i].Name == "Fat")
			{
				_Compound[i].CurValue = _fatCurValue;	
				_Compound[i].MaxValue = _fatMaxValue;
			}
			else if(_Compound[i].Name == "Water")
			{
				_Compound[i].CurValue = _waterCurValue;	
				_Compound[i].MaxValue = _waterMaxValue;
			}
			else if(_Compound[i].Name == "CO2")
			{
				_Compound[i].CurValue = _co2CurValue;	
				_Compound[i].MaxValue = _co2MaxValue;
			}			
		}
	}
	
	//Initialize the process array and give values to process parameters
	public void SetupProcess()
	{
		for(int i = 0; i < _Process.Length; i++)
		{
			_Process[i] = new Process();
			_Process[i].Name = ((ProcessName)i).ToString();
			if(_Process[i].Name == "AnaerobicResp")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = true;
				_Process[i].CompInput  = "1xSugar";
				_Process[i].CompOutput = "1xCO2+18xATP";
			}
			
			if(_Process[i].Name == "AerobicResp")
			{
				_Process[i].Available  = true;
				_Process[i].Activated  = true;
				_Process[i].CompInput  = "6xOxygen+Sugar";
				_Process[i].CompOutput = "38xATP+6xWater+6xCO2";
			}
		}
	}	
	
	// Use this for initialization
	void Start () {
		//_Compound[(int)CompoundName.ATP].MaxValue = 1000;
		//_Compound[(int)CompoundName.ATP].CurValue = _Compound[(int)CompoundName.ATP].MaxValue;
		moveCostATP = 1;
	}
	
	// Reduce ATP ressource whenever cell has moved
	public void cellMoved()
	{
		_Compound[(int)CompoundName.ATP].CurValue -= moveCostATP;
	}
	
	// Add inputed amount of ATP to the cell
	public void GainATP(int _ATPGained)
	{
		_Compound[(int)CompoundName.ATP].CurValue += _ATPGained;
		if(_Compound[(int)CompoundName.ATP].CurValue > _Compound[(int)CompoundName.ATP].MaxValue)
		{
			_Compound[(int)CompoundName.ATP].CurValue = _Compound[(int)CompoundName.ATP].MaxValue;
		}
	}
}

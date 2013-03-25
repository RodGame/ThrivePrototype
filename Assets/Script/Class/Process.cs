using UnityEngine;
using System.Collections;

public class Process {
	
	private string _name;
	private bool _available = false;
	private bool _activated = false;
	private string _compInput;
	private string _compOutput;
	
	public string Name
	{
		get {return _name; }
		set {_name = value; }
	}
	
	public bool Available
	{
		get {return _available; }
		set {_available = value; }
	}
	
	public string CompInput
	{
		get {return _compInput; }
		set {_compInput = value; }
	}
	
	public string CompOutput
	{
		get {return _compOutput; }
		set {_compOutput = value; }
	}
	
	public bool Activated
	{
		get {return _activated; }
		set {_activated = value; }
	}

}

// Enumeration of all Process
public enum ProcessName {
	AnaerobicResp, // = 0,
	AerobicResp,   // = 1..
	FattyAcidResp,
	AminoAcidResp,
	Photosynthesis,
	ProteinCatab,
	AminoAcidBiosynth,
	FattyAcidSynth,
	ProteinSynth
}
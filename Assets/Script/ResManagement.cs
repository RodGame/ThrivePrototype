using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For List

public class ResManagement : MonoBehaviour {
	
	public GameObject resGlucose;
	public List<GameObject> resList; 
	
	public int radiusSpawnRes; 
	public int nbrSugarToCreate;	
	public int nbrSugarInSoup;
	
	// Use this for initialization
	void Awake () {
		nbrSugarToCreate = 50;
		radiusSpawnRes = 10;
	}
	
	void Start () 
	{
		resList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		while(nbrSugarInSoup < nbrSugarToCreate)
		{
			CreateRes("Sugar");
		}
	}
			
	// Instantiate a glucose prefab on the map
	void CreateRes(string _resType)
	{
		GameObject _curGameObject;
		Vector3 _resPos;
		_resPos = Random.insideUnitSphere*radiusSpawnRes;
		_resPos.y = resGlucose.transform.localScale.y/2;
		_curGameObject = Instantiate(resGlucose, _resPos, Quaternion.identity) as GameObject;
		nbrSugarInSoup++;
	}
}

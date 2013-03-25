using UnityEngine;
using System.Collections;
using System.Collections.Generic; // For List

public class ResManagement : MonoBehaviour {
	
	public GameObject resGlucose;
	public List<GameObject> resList; 
	
	public int nbrRes;
	public int radiusSpawnRes; 
	
	
	
	
	// Use this for initialization
	void Awake () {
		nbrRes = 100;
		radiusSpawnRes = 50;
	}
	
	void Start () 
	{
		resList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		while(resList.Count <= nbrRes)
		{
			CreateRes("Glucose");
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
		_curGameObject.name = "Res_Glucose";
		resList.Add (_curGameObject);
	}
}

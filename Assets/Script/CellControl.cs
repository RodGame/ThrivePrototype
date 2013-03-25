using UnityEngine;
using System.Collections;

public class CellControl : MonoBehaviour {
	
	public Vector3 newCellPosition;
	//public GameObject cell;
	public Transform cellCamera;
	public float cellSpeed;
	public float zoomSpeed;
	public int camUpperLimit;
	public int camLowerLimit;
		
	// Use this for initialization
	void Start () {
		//cell = GameObject.FindGameObjectWithTag("Player");
		cellCamera = transform.FindChild ("Camera");
		
		camUpperLimit = 1000 / 10 / 2; //Division by 10 is to convert it in unity unit. /2 reason is need for upper limit
		camLowerLimit = 30 	/ 10;	  //Division by 10 is to 
		
		
		cellSpeed = 2.5f;
		zoomSpeed = 1;
		newCellPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		getKeyboardInput();
		getMouseInput();
	}
	
	void getKeyboardInput()
	{
		int _curATP;
		_curATP = transform.GetComponent<CellParam>()._Compound[(int)CompoundName.ATP].CurValue;
		if(Input.GetKey (KeyCode.W) && _curATP > 0) 		// zForward
		{
			transform.rigidbody.AddForce(new Vector3(0, 0, cellSpeed));
			transform.GetComponent<CellParam>().cellMoved();
		}
		
		if(Input.GetKey (KeyCode.S) && _curATP > 0) 		// zBackward
		{
			transform.rigidbody.AddForce(new Vector3(0, 0, -cellSpeed));
			transform.GetComponent<CellParam>().cellMoved();
		}
		
		if(Input.GetKey (KeyCode.D)&& _curATP > 0) 		// xForward
		{
			transform.rigidbody.AddForce(new Vector3(cellSpeed, 0, 0));	
			transform.GetComponent<CellParam>().cellMoved();
		}
		
		if(Input.GetKey (KeyCode.A)&& _curATP > 0) 		// xBackward
		{
			transform.rigidbody.AddForce(new Vector3(-cellSpeed, 0, 0));	
			transform.GetComponent<CellParam>().cellMoved();
		}
		
		if(Input.GetKeyDown (KeyCode.Q)) 			    // Open cell stats
		{
			transform.GetComponent<CellHUD>().switchStatsHUD();
		}
		
	}
	
	void getMouseInput()
	{
		if(Input.GetAxis("Mouse ScrollWheel") < 0 && cellCamera.camera.transform.position.y < camUpperLimit) //Zoom in
		{
			
			cellCamera.transform.position = new Vector3(cellCamera.camera.transform.position.x, (int)(cellCamera.camera.transform.position.y + zoomSpeed), cellCamera.camera.transform.position.z);
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") > 0 && cellCamera.camera.transform.position.y > camLowerLimit) //Zoom out
		{
			cellCamera.transform.position = new Vector3(cellCamera.camera.transform.position.x, (int)(cellCamera.camera.transform.position.y - zoomSpeed), cellCamera.camera.transform.position.z);
		}
		
	
	}
	
    void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
		if(other.name == "Res_Glucose")
		{
			transform.GetComponent<CellParam>().GainATP (16);
		}
		
    }
}

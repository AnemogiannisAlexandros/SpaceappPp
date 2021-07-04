using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangePlanetType : MonoBehaviour {

    // Use this for initialization
    void Awake() 
    {
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
        Debug.Log("Running");
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Clicked");
        }
	}
}

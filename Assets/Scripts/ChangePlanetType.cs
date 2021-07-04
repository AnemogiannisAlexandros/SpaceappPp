using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralPlanets;
public class ChangePlanetType : MonoBehaviour {

    SolidPlanet planet;
    // Use this for initialization
    void Awake() 
    {
        planet = GetComponent<SolidPlanet>();
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
        Debug.Log("Running");
        if (Input.GetMouseButtonDown(0)) 
        {
            Debug.Log("Clicked");
            planet.SetPlanetBlueprint(Random.Range(0,6));
        }
	}
}

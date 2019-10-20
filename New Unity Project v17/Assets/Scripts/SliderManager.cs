using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderManager : MonoBehaviour {

    PlanetCreator create;
	// Use this for initialization
	void Start () {
        create = FindObjectOfType<PlanetCreator>();
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (create.GetCurrentPlanet())
        {
            create.GetCurrentPlanet().GetComponentInChildren<Ellipse>().SetMajorAxisLength(GetComponent<Slider>().value);
        }    
    }
}

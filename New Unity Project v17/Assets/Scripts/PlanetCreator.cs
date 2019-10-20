using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ProceduralPlanets;

public class PlanetCreator : MonoBehaviour
{
    public Vector3 creationPosition;
    public float ellipseDiameter;
    [Range(0, 1)]
    public float eccentricity;
    public List<GameObject> planets;
    public void Start() 
    {
        planets = new List<GameObject>();
    }
    public void InstantiatePlanet()
    {
        GameObject go = new GameObject();
        PlanetManager.CreatePlanet(creationPosition).gameObject.transform.SetParent(go.transform);
        go.AddComponent<Ellipse>().SetupData(Ellipse.MajorAxis.AxisX,0.5f,200,23,80);
        go.AddComponent<OrbitMotion>();
        go.AddComponent<OrbitRenderer>();
        go.AddComponent<PlanetData>();
        planets.Add(go);
    }

    public GameObject GetCurrentPlanet() 
    {

        if (planets.Count > 0)
        {
            return planets[planets.Count - 1];
        }
        return null;
    }
}

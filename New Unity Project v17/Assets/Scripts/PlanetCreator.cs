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

    public void InstantiatePlanet()
    {
        GameObject go = new GameObject();
        PlanetManager.CreatePlanet(creationPosition).gameObject.transform.SetParent(go.transform);
        go.AddComponent<Ellipse>();
        go.AddComponent<OrbitMotion>();
        go.AddComponent<OrbitRenderer>();

    }

}

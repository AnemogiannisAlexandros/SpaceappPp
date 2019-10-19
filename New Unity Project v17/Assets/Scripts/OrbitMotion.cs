using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

    public Transform orbitingObject;
    public Ellipse orbitPath;

    [Range(0f, 1f)]
    public float orbitProgress = 0;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    void Awake()
    {
        orbitingObject = transform.GetChild(0);
        orbitPath = GetComponent<Ellipse>();
    }
    // Use this for initialization
    void Start()
    {
        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();
    }
    void Update()
    {
        AnimteOrbit();
    }
    void SetOrbitingObjectPosition()
    {
        Vector2 orbitPos = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPos.x, 0, orbitPos.y);
    }

    void AnimteOrbit()
    {

        float orbitSpeed = 1f / orbitPeriod;
        if (orbitActive)
        {
            orbitProgress +=Mathf.Abs(Time.deltaTime * orbitSpeed);
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse : MonoBehaviour
{
    public MajorAxis axis;
    private OrbitMotion motion;
    public float majorAxisLength;
    private float xAxis, yAxis;
    private float focalParameter;
    private float angleTheta;
    private float distance;
    private float SunPullForce;
    private float angularVelocity;
    private float axialTilt;
    private float rotationSpeed;
    public GameObject Sun;
    [Range(0,1)]
    public float eccentricity;
    private float gamma;
    public enum MajorAxis
    {
        AxisX,
        AxisY
    };
    public Ellipse(float xAxis, float yAxis)
    {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }
    public Ellipse(MajorAxis axis,float ecentricity,float majorAxisLength,float axialTilt,float RotaionSpeed) 
    {
        this.axis = axis;
        this.eccentricity = ecentricity;
        this.majorAxisLength = majorAxisLength;
        this.axialTilt = axialTilt;
        this.rotationSpeed = RotaionSpeed;
    }
    public void SetupData(MajorAxis axis, float ecentricity, float majorAxisLength, float axialTilt, float RotaionSpeed)
    {
        this.axis = axis;
        this.eccentricity = ecentricity;
        this.majorAxisLength = majorAxisLength;
        this.axialTilt = axialTilt;
        this.rotationSpeed = RotaionSpeed;
    }
        public Vector2 Evaluate(float t)
    {
   

        float angle = Mathf.Deg2Rad * 360 * t;
        float x = Mathf.Cos(angle) * xAxis;
        float y = Mathf.Sin(angle) * yAxis;
        return new Vector2(x, y);
    }
    
	// Use this for initialization
	void Awake ()
    {
        
        if (eccentricity != 0)
        {
            if (axis == MajorAxis.AxisX)
            {
                xAxis = majorAxisLength / 2;
                gamma = eccentricity * xAxis;
                yAxis = Mathf.Sqrt(Mathf.Pow(xAxis, 2) - Mathf.Pow(gamma, 2));
                focalParameter = xAxis / eccentricity - gamma;
            }
            else
            {
                yAxis = majorAxisLength / 2;
                gamma = eccentricity * yAxis;
                xAxis = Mathf.Sqrt(Mathf.Pow(yAxis, 2) - Mathf.Pow(gamma, 2));
                focalParameter = yAxis / eccentricity - gamma;
            }
        }
        else
        {
            xAxis = majorAxisLength / 2;
            yAxis = majorAxisLength / 2;
            gamma = xAxis * eccentricity;
        }

    }
    void Start() 
    {
        motion = GetComponent<OrbitMotion>();
        transform.eulerAngles = new Vector3(0, 0, axialTilt);
    }
	// Update is called once per frame
	void Update ()
    {
        if (eccentricity != 0)
        {
            if (axis == MajorAxis.AxisX)
            {
                xAxis = majorAxisLength / 2;
                gamma = eccentricity * xAxis;
                yAxis = Mathf.Sqrt(Mathf.Pow(xAxis, 2) - Mathf.Pow(gamma, 2));
                focalParameter = (xAxis / eccentricity) - gamma;
            }
            else
            {
                yAxis = majorAxisLength / 2;
                gamma = eccentricity * yAxis;
                xAxis = Mathf.Sqrt(Mathf.Pow(yAxis, 2) - Mathf.Pow(gamma, 2));
                focalParameter = (yAxis / eccentricity) - gamma;
            }
        } else
        {
            xAxis = majorAxisLength/2;
            yAxis = majorAxisLength/2;
            focalParameter = (yAxis / eccentricity) - gamma;
        }

        angleTheta = 360 * motion.orbitProgress;
        distance = Vector3.Distance(transform.GetChild(0).position, Sun.transform.position);

        SunPullForce = ((6.6742f * Mathf.Pow(10, -11)*GetComponent<PlanetData>().Mass * Sun.GetComponent<PlanetData>().Mass)/Mathf.Pow(distance,2));
        transform.GetChild(0).Rotate(new Vector3(0, axialTilt,0).normalized, rotationSpeed * Time.fixedDeltaTime);
       // angularVelocity = Mathf.Sqrt(Mathf.Pow(SunPullForce, 2)+Mathf.Pow());

    }
    public float SetMajorAxisLength(float length) 
    {
        this.majorAxisLength = length;
        return this.majorAxisLength;
    }
    public float SetEccentricity(float range)
    {
        if (range >= 0 && range <= 1) 
        {
            this.eccentricity = range;
            return this.eccentricity;
        }
        return this.eccentricity;
    }
    public MajorAxis SetNewAxis(MajorAxis axis) 
    {
        this.axis = axis;
        return this.axis;
    }
    public float SetAxialTilt(float degrees) 
    {
        this.axialTilt = degrees;
        return this.axialTilt;
    }
    public float SetRotationSpeed(float speed) 
    {
        this.rotationSpeed = speed;
        return this.rotationSpeed;
    }
    public void SetOrbitalPeriod(float period) 
    {
        GetComponent<OrbitMotion>().orbitPeriod = period;
    }
    public void SetInMotion(bool active) 
    {
        GetComponent<OrbitMotion>().orbitActive = active;
    }
}

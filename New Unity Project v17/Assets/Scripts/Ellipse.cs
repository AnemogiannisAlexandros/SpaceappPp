using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse : MonoBehaviour
{
    public MajorAxis axis;
    public float majorAxisLength;
    private float xAxis, yAxis;
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
    public Vector2 Evaluate(float t)
    {
   

        float angle = Mathf.Deg2Rad * 360 * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
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
            }
            else
            {
                yAxis = majorAxisLength / 2;
                gamma = eccentricity * yAxis;
                xAxis = Mathf.Sqrt(Mathf.Pow(yAxis, 2) - Mathf.Pow(gamma, 2));
            }
        }
        else
        {
            xAxis = majorAxisLength / 2;
            yAxis = majorAxisLength / 2;
            gamma = xAxis * eccentricity;
        }

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
            }
            else
            {
                yAxis = majorAxisLength / 2;
                gamma = eccentricity * yAxis;
                xAxis = Mathf.Sqrt(Mathf.Pow(yAxis, 2) - Mathf.Pow(gamma, 2));
            }
        } else
        {
            xAxis = majorAxisLength/2;
            yAxis = majorAxisLength/2;
        }
      

        
        

            //gamma = Mathf.Sqrt(Mathf.Pow(yAxis, 2) - Mathf.Pow(xAxis, 2));
            //eccentricity = gamma / yAxis / 2;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class OrbitRenderer : MonoBehaviour
{
    
    public LineRenderer lr;

    public int Segments;
    public Ellipse ellipse;

    public void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = false;
        ellipse = GetComponent<Ellipse>();
        Segments = 68;
        CalculateEllipse();
    }
    public void Update()
    {
        CalculateEllipse();
    }
	// Use this for initialization
	void CalculateEllipse ()
    {
        Vector3[] points = new Vector3[Segments + 1];
        for (int i = 0; i < Segments; i++)
        {
            Vector2 position2D = ellipse.Evaluate((float)i / (float)Segments);
            points[i] = new Vector3(position2D.x,0f, position2D.y);
        }
        points[Segments] = points[0];

        lr.positionCount = Segments + 1;
        lr.SetPositions(points);
	}
    public void OnValidate()
    {
        if (Application.isPlaying && lr!=null)
        {
            CalculateEllipse();
        }
    }
}

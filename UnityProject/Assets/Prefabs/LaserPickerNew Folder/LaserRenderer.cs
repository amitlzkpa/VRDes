using UnityEngine;
using System.Collections;

public class LaserRenderer : MonoBehaviour {


    private LineRenderer lineRenderer;
    private LaserImplm laser;




    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        laser = GetComponent<LaserImplm>();
    }
	



	// Update is called once per frame
	void Update () {
        if (!laser.imp_isOn())
        {
            return;
        }
        lineRenderer.SetPosition(0, laser.imp_getStartPoint());
        lineRenderer.SetPosition(1, laser.imp_getTerminalPoint());
    }
}

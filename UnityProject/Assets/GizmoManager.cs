using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoManager : MonoBehaviour {



    public GameObject moveGizmoPrefab;


    private GameObject moveGizmoObject;


	// Use this for initialization
	void Start () {
        moveGizmoObject = Instantiate(moveGizmoPrefab, transform.position, Quaternion.LookRotation(transform.forward), transform);
        hideMoveGizmo();
    }



    //---------------------------------------------------------------




    private bool isVisible = true;


    public void hideMoveGizmo()
    {
        isVisible = false;
        moveGizmoObject.SetActive(isVisible);
    }


    public void showMoveGizmo()
    {
        isVisible = true;
        moveGizmoObject.SetActive(isVisible);
    }


    public void toggleMoveGizmo()
    {
        isVisible = !isVisible;
        moveGizmoObject.SetActive(isVisible);
    }



}

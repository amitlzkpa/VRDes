using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoManager : MonoBehaviour {



    public GameObject moveGizmoPrefab;
    private GameObject moveGizmoObject;



    public GameObject rotateGizmoPrefab;
    private GameObject rotateGizmoObject;



    //---------------------------------------------------------------



    private bool moveGizIsVisible = true;


    public void hideMoveGizmo()
    {
        moveGizIsVisible = false;
        moveGizmoObject.SetActive(moveGizIsVisible);
    }


    public void showMoveGizmo()
    {
        moveGizIsVisible = true;
        moveGizmoObject.SetActive(moveGizIsVisible);
    }


    public void toggleMoveGizmo()
    {
        moveGizIsVisible = !moveGizIsVisible;
        moveGizmoObject.SetActive(moveGizIsVisible);
    }



    //---------------------------------------------------------------



    private bool rotGizIsVisible = true;


    public void hideRotateGizmo()
    {
        rotGizIsVisible = false;
        rotateGizmoObject.SetActive(rotGizIsVisible);
    }


    public void showRotateGizmo()
    {
        rotGizIsVisible = true;
        rotateGizmoObject.SetActive(rotGizIsVisible);
    }


    public void toggleRotateGizmo()
    {
        rotGizIsVisible = !rotGizIsVisible;
        rotateGizmoObject.SetActive(rotGizIsVisible);
    }



    //---------------------------------------------------------------



    // Use this for initialization
    void Start()
    {
        moveGizmoObject = Instantiate(moveGizmoPrefab, transform.position, Quaternion.LookRotation(transform.forward), transform);
        hideMoveGizmo();
        rotateGizmoObject = Instantiate(rotateGizmoPrefab, transform.position, Quaternion.LookRotation(transform.forward), transform);
        hideRotateGizmo();
    }



}

using UnityEngine;
using System.Collections;
using System;

public class RefObjects_Point : MonoBehaviour, RefObjectManager
{


    //---------------------------------------------------------------


    public GameObject pointRepPrefab;


    //---------------------------------------------------------------


    private GameObject pt;



    //---------------------------------------------------------------



    public Vector3 getPt()
    {
        return pt.transform.position;
    }


    //---------------------------------------------------------------


    private bool refObjVisisble;


    public void hideRefObjects()
    {
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        refObjVisisble = false;
    }

    public void showRefObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        refObjVisisble = true;
    }

    public void toggleRefObjects()
    {
        if (refObjVisisble) hideRefObjects();
        else showRefObjects();
    }


    //---------------------------------------------------------------


    void Awake () {
        pt = (GameObject)Instantiate(pointRepPrefab, transform.position, Quaternion.identity, transform);
        hideRefObjects();
    }
}

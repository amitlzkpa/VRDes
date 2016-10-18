using UnityEngine;
using System.Collections;

public class EditPlane : MonoBehaviour, Editable {


    private GameObject refObjs;


    public void enterEditMode()
    {
        refObjs.SetActive(true);
    }

    public void exitEditMode()
    {
        refObjs.SetActive(false);
    }

    public void toggleEditMode()
    {
        refObjs.SetActive(!refObjs.activeInHierarchy);
    }





    // Use this for initialization
    void Start () {
        refObjs = transform.FindChild("_RefObjects").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

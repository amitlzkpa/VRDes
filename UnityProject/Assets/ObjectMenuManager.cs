using UnityEngine;
using System.Collections;

public class ObjectMenuManager : MonoBehaviour {



    public void setObjectMenu(GameObject menuObj)
    {
        menuObj.transform.SetParent(transform, false);
        menuObj.transform.localPosition = Vector3.zero;
    }



    public void detachObjectMenu()
    {
        transform.DetachChildren();
    }



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

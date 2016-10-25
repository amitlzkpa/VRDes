using UnityEngine;
using System.Collections;

public class ObjectMenuManager : MonoBehaviour {

    private CursorManagerRight cursorManager;
    private GameObject cursorObj;
    private GameObject menuContainer;


    public void setObjectMenu(GameObject menuObj)
    {
        menuObj.transform.SetParent(menuContainer.transform, false);
        menuObj.transform.localPosition = Vector3.zero;
        cursorManager.enableCursor();
    }



    public void detachObjectMenu(GameObject tgtObject)
    {
        GameObject menuObj = menuContainer.transform.GetChild(0).gameObject;
        menuObj.transform.SetParent(tgtObject.transform);
        menuObj.transform.localPosition = Vector3.zero;
        menuObj.transform.localRotation = Quaternion.identity;
        cursorManager.disableCursor();
    }



    // Use this for initialization
    void Start () {
        cursorManager = GetComponent<CursorManagerRight>();
        cursorObj = cursorManager.getCursorObj();
        menuContainer = transform.FindChild("_MenuContainer").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

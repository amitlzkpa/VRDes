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
        RectTransform menuRectSize = menuObj.GetComponent<RectTransform>();
        cursorManager.setCursorRange(menuRectSize.rect.width, menuRectSize.rect.height);
        cursorManager.enableCursor();
    }



    public void detachObjectMenu(GameObject tgtObject)
    {
        if (!hasObjectMenu()) return;
        GameObject menuObj = menuContainer.transform.GetChild(0).gameObject;
        menuObj.transform.SetParent(tgtObject.transform);
        menuObj.transform.localPosition = Vector3.zero;
        menuObj.transform.localRotation = Quaternion.identity;
        cursorManager.setCursorRange(1.5f, 1.2f);
        cursorManager.disableCursor();
    }


    // theres an object menu if there are more than one menu parented below
    public bool hasObjectMenu()
    {
        return (menuContainer.transform.childCount > 0);
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

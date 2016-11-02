using UnityEngine;
using System.Collections;

public class ObjectMenuManager : MonoBehaviour {

    private CursorManagerRight cursorManager;
    private GameObject cursorObj;
    private GameObject menuContainer;


    // Always pass a new copy of the menuObj with the same state as menu to be created
    // needs a copy since the object will be destroyed on unsetting
    public void setObjectMenu(GameObject menuObj)
    {
        menuObj.SetActive(true);
        menuObj.transform.SetParent(menuContainer.transform, false);
        menuObj.transform.localPosition = Vector3.zero;
        RectTransform menuRectSize = menuObj.GetComponent<RectTransform>();
        cursorManager.setCursorRange(menuRectSize.rect.width, menuRectSize.rect.height);
        cursorManager.enableCursor();
    }


    // deletes all objetcs nested under the mnu container
    public void deleteObjectMenu()
    {
        for (int i=0; i<menuContainer.transform.childCount; i++)
        {
            GameObject menuObj = menuContainer.transform.GetChild(i).gameObject;
            Destroy(menuObj);
        }
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

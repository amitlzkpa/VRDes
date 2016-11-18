using UnityEngine;
using System.Collections;

public class ObjectMenuManager : MonoBehaviour {

    private CursorManagerRight cursorManager;
    private GameObject cursorObj;
    private GameObject menuContainer;


    /// <summary>
    /// Detaches all object form the menuObject and transfers it to the retutnContainer.
    /// </summary>
    /// <returns></returns>
    public void getAndEmptyObjectMenu(GameObject returnContainer)
    {
        for (int i = 0; i < menuContainer.transform.childCount; i++)
        {
            GameObject menuObj = menuContainer.transform.GetChild(i).gameObject;
            menuObj.transform.SetParent(returnContainer.transform);
        }
    }


    /// <summary>
    /// Always pass a new copy of the menuObj with the same state as menu to be created.
    /// Needs a copy since the object will be destroyed on unsetting.
    /// </summary>
    /// <param name="menuObj">The menuObject to be nested as menu.</param>
    public void setObjectMenu(GameObject menuObj)
    {
        menuObj.SetActive(true);
        menuObj.transform.SetParent(menuContainer.transform, false);
        menuObj.transform.localPosition = Vector3.zero;
        RectTransform menuRectSize = menuObj.GetComponent<RectTransform>();
        cursorManager.setCursorRange(menuRectSize.rect.width, menuRectSize.rect.height);
        cursorManager.enableCursor();
    }



    /// <summary>
    /// Deletes all objetcs nested under the menu container by destroying the instances. Use get @getAndEmptyObjectMenu()
    /// if you need live access to the menu object instead of destroying it.
    /// </summary>
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



    /// <summary>
    /// Check if there are any menus currently active.
    /// Theres an object menu if there are more than one menu parented below.
    /// </summary>
    /// <returns></returns>
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

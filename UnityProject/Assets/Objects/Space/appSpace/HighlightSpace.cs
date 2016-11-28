    using UnityEngine;
using System.Collections;

public class HighlightSpace : MonoBehaviour, Highlightable
{

    private GameObject modelObj;
    private GameObject infoCanvasObj;
    private GameObject objectMenuObj;
    private bool objectMenuDisplayed;
    private RefObjectManager refObj;

    private Vector3 directionVec;
    Quaternion tgtRotation;
    private float verticalOffsetVal;


    private bool highlightOn;
    private bool prevHighlight;


    private bool highlightDown;
    private bool highlightPressed;
    private bool highlightUp;


    //---------------------------------------------------------------


    public void highlightObject()
    {
        transform.FindChild("_ObjectInfo").gameObject.GetComponent<UIUpdater>().updateUI();
        highlightOn = true;
    }


    public void displayObjectMenu()
    {
        if (objectMenuDisplayed) return;
        setObjectMenu();
        objectMenuDisplayed = true;
    }


    public void hideObjectMenu()
    {
        if (objectMenuDisplayed)
        {
            unsetObjectMenu();
            objectMenuDisplayed = false;
        }
    }



    //---------------------------------------------------------------



    public void displayInfoMenu()
    {
        infoCanvasObj.SetActive(true);
    }


    public void hideInfoMenu()
    {
        infoCanvasObj.SetActive(false);
    }



    //---------------------------------------------------------------


    public void setHighlightMaterial()
    {
        for (int i = 0; i < modelObj.transform.childCount; i++)
        {
            modelObj.transform.GetChild(i).gameObject.GetComponent<Highlightable>().setHighlightMaterial();
        }
    }


    public void unsetHighlightMaterial()
    {
        for (int i = 0; i < modelObj.transform.childCount; i++)
        {
            modelObj.transform.GetChild(i).gameObject.GetComponent<Highlightable>().unsetHighlightMaterial();
        }
    }


    private void orientUICanvas()
    {
        directionVec = (GeneralSettings.player.transform.position - refObj.getPtCenter()).normalized;
        tgtRotation = Quaternion.LookRotation(directionVec);
        infoCanvasObj.transform.rotation = tgtRotation;
        // rotate canvas so it faces the user instead of away from the user
        infoCanvasObj.transform.RotateAround(infoCanvasObj.transform.position, infoCanvasObj.transform.up, 180f);
        // move the canvas vertically a little wrt where the user is looking from
        // i.e. when user looks from exactly below move it down a little; useful when the tag is on the ceiling
        // and user tries to look at it from the same level
        verticalOffsetVal = (infoCanvasObj.transform.localScale.y * directionVec.y);
        infoCanvasObj.transform.position = refObj.getPtCenter() + (directionVec * 0.6f) + new Vector3(0, verticalOffsetVal, 0);
    }


    private void resetUICanvasOrient()
    {
        infoCanvasObj.transform.position = refObj.getPtCenter();
        infoCanvasObj.transform.rotation = Quaternion.identity;
    }


    //---------------------------------------------------------------


    private void setObjectMenu()
    {
        // menu object has to be duplicated with the same state as it'll be destryed on unsetting
        GameObject menuToSend = (GameObject)Object.Instantiate(objectMenuObj, transform);
        GeneralSettings.setObjectMenu(menuToSend);
    }


    private void unsetObjectMenu()
    {
        GeneralSettings.deleteObjectMenu();
    }


    //---------------------------------------------------------------


    // Use this for initialization
    void Start()
    {
        modelObj = transform.FindChild("_Model").gameObject;
        infoCanvasObj = transform.FindChild("_ObjectInfo").gameObject;
        infoCanvasObj.SetActive(false);
        objectMenuObj = transform.FindChild("_ObjectMenu").gameObject;
        objectMenuObj.SetActive(false);
        refObj = transform.FindChild("_RefObjects").gameObject.GetComponent<RefObjectManager>();
    }


    // Update is called once per frame
    void Update()
    {

        if (!prevHighlight && highlightOn)
        {
            highlightDown = true;
            highlightPressed = false;
            highlightUp = false;
        }

        if (prevHighlight && !highlightOn)
        {
            highlightDown = false;
            highlightPressed = false;
            highlightUp = true;
        }

        if (prevHighlight && highlightOn)
        {
            highlightDown = false;
            highlightPressed = true;
            highlightUp = false;
        }


        if(highlightDown)
        {
            setHighlightMaterial();
            displayInfoMenu();
            highlightDown = false;
        }

        if (highlightPressed)
        {
            orientUICanvas();
        }

        if (highlightUp)
        {
            unsetHighlightMaterial();
            hideInfoMenu();
            resetUICanvasOrient();
            highlightUp = false;
        }


        prevHighlight = highlightOn;
        highlightOn = false;
    }
}

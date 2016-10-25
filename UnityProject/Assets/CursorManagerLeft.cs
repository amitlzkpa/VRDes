using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorManagerLeft : MonoBehaviour
{


    private float windowWidth = 0.15f;
    private float windowHeight = 0.12f;

    private GameObject cursor;
    private bool cursorOn;
    private bool cursorStateUpdate = false;

    private Vector3 touchPos;
    private Vector3 defaultScale;
    private Vector3 clickScale;
    private Vector3 activeScale;

    private float defScaleVal = 0.005f;
    private float clickScaleVal = 0.01f;

    private Ray cursorRay;
    private RaycastHit cursorRayHit;
    private Button clickButton;


    //---------------------------------------------------------------


    public GameObject getCursorObj()
    {
        return cursor;

    }


    //---------------------------------------------------------------


    public void enableCursor()
    {
        cursorOn = true;
        cursorStateUpdate = true;
    }


    public void disableCursor()
    {
        cursorOn = false;
        cursorStateUpdate = true;
    }


    public void setCursorRange(float width, float height)
    {
        windowWidth = Mathf.Abs(width);
        windowHeight = Mathf.Abs(height);
    }


    //---------------------------------------------------------------


    private float remap(float srcMax, float tgtMax, float val)
    {
        return (val / srcMax) * tgtMax;
    }


    //---------------------------------------------------------------


    float xVal;
    float yVal;


    private void moveCursor()
    {
        xVal = remap(1f, windowWidth, WandControlsManager.WandControllerLeft.getTouchPadX());
        yVal = remap(1f, windowHeight, WandControlsManager.WandControllerLeft.getTouchPadY());
        xVal = Mathf.Abs(xVal) > windowWidth / 2f ? Mathf.Sign(xVal) * windowWidth / 2 : xVal;
        yVal = Mathf.Abs(yVal) > windowHeight / 2f ? Mathf.Sign(yVal) * windowHeight / 2 : yVal;
        touchPos.x = xVal;
        touchPos.y = yVal;
        cursor.transform.localPosition = touchPos;
        cursor.transform.localScale = activeScale;
    }



    // Use this for initialization
    void Start()
    {
        cursor = transform.FindChild("_Cursor").gameObject;
        touchPos = new Vector3();
        touchPos.z = cursor.transform.localPosition.z;
        defaultScale = new Vector3(defScaleVal, defScaleVal, defScaleVal);
        clickScale = new Vector3(clickScaleVal, clickScaleVal, clickScaleVal);
        setCursorRange(0.2f, 0.3f);
        enableCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorStateUpdate)
        {
            cursor.SetActive(cursorOn);
            cursorStateUpdate = false;
        }




        if (WandControlsManager.WandControllerLeft.getTouchPadTouched())
        {
            if (WandControlsManager.WandControllerLeft.getTouchPadButtonPressed())
            {
                activeScale = clickScale;
            }
            else
            {
                activeScale = defaultScale;
                moveCursor();
            }

            cursorRay = new Ray(cursor.transform.position, cursor.transform.forward);
            if (WandControlsManager.WandControllerLeft.getTouchPadButtonUp())
            {
                if (Physics.Raycast(cursorRay, out cursorRayHit, 0.1f))
                {
                    clickButton = cursorRayHit.transform.gameObject.GetComponent<Button>();
                    if (clickButton != null)
                    {
                        clickButton.onClick.Invoke();
                    }
                }
            }
        }
    }
}

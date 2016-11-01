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

    private Vector3 upPos;
    private Vector3 touchPos;

    private Vector3 defaultScale;
    private Vector3 clickScale;
    private Vector3 activeScale;

    private float defScaleVal = 0.04f;
    private float clickScaleVal = 0.08f;

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
        if (WandControlsManager.WandControllerLeft.getTouchPadButtonPressed())
        {
            activeScale = clickScale;
        }
        else
        {
            activeScale = defaultScale;
        }


        if (WandControlsManager.WandControllerLeft.getTouchPadTouchedUp())
        {
            upPos = cursor.transform.localPosition;
            return;
        }
        if (WandControlsManager.WandControllerLeft.getTouchPadTouched())
        {
            xVal = remap(1f, windowWidth/2f, WandControlsManager.WandControllerLeft.getTouchPadX());
            yVal = remap(1f, windowHeight/3f, WandControlsManager.WandControllerLeft.getTouchPadY());
            touchPos.x = xVal + upPos.x;
            touchPos.y = yVal + upPos.y;
            touchPos.x = Mathf.Abs(touchPos.x) > windowWidth / 2f ? Mathf.Sign(touchPos.x) * windowWidth / 2 : touchPos.x;
            touchPos.y = Mathf.Abs(touchPos.y) > windowHeight / 2f ? Mathf.Sign(touchPos.y) * windowHeight / 2 : touchPos.y;
            cursor.transform.localPosition = touchPos;
            cursor.transform.localScale = activeScale;
        }
    }



    // Use this for initialization
    void Start()
    {
        cursor = transform.FindChild("_Cursor").gameObject;
        upPos = new Vector3();
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


        moveCursor();
        if (WandControlsManager.WandControllerLeft.getTouchPadButtonDown())
        {
            cursorRay = new Ray(cursor.transform.position, cursor.transform.forward);
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

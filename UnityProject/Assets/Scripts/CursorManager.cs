using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour {

    private GameObject cursor;
    private bool cursorOn;

    private Vector3 touchPos;
    private Vector3 defaultScale;
    private Vector3 clickScale;
    private Vector3 activeScale;

    private float defScaleVal = 0.4f;
    private float clickScaleVal = 0.8f;

    private Ray cursorRay;
    private RaycastHit cursorRayHit;
    private Button clickButton;

    // Use this for initialization
    void Start () {
        cursor = transform.FindChild("_Cursor").gameObject;
        touchPos = new Vector3();
        touchPos.z = cursor.transform.localPosition.z;
        defaultScale = new Vector3(defScaleVal, defScaleVal, defScaleVal);
        clickScale = new Vector3(clickScaleVal, clickScaleVal, clickScaleVal);
    }
	
	// Update is called once per frame
	void Update () {
        if (WandControlsManager.WandControllerRight.getTouchPadTouched())
        {
            if (WandControlsManager.WandControllerRight.getTouchPadButtonPressed())
            {
                activeScale = clickScale;
            }
            else
            {
                activeScale = defaultScale;
            }
            touchPos.x = WandControlsManager.WandControllerRight.getTouchPadX();
            touchPos.y = WandControlsManager.WandControllerRight.getTouchPadY();
            cursor.transform.localPosition = touchPos;
            cursor.transform.localScale = activeScale;
            

            cursorRay = new Ray(cursor.transform.position, cursor.transform.forward);
            if (WandControlsManager.WandControllerRight.getTouchPadButtonUp())
            {
                if (Physics.Raycast(cursorRay, out cursorRayHit, 0.1f)) {
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

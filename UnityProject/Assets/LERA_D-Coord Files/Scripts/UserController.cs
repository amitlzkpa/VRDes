using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

    private float moveSpeed = 0.05f;


    private GameObject leftWand;
    private LaserPicker leftLaser;
    private GameObject headCamera;

    private bool XZMoveOn = false;
    private float XZFirstTouchTime;
    private float XZLag = 0.5f;
    private bool YMoveOn = false;


    // Use this for initialization
    void Start ()
    {
        leftWand = transform.FindChild("Controller (left)").gameObject;
        headCamera = transform.FindChild("Camera (eye)").FindChild("Camera (eye)").gameObject;
        leftLaser = leftWand.transform.FindChild("_LaserPicker").GetComponent<LaserPicker>();
    }

	
	// Update is called once per frame
	void Update () {


        // teleport
        if (WandControlsManager.WandControllerLeft.getTriggerDown())
        {
            if (leftLaser.isOn())
            {
                GeneralSettings.fadeScreen();
                Vector3 targetPosition = leftLaser.getHitPoint();
                targetPosition.y += 1f;
                transform.position = targetPosition;
            }
        }


        // fly
        if (WandControlsManager.WandControllerLeft.getGripPressed())
        {
            Vector3 moveVector = (leftWand.transform.forward * moveSpeed);
            transform.Translate(moveVector);
        }







        /*


        // toggle laser
        if (WandControlsManager.WandControllerLeft.getTouchPadSwipeDown())
        {
            leftLaser.disableLaser();
        }
        if (WandControlsManager.WandControllerLeft.getTouchPadSwipeUp())
        {
            leftLaser.enableLaser();
        }




        // THIS PART STILL NEEDS REFINEMENT

        // Y move up
        YMoveOn = false;
        if (WandControlsManager.WandControllerLeft.getTouchPadButtonClickUp())
        {
            YMoveOn = true;
        }

        // Y move down
        if (WandControlsManager.WandControllerLeft.getTouchPadButtonClickDown())
        {
            YMoveOn = true;
        }

        if (YMoveOn)
        {
            Vector3 moveVector = (Vector3.up * WandControlsManager.WandControllerLeft.getTouchPadY() * moveSpeed);
            transform.Translate(moveVector);
            XZFirstTouchTime = Time.time;
        }



        // XZ move
        if (YMoveOn)
        {
            // dont xz move when performing y move
            return;
        }
        XZMoveOn = false;
        if (WandControlsManager.WandControllerLeft.getTouchPadTouchedDown())
        {
            XZFirstTouchTime = Time.time;
        }
        if((WandControlsManager.WandControllerLeft.getTouchPadTouched()) && ((Time.time - XZFirstTouchTime) > XZLag))
        {
            XZMoveOn = true;
        }
        if (WandControlsManager.WandControllerLeft.getTouchPadTouchedUp())
        {
            XZMoveOn = false;
        }
        if (WandControlsManager.WandControllerLeft.getTouchPadButtonPressed())
        {
            XZMoveOn = false;
        }
        if (XZMoveOn)
        {
            Vector3 moveVector = (headCamera.transform.forward * WandControlsManager.WandControllerLeft.getTouchPadY() * moveSpeed) +
                                 (headCamera.transform.right * WandControlsManager.WandControllerLeft.getTouchPadX() * moveSpeed);
            moveVector.y = 0;
            transform.Translate(moveVector);
        }
        */



    }
}

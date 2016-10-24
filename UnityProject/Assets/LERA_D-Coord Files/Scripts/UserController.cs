using UnityEngine;
using System.Collections;

public class UserController : MonoBehaviour {

    private float moveSpeed = 0.05f;


    private GameObject leftWand;
    private LaserPicker leftLaser;
    private GameObject headCamera;

    private float eyeHeight = 1.8f;



    IEnumerator teleportToPoint(Vector3 hitPt)
    {
        yield return new WaitForSeconds(0.5f);
        hitPt.y += eyeHeight;
        transform.position = hitPt;
    }


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
                StartCoroutine(teleportToPoint(leftLaser.getHitPoint()));
            }
        }


        // fly
        if (WandControlsManager.WandControllerLeft.getGripPressed())
        {
            Vector3 moveVector = (leftWand.transform.forward * moveSpeed);
            transform.Translate(moveVector);
        }



    }
}

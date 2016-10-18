using UnityEngine;
using System.Collections;
using System.Collections.Generic;









public class ToolBoxManager : MonoBehaviour {


    List<GameObject> toolsScreenList;



    private int activeScreenIdx = 0;
    private bool update;
    private bool rotationOn;



    // ref: https://forum.unity3d.com/threads/rotate-spin-object-360-degrees-over-set-time-in-coroutine.395332/
    IEnumerator rotateRoutine(float rotAmt)
    {
        rotationOn = true;
        float startRotation = transform.localEulerAngles.z;
        float endRotation = startRotation + rotAmt;
        float t = 0.0f;
        float duration = 0.2f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, zRotation);
            yield return null;
        }
        rotationOn = false;
    }




    private void rotateLeft()
    {
        activeScreenIdx++;
        activeScreenIdx %= 4;
        StartCoroutine(rotateRoutine(90));
    }

    private void rotateRight()
    {
        activeScreenIdx--;
        if (activeScreenIdx < 0) activeScreenIdx = 3;
        StartCoroutine(rotateRoutine(-90));
    }






    // Use this for initialization
    void Start () {

        toolsScreenList = new List<GameObject>();

        for(int i=0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name.ToLower().Contains("screen"))
            {
                toolsScreenList.Add(transform.GetChild(i).gameObject);
            }
        }

    }
	





	// Update is called once per frame
	void Update () {

        // dont do anythig when the toolbox is rotating
        if (rotationOn) return;


        if (WandControlsManager.WandControllerLeft.getTouchPadSwipeLeft())
        {
            rotateLeft();
        }

        if (WandControlsManager.WandControllerLeft.getTouchPadSwipeRight())
        {
            rotateRight();
        }





        if (WandControlsManager.WandControllerLeft.getTouchPadSwipeUp())
        {
            toolsScreenList[activeScreenIdx].GetComponent<ToolScreenManager>().prevButton();
        }


        if (WandControlsManager.WandControllerLeft.getTouchPadSwipeDown())
        {
            toolsScreenList[activeScreenIdx].GetComponent<ToolScreenManager>().nextButton();
        }


        if (WandControlsManager.WandControllerLeft.getTouchPadButtonDown())
        {
            toolsScreenList[activeScreenIdx].GetComponent<ToolScreenManager>().clickActiveButton();
        }

    }
}

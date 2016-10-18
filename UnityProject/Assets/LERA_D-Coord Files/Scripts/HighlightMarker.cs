using UnityEngine;
using System.Collections;

public class HighlightMarker : MonoBehaviour {



    private bool highlightOn;
    private bool prevHighlight;
    private GameObject model;
    private GameObject canvas;
    private Vector3 directionVec;
    Quaternion targetRotation;
    private float verticalOffsetVal;




    public void highlightMarker()
    {
        highlightOn = true;
    }



	// Use this for initialization
    void Start () {
        model = transform.FindChild("_Model").gameObject;
        canvas = transform.FindChild("_Canvas").gameObject;
        canvas.transform.position = model.transform.position;
	}
	
	// Update is called once per frame
	void Update () {


        if (highlightOn)
        {
            model.transform.RotateAround(model.transform.position, model.transform.up, 1f);
            canvas.SetActive(true);
            directionVec = (GeneralSettings.player.transform.position - model.transform.position).normalized;
            targetRotation = Quaternion.LookRotation(directionVec);
            canvas.transform.rotation = targetRotation;
            // rotate canvas so it faces the user instead of away from the user
            canvas.transform.RotateAround(canvas.transform.position, canvas.transform.up, 180f);
            // move the canvas vertically a little wrt where the user is looking from
            // i.e. when user looks from exactly below move it down a little; useful when the tag is on the ceiling
            // and user tries to look at it from the same level
            verticalOffsetVal = (canvas.transform.localScale.y * directionVec.y);
            canvas.transform.position = model.transform.position + (directionVec * 0.6f) + new Vector3(0, verticalOffsetVal, 0);
        }

        if (!highlightOn && prevHighlight)
        {
            canvas.transform.position = model.transform.position;
            model.transform.rotation = Quaternion.identity;
            canvas.transform.rotation = Quaternion.identity;
            canvas.SetActive(false);
        }


        prevHighlight = highlightOn;
        highlightOn = false;
	}
}

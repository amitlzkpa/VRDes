using UnityEngine;
using System.Collections;

public class DisappearAfterTime : MonoBehaviour {


    public float timeInSecs = 30;



	// Use this for initialization
	void Start () {
        if (timeInSecs == 0)
        {
            gameObject.GetComponent<DisappearAfterTime>().enabled = false;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timeInSecs)
            gameObject.SetActive(false);
	}
}

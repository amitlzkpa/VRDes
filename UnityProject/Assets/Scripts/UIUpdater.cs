using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour {

    private bool update;
    private Text uiText;

    public void updateUI()
    {
        update = true;
    }


	// Use this for initialization
	void Start () {
        uiText = transform.FindChild("Canvas").FindChild("info").gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!update) return;
        uiText.text = GeneralSettings.getParentClone(gameObject, "app_").GetComponent<InfoObject>().getInfoString();
        update = false;
	}
}

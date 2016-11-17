using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumPadManager : MonoBehaviour {


    private string text = "";
    private Text UIText; 



	// Use this for initialization
	void Start () {
        UIText = transform.FindChild("_Text").gameObject.GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}



    public void add(string inp)
    {
        if (inp.Equals("bksp") && text.Length > 0)
        {
            text = text.Substring(0, text.Length - 1);
        }
        else
        {
            text += inp.ToString();
        }
        UIText.text = text;
    }


}

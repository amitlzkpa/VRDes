using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloseMenuButtonActions : MonoBehaviour {


    string stringBuffer;
    private InputField bufferUI;


    private void updateUIBuffer()
    {
        bufferUI.text = stringBuffer;
    }


    private void addToStringBuffer(string inputChar)
    {
        stringBuffer += inputChar;
    }


    public void pressButton(GameObject clickedObj)
    {
        string c = clickedObj.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        switch (c)
        {
            case "ENTER":
                {
                    GeneralSettings.addLineToConsole(stringBuffer);
                    stringBuffer = "";
                    break;
                }
            case "CANCEL":
                {
                    stringBuffer = "";
                    break;
                }
            case "SPACE":
                {
                    addToStringBuffer(" ");
                    break;
                }
            case "BACK":
                {
                    stringBuffer = stringBuffer.Remove(stringBuffer.Length - 1, 1);
                    break;
                }
            default:
                {
                    addToStringBuffer(c);
                    break;
                }
        }
        updateUIBuffer();
    }



    void Start()
    {
        bufferUI = transform.parent.FindChild("_InputBox").gameObject.GetComponent<InputField>();
    }

}

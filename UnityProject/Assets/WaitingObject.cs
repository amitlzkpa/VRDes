using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingObject : ScriptableObject {


    private bool valStored = false;
    private string storedString;


    public void setString(string inp)
    {
        if (valStored)
        {
            Debug.LogError("Attempting to set value of a WaitingObject twice.");
            return;
        }
        storedString = inp;
        valStored = true;
    }


    public string readString()
    {
        if (!valStored)
        {
            Debug.LogError("Attempting to read an uninitiated string.");
        }
        return storedString;
    }


    public bool isSet()
    {
        return valStored;
    }


}

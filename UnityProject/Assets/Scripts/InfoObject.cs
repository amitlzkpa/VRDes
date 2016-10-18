using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class InfoObject : MonoBehaviour, Infoable
{


    private string objName;
    private HashSet<string> tags;
    private int layer;
    private Vector3 position;





    public void setName(string name)
    {
        objName = name;
    }

    public string getName()
    {
        return objName;
    }

    public List<string> getTags()
    {
        return new List<string>(tags);
    }

    public bool hasTag(string tag)
    {
        return tags.Contains(tag);
    }

    public void addTag(string tag)
    {
        tags.Add(tag);
    }

    public void addTags(List<string> tagList)
    {
        foreach (string tag in tagList)
        {
            tags.Add(tag);
        }
    }

    public Vector3 getPosition()
    {
        return position;
    }

    public void setLayer(int layer)
    {
        this.layer = layer;
    }

    public int getLayer()
    {
        return layer;
    }

    public bool isOnLayer(int layer)
    {
        return this.layer == layer;
    }






    private void updateValues()
    {
        objName = gameObject.name;
        layer = gameObject.layer;
        position = transform.position;
    }



    private string getTagString(HashSet<string> inputTagSet)
    {
        string returnStr = "";
        if (inputTagSet.Count == 0) return returnStr;
        foreach(string tag in inputTagSet)
        {
            returnStr += tag;
            returnStr += ", ";
        }
        returnStr = returnStr.Substring(0, returnStr.Length - 2);
        return returnStr;
    }


    public SortedDictionary<string, string> getInfoDict()
    {
        updateValues();
        SortedDictionary<string, string> infoDict = new SortedDictionary<string, string>();
        infoDict.Add("Name", objName);
        infoDict.Add("Tags", getTagString(tags));
        infoDict.Add("Layer", layer.ToString());
        infoDict.Add("Location", position.ToString());
        return infoDict;
    }

    public string getInfoString()
    {
        SortedDictionary<string, string> infoDict = getInfoDict();
        string retTxt = "";
        foreach (string key in infoDict.Keys)
        {
            retTxt += key;
            retTxt += " : ";
            string valContainer;
            infoDict.TryGetValue(key, out valContainer);
            retTxt += valContainer;
            retTxt += "\n";
        }
        return retTxt.Trim();
    }





    // Use this for initialization
    void Start()
    {
        tags = new HashSet<string>();
        updateValues();
    }
}

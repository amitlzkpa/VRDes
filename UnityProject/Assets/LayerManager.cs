using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayerManager : MonoBehaviour {




    public GameObject buttonPrefab;
    private GameObject modelObjects;
    private GameObject layerButtonSets;

    private int layerCount;

    private float xPos = 1.043081e-06f;
    private float yPos = 2800f;
    private float yGap = -350f;

    private int layerLimit = 15;


    //---------------------------------------------------------------


    private void destroyAllButtonSets()
    {
        GameObject buttonObj;
        for (int i = 0; i < layerButtonSets.transform.childCount; i++)
        {
            buttonObj = layerButtonSets.transform.GetChild(i).gameObject;
            Destroy(buttonObj);
        }
    }


    private void createButtonSets()
    {
        layerCount = modelObjects.transform.childCount;
        if (layerCount > layerLimit)
        {
            GeneralSettings.addLineToConsole(string.Format("Layer limit exceeded. Max {0} layers can be created. Ignoring the bottom {1} layers.", layerLimit, layerCount - layerLimit));
            layerCount = layerLimit;
        }

        GameObject buttonCreated;
        for (int i = 0; i < layerCount; i++)
        {
            GameObject layerObject = modelObjects.transform.GetChild(i).gameObject;
            buttonCreated = (GameObject)Instantiate(buttonPrefab, gameObject.GetComponent<RectTransform>().localPosition, Quaternion.identity);
            buttonCreated.name = layerObject.name;
            buttonCreated.transform.SetParent(layerButtonSets.transform, false);
            buttonCreated.transform.position = buttonCreated.transform.TransformPoint(new Vector3(xPos, yPos + (i * yGap), -5f));
            buttonCreated.GetComponent<LayerButtonSet>().setLayerObject(layerObject);
        }
    }


    private void refreshButtonSets()
    {
        destroyAllButtonSets();
        createButtonSets();
    }


    //---------------------------------------------------------------


    public void deleteLayer(GameObject layerObject)
    {
        if (layerObject == modelObjects.transform.GetChild(0).gameObject)
        {
            GeneralSettings.addLineToConsole(string.Format("Can not delete the {0} layer.", layerObject.name));
            return;
        }
        Destroy(layerObject);
        Invoke("refreshButtonSets", 0.00001f);
    }



    public void addLayer()
    {
        GameObject newLayerObj = new GameObject("New Layer");
        newLayerObj.transform.SetParent(modelObjects.transform);
        Invoke("refreshButtonSets", 0.00001f);
    }



    public void unhideAllLayers()
    {
        layerCount = modelObjects.transform.childCount;
        GameObject layerObject;
        for (int i = 0; i < layerCount; i++)
        {
            layerObject = modelObjects.transform.GetChild(i).gameObject;
            layerObject.SetActive(true);
        }
    }


    //---------------------------------------------------------------


    void Start()
    {
        modelObjects = GeneralSettings.modelObjects;
        layerButtonSets = transform.FindChild("_LayerButtonSets").gameObject;
        createButtonSets();
    }




}

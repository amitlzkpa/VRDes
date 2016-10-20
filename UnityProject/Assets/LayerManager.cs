using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayerManager : MonoBehaviour {

    public GameObject buttonPrefab;
    private GameObject modelObjects;

    private int layerCount;

    private float xPos = 1.043081e-06f;
    private float yPos = 0f;
    private float yGap = 0.015f;

    private int layerLimit = 9;






    private void setUpLayerUI()
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
            buttonCreated.transform.SetParent(transform, false);
            buttonCreated.transform.localPosition = new Vector3(xPos, yPos - (i * yGap), 0f);
            buttonCreated.GetComponent<LayerButtonSet>().setLayerObject(layerObject);
        }
    }







    public void deleteLayer(GameObject layerObject)
    {
        string layerName = layerObject.name;
        Destroy(layerObject);
        Destroy(transform.FindChild(layerName).gameObject);
    }



    public void addLayer()
    {
        Debug.Log("Implement this.");
    }





    void Start()
    {
        modelObjects = GeneralSettings.modelObjects;
        setUpLayerUI();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

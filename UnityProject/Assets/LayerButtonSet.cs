using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LayerButtonSet : MonoBehaviour {



    private LayerManager layerManager;


    private GameObject layerObj;




    public void setLayerObject(GameObject inpObject)
    {
        layerObj = inpObject;
        transform.FindChild("_Name").FindChild("Text").gameObject.GetComponent<Text>().text = layerObj.name;
    }


    public void deleteLayer()
    {
        layerManager.deleteLayer(layerObj);
    }


    public void hideLayer()
    {
        layerObj.SetActive(false);
    }


    public void unhideLayer()
    {
        layerObj.SetActive(true);
    }


    public void toggleLayer()
    {
        layerObj.SetActive(!layerObj.activeSelf);
    }








    private GameObject layerNameButtonObj;
    private GameObject layerVisibilityButtonObj;
    private GameObject layerDeleteButtonObj;

    private Button layerNameButton;
    private Button layerVisibilityButton;
    private Button layerDeleteButton;



    void Start() {
        layerManager = transform.parent.gameObject.GetComponent<LayerManager>();

        layerNameButtonObj = transform.FindChild("_Name").gameObject;
        layerVisibilityButtonObj = transform.FindChild("_Visibility").gameObject;
        layerDeleteButtonObj = transform.FindChild("_Delete").gameObject;

        layerNameButton = layerNameButtonObj.GetComponent<Button>();
        layerVisibilityButton = layerVisibilityButtonObj.GetComponent<Button>();
        layerDeleteButton = layerDeleteButtonObj.GetComponent<Button>();
}
	
	// Update is called once per frame
	void Update () {
	
	}
}

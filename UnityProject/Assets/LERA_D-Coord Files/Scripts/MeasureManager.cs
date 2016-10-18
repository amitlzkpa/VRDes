using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MeasureManager : MonoBehaviour {
    /*


	///////////////////////////////////////////////////



    public GameObject tickMarker;
	public Material dimensionLineMaterial;



	///////////////////////////////////////////////////



	private LaserPicker laser;



	///////////////////////////////////////////////////



	// used in live updating measurement
    private Text measurementTextUI;

    private string defaultText = "-- m       |       -- ft";
    private string measuredText;
    private string distM;
	private string distF;



	///////////////////////////////////////////////////



	private GameObject dimensionSet;
	private GameObject currentDim;
	private string dimObjectName = "DimensionObject";
	private int dimCount = -1;


	private GameObject currentTickObj;
	private string tickObjectName = "TickObject";
	private int tickCount = 0;



	///////////////////////////////////////////////////



	// not sure if this is used
	public string getCurrentDimObjName()
	{
		return currentDim.name;
	}



	///////////////////////////////////////////////////



    private string getDistInFeet(float distM)
    {
        float distInFeet = distM * 3.2808399f; //multiplier for metres to feet conversion
        int feet = Mathf.FloorToInt(distInFeet);
        int inches = Mathf.FloorToInt((distInFeet-feet) * 12);
        return feet.ToString() + "\' " + inches.ToString() + "\"";
	}



	///////////////////////////////////////////////////



	private void liveMeasureUpdate()
	{
		if (laser.isHit())
		{
			distM = laser.getHitDistance().ToString("F1");
			distF = getDistInFeet(laser.getHitDistance());
			measuredText = distM + " m       |       " + distF + " ft";
			measurementTextUI.text = measuredText;
		}
		else
		{
			measurementTextUI.text = defaultText;
		}
	}



	private void createNewTick(Vector3 targetPoint)
	{
		currentTickObj = (GameObject)Instantiate(tickMarker, targetPoint, Quaternion.identity);
	}



	private void createNewDimSet()
	{
		currentDim = new GameObject();
		dimCount++;
		currentDim.transform.SetParent(dimensionSet.transform);
		currentDim.name = dimObjectName + "_" + dimCount;
		currentDim.AddComponent<DimensionLineRenderer>();
	}



	private void manageDimLineAdd()
	{
		Vector3 targetPoint = laser.getHitPoint();
		createNewTick(targetPoint);
		if (tickCount == 0)
		{
			// start new dimension line
			createNewDimSet();
			currentTickObj.transform.SetParent(currentDim.transform);
            currentTickObj.name = tickObjectName + "_" + dimCount + "_" + tickCount;
            GeneralSettings.addLineToConsole("New dimension line started.");
		}
		else
		{
			// close existing dimension line
			currentTickObj.transform.SetParent(currentDim.transform);
			currentTickObj.name = tickObjectName + "_" + dimCount + "_" + tickCount;
            currentDim.GetComponent<DimensionLineRenderer>().updateDimensionLine();
            GeneralSettings.addLineToConsole((System.String.Format("Dimension line completed: {0}", currentDim.name)));
            GeneralSettings.currentlyActiveTagObject.transform.GetChild(0).gameObject.GetComponent<TagInfo>().attachItem(currentDim.name);
		}
		tickCount++;
		tickCount %= 2;
	}



    private void undoDimSet()
    {
        if (dimensionSet.transform.childCount > 0)
        {
            Destroy (dimensionSet.transform.GetChild (dimensionSet.transform.childCount - 1).gameObject);
        }
    }



    ///////////////////////////////////////////////////








    public string getExportString()
    {
        string output = "";
        string header = "Name,Start-X,Start-Y,Start-Z,End-X,End-Y,End-Z,Metres";
        string seperator = ",";
        output += header;
        output += System.Environment.NewLine;
        GameObject currDimInProcess;
        string workTxt;
        for(int i=0; i < dimensionSet.transform.childCount; i++)
        {
            currDimInProcess = dimensionSet.transform.GetChild(i).gameObject;
            if (currDimInProcess == null || currDimInProcess.transform.childCount != 3)
                continue;
            output += currDimInProcess.name;
            output += seperator;

            workTxt = currDimInProcess.transform.GetChild(0).position.ToString().Replace("(", "").Replace(")", "").Replace(" ", "");
            output += workTxt;
            output += seperator;
            workTxt = currDimInProcess.transform.GetChild(1).position.ToString().Replace("(", "").Replace(")", "").Replace(" ", "");
            output += workTxt;
            output += seperator;

            output += Vector3.Distance(currDimInProcess.transform.GetChild(0).position, currDimInProcess.transform.GetChild(1).position);
            output += System.Environment.NewLine;
        }
        return output;
    }



	///////////////////////////////////////////////////



    // Use this for initialization
    void Start () {

        laser = transform.FindChild("LaserPicker").GetComponent<LaserPicker>();
        measurementTextUI = transform.FindChild("Canvas").FindChild("MeasurementText").GetComponent<Text>();

        dimensionSet = GeneralSettings.dimensionSetManager;
    }





	// Update is called once per frame
	void Update () {



        liveMeasureUpdate();



        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (laser.isHit())
            {
				manageDimLineAdd();
            }
        }



		if (WandControlsManager.WandControllerRight.getGripDown ())
		{
            GeneralSettings.resetActiveTagAndSwitchToTagManager();
        }






	
	}

    */
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour {

    /*

    private LaserPicker laser;

    private GameObject audioManagerRef;
    private Text UIText;
    private Image UIBackground;


    private GameObject currAudioObject;
    private string audRecObjectName = "AudioObject";
    private int audRecCount = 0;


    private bool isRec = false;
    private int maxTimeInSecs = (15 * 60);
    private int sampleRate = 44100;


    private AudioClip recClip;
    private AudioClip tempClip;
    private float startTime;
    private bool timeLimCheck = false;


    private string recordingOnText = "Voice recording on...";
    private string recordingOffText = "Press trigger to start recording.";






    public void startNewAudioRecording()
    {
        tempClip = Microphone.Start(null, false, maxTimeInSecs, sampleRate);
        isRec = true;
        startTime = Time.time;
        UIText.text = recordingOnText;
        GeneralSettings.addLineToConsole("Started recording an audio note. Max time is 15 minutes.");
    }




    public void stopAndSaveRecording()
    {
        currAudioObject = new GameObject();
        currAudioObject.transform.SetParent(audioManagerRef.transform);
        currAudioObject.name = audRecObjectName + "_" + audRecCount;
        currAudioObject.AddComponent<AudioSource>();
        string fileName = audRecObjectName + "_" + audRecCount + "_" + GeneralSettings.sessionID;
        recClip = getClippedAudio(fileName);
        SavWav.Save(fileName, recClip);
        currAudioObject.GetComponent<AudioSource>().clip = recClip;
        audRecCount++;
        isRec = false;
        UIText.text = recordingOffText;
        GeneralSettings.currentlyActiveTagObject.transform.GetChild(0).gameObject.GetComponent<TagInfo>().attachItem(currAudioObject.name);
        GeneralSettings.addLineToConsole("Audio note saved.");
    }





    private AudioClip getClippedAudio(string fileName)
    {
        int lastSample = Microphone.GetPosition(null);
        int tgtSampleSize = lastSample != 0 ? lastSample : maxTimeInSecs * sampleRate;
        float[] holdingFloats = new float[tgtSampleSize];
        tempClip.GetData(holdingFloats, 0);
        AudioClip clippedClip = AudioClip.Create(fileName, tgtSampleSize, 1, sampleRate, false);
        clippedClip.SetData(holdingFloats, 0);
        return clippedClip;
    }






    private int pulseDuration = 90;
    private float pulseLow = 0.2f;
    private float pulseHigh = 1f;
    private float changeVal;
    private Color holdingColor;
    private Color originalCol;


    private void pulsatingBackground()
    {
        holdingColor = UIBackground.color;
        holdingColor.r += changeVal;
        UIBackground.color = holdingColor;
        if (UIBackground.color.r < pulseLow || UIBackground.color.r > pulseHigh)
            changeVal *= -1;
    }


    private void setDefaultBackgroundColor()
    {
        if (UIBackground.color != originalCol)
            UIBackground.color = originalCol;
    }











	// Use this for initialization
    void Start () {
        laser = transform.FindChild("LaserPicker").GetComponent<LaserPicker>();
        laser.disableLaser();

        audioManagerRef = GeneralSettings.audioSetManager;
        UIText = transform.FindChild("Canvas").FindChild("RecordingText").gameObject.GetComponent<Text>();
        UIBackground = transform.FindChild("Canvas").FindChild("Background").gameObject.GetComponent<Image>();
        UIText.text = recordingOffText;
        originalCol = UIBackground.color;
        changeVal = (pulseHigh - pulseLow) / pulseDuration;
	}


	
	// Update is called once per frame
	void Update () {
        




        if (WandControlsManager.WandControllerRight.getTriggerDown())
        {
            if (!isRec)
            {
                startNewAudioRecording();
            }
            else
            {
                stopAndSaveRecording();
            }
        }






        if (WandControlsManager.WandControllerRight.getGripDown ())
        {
            GeneralSettings.resetActiveTagAndSwitchToTagManager();
        }







        if (isRec)
            pulsatingBackground();
        else
            setDefaultBackgroundColor();
        
        if (isRec && ((Time.time-startTime) > maxTimeInSecs))
            timeLimCheck = true;
        
        if (timeLimCheck)
        {
            GeneralSettings.addLineToConsole("Max record time reached. Please create a new note to record more.");
            stopAndSaveRecording();
            timeLimCheck = false;
        }





	}

    */
}

using UnityEngine;
using System.Collections;

public class AudioRecordManager : MonoBehaviour {

    /*

    public bool isRecording
    {
        get
        {
            return isRec;
        }
    }



    private GameObject audioManagerRef;


    private GameObject currAudioObject;
    private string audRecObjectName = "AudioClipObject";
    private int audRecCount = 0;


    private bool isRec = false;
    private int maxTimeInSecs = (5 * 1);
    private int sampleRate = 44100;

    private AudioClip recClip;
    private AudioClip tempClip;
    private float startTime;








    public void startNewAudioRecording()
    {
        currAudioObject = new GameObject();
        currAudioObject.transform.SetParent(audioManagerRef.transform);
        currAudioObject.name = audRecObjectName + "_" + audRecCount;
        currAudioObject.AddComponent<AudioSource>();
        tempClip = Microphone.Start(null, false, maxTimeInSecs, sampleRate);
        isRec = true;
        startTime = Time.time;
    }




    public void stopAndSaveRecording()
    {
        if (!isRecording)
        {
            Debug.Log("No recording underway.");
            return;
        }
        string fileName = audRecObjectName + "_" + audRecCount + Time.time.ToString();
        recClip = getClippedAudio(fileName);
        SavWav.Save(fileName, recClip);
        currAudioObject.GetComponent<AudioSource>().clip = recClip;
        audRecCount++;
        isRec = false;
    }



    // FIX-THIS: rework absolute calls to make relative calls
    public void replayLast()
    {
        GameObject.Find("[CameraRig]").AddComponent<AudioSource>();
        AudioSource speakerNearEar = GameObject.Find("[CameraRig]").GetComponent<AudioSource>();
        speakerNearEar.clip = currAudioObject.GetComponent<AudioSource>().clip;
        speakerNearEar.Play();
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







	// Use this for initialization
	void Start () {
        audioManagerRef = GeneralSettings.audioSetManager;
    }
	


	// Update is called once per frame
	void Update () {
	    
        if (isRecording)
        {
            if ((Time.time-startTime) > maxTimeInSecs)
            {
                stopAndSaveRecording();
            }
        }


	}

    */
}

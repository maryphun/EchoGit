using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class VoicePlayback : MonoBehaviour
{
    // Boolean flags shows if the microphone is connected 
    private bool micConnected = false;

    //The maximum and minimum available recording frequencies  
    private int minFreq;
    private int maxFreq;

    //A handle to the attached AudioSource  
    private AudioSource goAudioSource;

    [SerializeField] DemoPlayerController player;
    [SerializeField] float maxRecordTime = 4.0f;

    void Start()
    {
        //Check if there is at least one microphone connected  
        if (Microphone.devices.Length <= 0)
        {
            //Throw a warning message at the console if there isn't  
            Debug.LogWarning("Microphone not connected!");
        }
        else //At least one microphone is present  
        {
            //Set our flag 'micConnected' to true  
            micConnected = true;

            //Get the default microphone recording capabilities  
            Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

            //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...  
            if (minFreq == 0 && maxFreq == 0)
            {
                //...meaning 44100 Hz can be used as the recording sampling rate  
                maxFreq = 44100;
            }

            //Get the attached AudioSource component  
            goAudioSource = this.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        //If there is a microphone  
        if (micConnected)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!Microphone.IsRecording(null) && player.GetEnableTarget())
                {
                    //Start recording and store the audio captured from the microphone at the AudioClip in the AudioSource  
                    goAudioSource.clip = Microphone.Start(null, true, Mathf.RoundToInt(maxRecordTime), maxFreq);
                }
            }
            
            if (Input.GetKeyUp(KeyCode.F))
            {
                Microphone.End(null); //Stop the audio recording  
                
                goAudioSource.Play();
            }
        }
        else // No microphone  
        {
            Debug.Log("no microphone detected!");
        }

    }
}  

//using UnityEngine;
//using System.Collections;
//using System;

//[RequireComponent(typeof(AudioSource))]
//public class VoicePlayback : MonoBehaviour
//{
//    // Boolean flags shows if the microphone is connected 
//    private bool micConnected = false;

//    //The maximum and minimum available recording frequencies  
//    private int minFreq;
//    private int maxFreq;

//    //A handle to the attached AudioSource  
//    private AudioSource goAudioSource;


//    public static float MicLevel;
//    private string device_;
//    private AudioClip clipRecord_;
//    private int sample_ = 128;
//    private bool isInitialized_;

//    bool speaking;

//    //microphone initialization
//    void InitMicrophone()
//    {
//        //Get the default microphone recording capabilities  
//        Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);

//        //According to the documentation, if minFreq and maxFreq are zero, the microphone supports any frequency...  
//        if (minFreq == 0 && maxFreq == 0)
//        {
//            //...meaning 44100 Hz can be used as the recording sampling rate  
//            maxFreq = 44100;
//        }

//        clipRecord_ = Microphone.Start(null, true, 20, maxFreq);

//        StartCoroutine(pauseForMicRecord(1.0f)); //after 10 seconds repeat the initialization of the microphone and record sound
//    }

//    private IEnumerator pauseForMicRecord(float pauseTime)
//    {
//        yield return new WaitForSeconds(pauseTime);
//        InitMicrophone();
//    }

//    void Start()
//    {
//        speaking = false;

//        //Check if there is at least one microphone connected  
//        if (Microphone.devices.Length <= 0)
//        {
//            //Throw a warning message at the console if there isn't  
//            Debug.LogWarning("Microphone not connected!");
//        }
//        else //At least one microphone is present  
//        {
//            //Set our flag 'micConnected' to true  
//            micConnected = true;
            
//            //Get the attached AudioSource component  
//            goAudioSource = this.GetComponent<AudioSource>();
            
//            InitMicrophone();
//            isInitialized_ = true;
//        }
//    }

//    private void Update()
//    {
//        MicLevel = LevelMax();

//        //If there is a microphone  
//        if (micConnected)
//        {
//            if ((MicLevel > 0.85f) && (MicLevel < 1.0f))
//            {
//                if (!speaking)
//                {
//                    Debug.Log("voice detected! [" + Math.Round(MicLevel, 3) + "]");
//                    speaking = true;

//                    //Start record
//                    goAudioSource.clip = Microphone.Start(null, true, 20, maxFreq);

//                    StartCoroutine(RecordingVoice());
//                }
//            }
//        }
//        else
//        {
//            Debug.Log("microphone not connected");
//        }

//    }

//    private IEnumerator RecordingVoice()
//    {
//        yield return new WaitForSeconds(1f);
//        if (speaking)
//        { 
//            if (!(MicLevel > 0.85f) && (MicLevel < 1.0f))
//            {
//                Debug.Log("voice ended");
//                // end record and play audioclip
//                speaking = false;
                
//                goAudioSource.Play(); //Playback the recorded audio  
//            }
//            else
//            {
//                StartCoroutine(RecordingVoice());
//            }
//        }
//    }

//    //get data from microphone into audioclip
//    float LevelMax()
//    {
//        float levelMax = 0;
//        float[] waveData = new float[sample_];
//        int micPosition = Microphone.GetPosition(null) - (sample_ + 1); // null is the first microphone
//        if (micPosition < 0) return 0;
//        clipRecord_.GetData(waveData, micPosition);

//        //get a peak on the last 128 samples
//        for (int i = 0; i < sample_; i++)
//        {
//            float wavePeak = waveData[i] * waveData[i];
//            if (levelMax < wavePeak)
//            {
//                levelMax = wavePeak;
//            }
//        }
//        return levelMax;
//    }
    
//    //stop mic when loading a new level or quit application
//    void OnDisable()
//    {
//        StopMicrophone();
//    }


//    void OnDestroy()
//    {
//        StopMicrophone();
//    }


//    //control mic when application gets focused
//    void OnApplicationFocus(bool focus)
//    {
//        if (focus)
//        {

//            if (!isInitialized_)
//            {
//                InitMicrophone();
//                isInitialized_ = true;
//            }
//        }
//        if (!focus)
//        {
//            StopMicrophone();
//            isInitialized_ = false;

//        }
//    }


//    void StopMicrophone()
//    {
//        Microphone.End(null);
//    }

//}


/*
Name of the module: Special effects
Date on which the module is created: 18/4/18
Author of the module:Manoj Reddy
Modification History: By Bhargav Mallala 19/4/18
                      By Balabolu Tushara Langulya 19/4/18
Synapsis of the module : This module is executed to generate sound and vibrations for the landslide
Functions: in TestAudioConfiguration class
                1.  void start()
                2.  void OnAudioConfigurationChanged(bool deviceWasChanged)
                3.  int GUIRow(string name, int[] valid, int value, ref bool modified)
                4.  void OnGUI()
            in Vibration class
                1.  public static void Vibrate()
                2.  public static void Vibrate(long milliseconds)
                3.  public static void Vibrate(long[] pattern, int repeat)
                4.  public static bool HasVibrator()
                5.  public static void Cancel()
                6.  private static bool isAndroid()
            in vibrate AndroidJavaClass
                1.void Start()
                2.void vibratephone()
Global Variables: 1.Shader used in waterbase class
                  2.RainFallParticleSystem in RainScript class
                  3.RainMistParticleSystem in RainScript class
 */
using UnityEngine;
using System.Collections;

public class TestAudioConfiguration : MonoBehaviour
{
    void Start()
    {
        AudioSettings.OnAudioConfigurationChanged += OnAudioConfigurationChanged;
    }

    void OnAudioConfigurationChanged(bool deviceWasChanged)
    {
        Debug.Log(deviceWasChanged ? "Device was changed" : "Reset was called");
        if (deviceWasChanged)
        {
            ///returns the current configuration of the audio device and System
            AudioConfiguration config = AudioSettings.GetConfiguration();
            config.dspBufferSize = 64;
            //resetting the dspbuffersize using config and then resetting
            AudioSettings.Reset(config);
        }
        //calls up the audio file
        GetComponent<AudioSource>().Play();
    }

    //defineing the various speaker mmodes
    static int[] validSpeakerModes =
    {

        (int)AudioSpeakerMode.Mono,
        (int)AudioSpeakerMode.Stereo,
        (int)AudioSpeakerMode.Quad,
        (int)AudioSpeakerMode.Surround,
        (int)AudioSpeakerMode.Mode5point1,
   };

      //the  buffersizes we intend to have are defined here
    static int[] validDSPBufferSizes =
    {
        32, 64, 128, 256, 340, 480, 512, 1024, 2048, 4096, 8192
    };

      //The sample rate setting used within the AudioImporter.
    static int[] validSampleRates =
    {
        11025, 22050, 44100, 48000, 88200, 96000,
    };

    static int[] validNumRealVoices =
    {
        1, 2, 4, 8, 16, 32, 50, 64, 100, 128, 256, 512,
    };

    static int[] validNumVirtualVoices =
    {
        1, 2, 4, 8, 16, 32, 50, 64, 100, 128, 256, 512,
    };

    int GUIRow(string name, int[] valid, int value, ref bool modified)
    {
          //begin a horizontal control group
        GUILayout.BeginHorizontal();
           //button with name=value
        GUILayout.Button(name + "=" + value);
        for (int i = 0; i < valid.Length; i++)
        {
            string s = valid[i].ToString();
            if (valid[i] == value)
                s = "[" + s + "]";
                 //if clicked on button GUILayout.Button(s) becomes true
            if (GUILayout.Button(s))
            {
                value = valid[i];
                 //if value is modified the modified becomes true
                modified = true;
            }
        }
         //ending the horizontal control group
        GUILayout.EndHorizontal();
        return value;
    }

    void OnGUI()
    {
        //storing the audio source in source
        AudioSource source = GetComponent<AudioSource>();
        bool modified = false;

        AudioConfiguration config = AudioSettings.GetConfiguration();
        //reconfiguring the values and appropraitely changing the modified values
        config.speakerMode = (AudioSpeakerMode)GUIRow("speakerMode", validSpeakerModes, (int)config.speakerMode, ref modified);
        config.dspBufferSize = GUIRow("dspBufferSize", validDSPBufferSizes, config.dspBufferSize, ref modified);
        config.sampleRate = GUIRow("sampleRate", validSampleRates, config.sampleRate, ref modified);
        config.numRealVoices = GUIRow("RealVoices", validNumRealVoices, config.numRealVoices, ref modified);
        config.numVirtualVoices = GUIRow("numVirtualVoices", validNumVirtualVoices, config.numVirtualVoices, ref modified);

          //we reset if the values have changed
        if (modified)
            AudioSettings.Reset(config);

            //when the button pressed the audio starts playing
        if (GUILayout.Button("Start"))
            source.Play();

           //audio stops playing on pressing the Stop
        if (GUILayout.Button("Stop"))
            source.Stop();
    }
}

public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        //if device is android calling vibrator
        if (isAndroid())
            vibrator.Call("vibrate");
        //triggering the vibration
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        //calling vibrator for milliseconds mentioned
        if (isAndroid())
            vibrator.Call("vibrate", milliseconds);
          //triggering the vibration
        else
            Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        //calling vibrator for the pattern and repeating it
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
        //  //triggering the vibration
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()

    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}

public class vibrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("vibratephone", 12.0f);

	}

	void vibratephone()
	{
		Vibration.Vibrate(10000);
	}


}

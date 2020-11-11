using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgoraJoin : MonoBehaviour
{
    public string appID;
    public static IRtcEngine mRtcEngine;

    public Text userName;
    public Text channelName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        mRtcEngine = IRtcEngine.GetEngine(appID);

        mRtcEngine.EnableVideo();
        mRtcEngine.EnableVideoObserver();
    }

    public void JoinChannelButton()
    {
        mRtcEngine.JoinChannel(channelName.text);
        
        SceneManager.LoadScene("Channel");
    }

    public void LeaveChannelButton()
    {
        if(mRtcEngine != null)
        {
            mRtcEngine.LeaveChannel();
            mRtcEngine.DisableVideoObserver();
        }
    }

    private void OnApplicationQuit()
    {
        if(mRtcEngine != null)
        {
            AudioRecordingDeviceManager.ReleaseInstance();
            mRtcEngine.LeaveChannel();
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
    }
}

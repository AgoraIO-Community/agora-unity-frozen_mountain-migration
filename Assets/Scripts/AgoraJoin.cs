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

    public VideoSurface videoSurface;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        mRtcEngine = IRtcEngine.GetEngine(appID);

        mRtcEngine.EnableVideo();
        mRtcEngine.EnableVideoObserver();

        mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
        mRtcEngine.OnUserJoined = OnUserJoinedHandler;
        mRtcEngine.OnUserOffline = OnUserOfflineHandler;
    }

    public void JoinChannelButton()
    {
        //int channelSuccess = mRtcEngine.JoinChannel(channelName.text, null, 0);
        int channelSuccess = mRtcEngine.JoinChannel(channelName.text);

        print("join channel success: " + channelSuccess);

        
        
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

    public void UnloadEngine()
    {
        if(mRtcEngine != null)
        {
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
    }

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        print("Join channel Success");
    }

    void OnUserJoinedHandler(uint uid, int elapsed)
    {
        print("user joined " + uid);


        if (videoSurface)
        {
            videoSurface.SetForUser(uid);
            videoSurface.SetEnable(true);
            videoSurface.SetGameFps(30);
        }
    }

    void OnUserOfflineHandler(uint uid, USER_OFFLINE_REASON reason)
    {

    }

    private void OnApplicationQuit()
    {
        if(mRtcEngine != null)
        {
            IRtcEngine.Destroy();
            mRtcEngine = null;
        }
    }
}

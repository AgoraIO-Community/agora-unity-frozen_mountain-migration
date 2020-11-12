using UnityEngine;
using agora_gaming_rtc;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AgoraJoin : MonoBehaviour
{
    public string appID;
    public static IRtcEngine mRtcEngine;
    public Text channelNameText;

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
        mRtcEngine.JoinChannel(channelNameText.text);

        SceneManager.LoadScene("Channel");
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
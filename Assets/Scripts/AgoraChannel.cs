using UnityEngine;
using UnityEngine.SceneManagement;
using agora_gaming_rtc;
using UnityEngine.UI;

public class AgoraChannel : MonoBehaviour
{
    public GameObject localVideoImage;
    public Transform spawnPoint;

    private int usersInChannel = 0;
    public Text audioDeviceText;
    public Text videoDeviceText;
    public Text channelIDText;

    void Start()
    {
        if(AgoraJoin.mRtcEngine != null)
        {
            AgoraJoin.mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
            AgoraJoin.mRtcEngine.OnUserJoined = OnUserJoinedHandler;
            AgoraJoin.mRtcEngine.OnUserOffline = OnUserOfflineHandler;
        }
    }

    // create video surface objects to hold the agora video feeds
    public void LeaveChannelButton()
    {
        if (AgoraJoin.mRtcEngine != null)
        {
            AgoraJoin.mRtcEngine.LeaveChannel();
            AgoraJoin.mRtcEngine.DisableVideo();
            AgoraJoin.mRtcEngine.DisableVideoObserver();
        }

        SceneManager.LoadScene("Join");
    }

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        VideoSurface localVideoSurface = localVideoImage.AddComponent<VideoSurface>();

        // Get the name name of your machine's microphone to display in the bottom panel
        string audioDeviceName = "null";
        string audioDeviceID = "null";
        AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine).CreateAAudioRecordingDeviceManager();
        AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine).GetCurrentRecordingDeviceInfo(ref audioDeviceName, ref audioDeviceID);
        audioDeviceText.text = audioDeviceName;

        // Get the name of your machine's webcam to display
        string videoDeviceName = "null";
        string videoDeviceID = "null";
        VideoDeviceManager.GetInstance(AgoraJoin.mRtcEngine).CreateAVideoDeviceManager();
        VideoDeviceManager.GetInstance(AgoraJoin.mRtcEngine).GetVideoDevice(0, ref videoDeviceName, ref videoDeviceID);
        videoDeviceText.text = videoDeviceName;

        // Set user name to name on the top panel
        channelIDText.text = channelName;
    }

    void OnUserJoinedHandler(uint uid, int elapsed)
    {
        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);

        // find a game object to render video stream from 'uid'
        GameObject go = GameObject.Find(uid.ToString());
        if (go != null)
        {
            return;
        }

        // create a GameObject and assign to this new user
        VideoSurface videoSurface = makeImageSurface(uid.ToString());
        if (videoSurface != null)
        {
            // configure videoSurface
            videoSurface.SetForUser(uid);
            videoSurface.SetEnable(true);
            videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
            videoSurface.SetGameFps(30);
        }

        usersInChannel++;
    }

    void OnUserOfflineHandler(uint uid, USER_OFFLINE_REASON reason)
    {
        Debug.Log("onUserOffline: uid = " + uid + " reason = " + reason);
     
        GameObject go = GameObject.Find(uid.ToString());
        if (go != null)
        {
            Destroy(go);
            usersInChannel--;
        }
    }

    public VideoSurface makeImageSurface(string goName)
    {
        GameObject go = new GameObject();

        if (go == null)
        {
            return null;
        }

        go.name = goName;
        go.AddComponent<RawImage>();

        GameObject canvas = GameObject.Find("pnlContainer");
        if (canvas != null)
        {
            go.transform.parent = canvas.transform;
        }

        // set up transform
        go.transform.Rotate(0f, 0.0f, 180.0f);
        go.transform.localPosition = spawnPoint.localPosition + (Vector3.up * usersInChannel * 300f);
        go.transform.localScale = Vector3.one * 2.5f;

        // configure videoSurface
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();
        return videoSurface;
    }   
}
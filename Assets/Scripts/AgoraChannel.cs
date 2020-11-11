using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using agora_gaming_rtc;
using UnityEngine.UI;

public class AgoraChannel : MonoBehaviour
{
    public GameObject localVideoImage;
    public Transform spawnPoint;

    private int usersInChannel = 0;

    AudioRecordingDeviceManager audioDevice;
    public Text audioDeviceText;
    public Text videoDeviceText;

    void Start()
    {
        if(AgoraJoin.mRtcEngine != null)
        {
            AgoraJoin.mRtcEngine.OnJoinChannelSuccess = OnJoinChannelSuccessHandler;
            AgoraJoin.mRtcEngine.OnUserJoined = OnUserJoinedHandler;
            AgoraJoin.mRtcEngine.OnUserOffline = OnUserOfflineHandler;

            //audioDevice = AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine);
        }
    }

    //private void Update()
    //{
    //    if (AgoraJoin.mRtcEngine == null)
    //        return;

    //    AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine).CreateAAudioRecordingDeviceManager();
    //    string audioDeviceName = "null";
    //    string audioDeviceID = "null";

    //    AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine).GetCurrentRecordingDeviceInfo(ref audioDeviceName, ref audioDeviceID);


    //    print("audiodevice name: " + audioDeviceName);
    //    print("audiodevice ID: " + audioDeviceID);
    //}

    // create video surface objects to hold the agora video feeds
    public void LeaveChannelButton()
    {
        if (AgoraJoin.mRtcEngine != null)
        {
            AgoraJoin.mRtcEngine.LeaveChannel();
            AgoraJoin.mRtcEngine.DisableVideoObserver();
        }

        SceneManager.LoadScene("Join");
    }

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        VideoSurface localVideoSurface = localVideoImage.AddComponent<VideoSurface>();

        localVideoSurface.SetForUser(uid);
        localVideoSurface.SetEnable(true);
        localVideoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
        localVideoSurface.SetGameFps(30);

        string audioDeviceName = "null";
        string audioDeviceID = "null";
        AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine).CreateAAudioRecordingDeviceManager();
        AudioRecordingDeviceManager.GetInstance(AgoraJoin.mRtcEngine).GetCurrentRecordingDeviceInfo(ref audioDeviceName, ref audioDeviceID);
        audioDeviceText.text = audioDeviceName;

        string videoDeviceName = "null";
        string videoDeviceID = "null";
        VideoDeviceManager.GetInstance(AgoraJoin.mRtcEngine).CreateAVideoDeviceManager();
        VideoDeviceManager.GetInstance(AgoraJoin.mRtcEngine).GetVideoDevice(0, ref videoDeviceName, ref videoDeviceID);
        videoDeviceText.text = videoDeviceName;
    }

    void OnUserJoinedHandler(uint uid, int elapsed)
    {
        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);
        // this is called in main thread

        // find a game object to render video stream from 'uid'
        GameObject go = GameObject.Find(uid.ToString());
        if (go != null)
        {
            return;
        }

        // create a GameObject and assign to this new user
        VideoSurface videoSurface = makeImageSurface(uid.ToString());
        if (!ReferenceEquals(videoSurface, null))
        {
            // configure videoSurface
            videoSurface.SetForUser(uid);
            videoSurface.SetEnable(true);
            videoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
            videoSurface.SetGameFps(30);
        }

        usersInChannel++;
    }

    private const float Offset = 100;
    public VideoSurface makeImageSurface(string goName)
    {
        GameObject go = new GameObject();

        if (go == null)
        {
            return null;
        }

        go.name = goName;

        // to be renderered onto
        go.AddComponent<RawImage>();

        // make the object draggable
        //go.AddComponent<UIElementDragger>();
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

    void OnUserOfflineHandler(uint uid, USER_OFFLINE_REASON reason)
    {
        usersInChannel--;
    }
}

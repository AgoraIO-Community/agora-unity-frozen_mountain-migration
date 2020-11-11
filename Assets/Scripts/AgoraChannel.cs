using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;
using UnityEngine.UI;

public class AgoraChannel : MonoBehaviour
{
    public GameObject localVideoImage;


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
            AgoraJoin.mRtcEngine.DisableVideoObserver();
        }
    }

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        VideoSurface localVideoSurface = localVideoImage.AddComponent<VideoSurface>();

        localVideoSurface.SetForUser(uid);
        localVideoSurface.SetEnable(true);
        localVideoSurface.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
        localVideoSurface.SetGameFps(30);

        // populate main video feed with local
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
        float xPos = Random.Range(Offset - Screen.width / 2f, Screen.width / 2f - Offset);
        float yPos = Random.Range(Offset, Screen.height / 2f - Offset);
        go.transform.localPosition = new Vector3(xPos, yPos, 0f);
        go.transform.localScale = new Vector3(3f, 4f, 1f);

        // configure videoSurface
        VideoSurface videoSurface = go.AddComponent<VideoSurface>();
        return videoSurface;
    }

    void OnUserOfflineHandler(uint uid, USER_OFFLINE_REASON reason)
    {

    }
}

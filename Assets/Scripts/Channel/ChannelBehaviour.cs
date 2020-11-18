using FM.LiveSwitch;
using FM.LiveSwitch.Unity;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using agora_gaming_rtc;

public class ChannelBehaviour : MonoBehaviour
{
    // AGORA FIELDS
    public GameObject localVideoImage;
    public Transform spawnPoint;
    public Text audioDeviceText;
    public Text videoDeviceText;
    public Text channelIDText;

    private int usersInChannel = 0;
    private bool isLocalVideoEnabled;

    // Frozen Mountain fields
    //static ILog _Log = Log.GetLogger(typeof(ChannelBehaviour));

    //private GameObject pnlContainer;
    //private Button btnLeave;
    //private Text txtChannelId;
    //private Dropdown cmbAudioDevice;
    //private Dropdown cmbVideoDevice;
    //private Text txtLog;
    //private GameObject localPanel;
    //private GameObject cnvSendEncoding;
    //private Toggle muteAudio;
    //private Toggle muteVideo;
    //private Toggle disableAudio;
    //private Toggle disableVideo;
    //private Dropdown cmbSendEncodings;
    //private GameObject remotePanel;
    //private GameObject cnvRecvEncoding;
    //private Toggle remoteDisableAudio;
    //private Toggle remoteDisableVideo;
    //private Dropdown cmbRecvEncodings;


    //private readonly string _GatewayUrl = "https://v1.liveswitch.fm:8443/sync";
    //private readonly string _ApplicationId = "my-app-id";
    //private readonly string _SharedSecret = "--replaceThisWithYourOwnSharedSecret--";
    //private readonly string _UserId = Guid.NewGuid().ToString().Replace("-", "");
    //private readonly string _DeviceId = Guid.NewGuid().ToString().Replace("-", "");

    //private string _Name;
    //private string _ChannelId;
    //private bool _AudioOnly;
    //private bool _ReceiveOnly;
    //private bool _Simulcast;
    //private bool _CaptureScreen;
    //private Mode _Mode;

    //private Client _Client;
    //private Channel _Channel;

    //private McuConnection _McuConnection;
    //private SfuUpstreamConnection _SfuUpstreamConnection;
    //private Dictionary<string, SfuDownstreamConnection> _SfuDownStreamConnections;
    //private Dictionary<string, PeerConnection> _PeerConnections;
    //private Dictionary<string, ManagedConnection> _RemoteMediaMaps;
    //private Dictionary<string, RectTransform> _RemoteViews;
    //public Dictionary<string, bool> _LocalAVMaps;
    //public Dictionary<string, bool> _RemoteAVMaps;
    //public Dictionary<string, int[]> _RemoteEncodingMaps;
    //public List<bool> sendEncodings;
    //public List<string> encodings;
    //public string currentId = "local";

    void OnEnable()
    {
        Register();
        //pnlContainer = GameObject.Find("pnlContainer");
        //btnLeave = GameObject.Find("btnLeave").GetComponent<Button>();
        //txtChannelId = GameObject.Find("txtChannelId").GetComponent<Text>();
        //cmbAudioDevice = GameObject.Find("cmbAudioDevice").GetComponent<Dropdown>();
        //cmbVideoDevice = GameObject.Find("cmbVideoDevice").GetComponent<Dropdown>();
        //txtLog = GameObject.Find("txtLog").GetComponent<Text>();

        //// items for local context menu
        //localPanel = GameObject.Find("localPanel");
        //cnvSendEncoding = GameObject.Find("cnvSendEncoding");
        //muteAudio = GameObject.Find("MuteAudio").GetComponent<Toggle>();
        //muteAudio.onValueChanged.AddListener((value) =>
        //{
        //    ToggleMuteAudio("local");
        //});
        //muteVideo = GameObject.Find("MuteVideo").GetComponent<Toggle>();
        //muteVideo.onValueChanged.AddListener((value) =>
        //{
        //   ToggleMuteVideo("local");
        //});
        //disableAudio = GameObject.Find("DisableAudio").GetComponent<Toggle>();
        //disableAudio.onValueChanged.AddListener((value) =>
        //{
        //    ToggleLocalDisableAudio("local");
        //});
        //disableVideo = GameObject.Find("DisableVideo").GetComponent<Toggle>();
        //disableVideo.onValueChanged.AddListener((value) =>
        //{
        //    ToggleLocalDisableVideo("local");
        //});
        //cmbSendEncodings = GameObject.Find("SendEncodings").GetComponent<Dropdown>();

        ////items for remote context menu
        //remotePanel = GameObject.Find("remotePanel");
        //cnvRecvEncoding = GameObject.Find("cnvRecvEncoding");
        //remoteDisableAudio = GameObject.Find("remoteDisableAudio").GetComponent<Toggle>();
        //remoteDisableAudio.onValueChanged.AddListener((value) =>
        //{
        //    ToggleRemoteDisableAudio();
        //});
        //remoteDisableVideo = GameObject.Find("remoteDisableVideo").GetComponent<Toggle>();
        //remoteDisableVideo.onValueChanged.AddListener((value) =>
        //{
        //    ToggleRemoteDisableVideo();
        //});
        //cmbRecvEncodings = GameObject.Find("RecvEncoding").GetComponent<Dropdown>();


        //_Name = JoinInfo.Name;
        //_ChannelId = JoinInfo.ChannelId;
        //_AudioOnly = JoinInfo.AudioOnly;
        //_ReceiveOnly = JoinInfo.ReceiveOnly;
        //_Simulcast = JoinInfo.Simulcast;
        //_CaptureScreen = JoinInfo.CaptureScreen;
        //_Mode = JoinInfo.Mode;

        //txtChannelId.text = _ChannelId;

        //_SfuDownStreamConnections = new Dictionary<string, SfuDownstreamConnection>();
        //_PeerConnections = new Dictionary<string, PeerConnection>();
        //_RemoteMediaMaps = new Dictionary<string, ManagedConnection>();
        //_RemoteViews = new Dictionary<string, RectTransform>();
        //_LocalAVMaps = new Dictionary<string, bool>();
        //_RemoteAVMaps = new Dictionary<string, bool>();
        //_RemoteEncodingMaps = new Dictionary<string, int[]>();
        //sendEncodings = new List<bool>();
        //encodings = new List<string>();
        //localPanel.SetActive(false);
        //remotePanel.SetActive(false);


        //var userAuthorization = UserAuthorization.Microphone;
        //if (!_AudioOnly)
        //{
        //    userAuthorization |= UserAuthorization.WebCam;
        //}

        //yield return Application.RequestUserAuthorization(userAuthorization);

        //Log.RegisterProvider(new FM.LiveSwitch.Unity.DebugLogProvider(LogLevel.Debug));
        //Log.RegisterProvider(new FM.LiveSwitch.Unity.TextLogProvider(txtLog, LogLevel.Info));

        //yield return Join().AsIEnumerator();

        //btnLeave.interactable = true;
    }

//    private LocalMedia _LocalMedia;
//    private AecContext _AecContext;
//    private LayoutManager _LayoutManager;

//    private VideoLayout _McuVideoLayout;
//    private string _McuConnectionId;
//    private string _McuViewId;

//    private const int INITIAL_REGISTER_BACKOFF = 200; // milliseconds
//    private const int MAX_REGISTER_BACKOFF = 60000; // milliseconds

//    private bool CAPTURE_SCREEN_WITH_TEXTURE2D = false;

//    private bool _Unregistering = false;
//    private int _RegisterBackoff = INITIAL_REGISTER_BACKOFF;


//    private ConcurrentQueue<Func<Task>> _MainThreadQueue = new ConcurrentQueue<Func<Task>>();

//    public async Task Join()
//    {
//        // create client
//        _Client = new Client(_GatewayUrl, _ApplicationId, _UserId, _DeviceId)
//        {
//            Tag = _Mode.ToString(),
//            DeviceAlias = "a-device-alias",
//            UserAlias = _Name
//        };

//        // log client state
//        _Client.OnStateChange += async (client) =>
//        {
//            _Log.Info(client.Id, $"Client is {client.State.ToString().ToLower()}.");

//            if (client.State == ClientState.Unregistered && !_Unregistering)
//            {
//                // apply retry backoff
//                await Task.Delay(_RegisterBackoff);

//                // increase register backoff
//                _RegisterBackoff = Math.Min(_RegisterBackoff * 2, MAX_REGISTER_BACKOFF);

//                // re-register/join
//                _MainThreadQueue.Enqueue(async () =>
//                {
//                    try
//                    {
//                        await Register();

//                        // reset backoff
//                        _RegisterBackoff = INITIAL_REGISTER_BACKOFF;
//                    }
//                    catch (Exception ex)
//                    {
//                        _Log.Error("Failed to re-register/join.", ex);
//                    }
//                });
//            }
//        };

//        // register/join
//        await Register();
//    }

    void Register()
    {
        //        // IMPORTANT: recommended to generate this server-side so your shared secret stays secret
        //        var registerToken = Token.GenerateClientRegisterToken(_Client, _ChannelId, _SharedSecret);

        //        // register (and join at the same time)
        //        var channels = await _Client.Register(registerToken);

        //        // get the channel reference
        //        _Channel = channels.First();

        //        //TODO: text messaging

        // AGORA NOTE: Joining Agora callbacks
        if(JoinBehaviour.mRtcEngine != null)
        {
            print("joining Agora callbacks");
            JoinBehaviour.mRtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccessHandler;
            JoinBehaviour.mRtcEngine.OnUserJoined += OnUserJoinedHandler;
            JoinBehaviour.mRtcEngine.OnUserOffline += OnUserOfflineHandler;
        }
//        // log channel events
//        _Channel.OnRemoteClientJoin += (remoteClientInfo) =>
//        {
//            _Log.Info(remoteClientInfo.Id, $"Remote client joined: {remoteClientInfo.ToJson()}");
//        };
//        _Channel.OnRemoteClientUpdate += (remoteClientInfo, newRemoteClientInfo) =>
//        {
//            _Log.Info(remoteClientInfo.Id, $"Remote client updated: {newRemoteClientInfo.ToJson()}");
//        };
//        _Channel.OnRemoteClientLeave += (remoteClientInfo) =>
//        {
//            _Log.Info(remoteClientInfo.Id, $"Remote client left: {remoteClientInfo.ToJson()}");
//        };
//        _Channel.OnRemoteUpstreamConnectionOpen += (remoteConnectionInfo) =>
//        {
//            _Log.Info(remoteConnectionInfo.Id, $"Remote upstream connection opened: {remoteConnectionInfo.ToJson()}");
//        };
//        _Channel.OnRemoteUpstreamConnectionUpdate += (remoteConnectionInfo, newRemoteConnectionInfo) =>
//        {
//            _Log.Info(remoteConnectionInfo.Id, $"Remote upstream connection updated: {newRemoteConnectionInfo.ToJson()}");
//        };
//        _Channel.OnRemoteUpstreamConnectionClose += (remoteConnectionInfo) =>
//        {
//            _Log.Info(remoteConnectionInfo.Id, $"Remote upstream connection closed: {remoteConnectionInfo.ToJson()}");
//        };

//#if !UNITY_EDITOR_OSX && !UNITY_STANDALONE_OSX && !UNITY_IOS
//        // create AEC context (optional)
//        _AecContext = new AecContext();
//#endif

//        // create layout manager (optional)
//        _LayoutManager = new LayoutManager(pnlContainer.GetComponent<RectTransform>());

//        if (!_ReceiveOnly)
//        {
//            // create local media
//            if (_CaptureScreen)
//            {
//                if (CAPTURE_SCREEN_WITH_TEXTURE2D)
//                {
//                    _LocalMedia = new LocalTexture2DMedia(disableAudio: false, disableVideo: _AudioOnly, aecContext: _AecContext);
//                }
//                else
//                {
//                    _LocalMedia = new LocalScreenMedia(disableAudio: false, disableVideo: _AudioOnly, aecContext: _AecContext);
//                }
//            }
//            else
//            {
//                _LocalMedia = new LocalCameraMedia(disableAudio: false, disableVideo: _AudioOnly, enableSimulcast: _Simulcast, aecContext: _AecContext);
//            }

//            // add local preview to layout (if applicable)
//            _LayoutManager.SetLocalView(_LocalMedia.View);

//            var button = pnlContainer.gameObject.AddComponent<LongPressButton>();
//            button.GetComponent<RectTransform>().SetParent(pnlContainer.transform);
//            button.onRightClick.AddListener(() => OnRightClick());
//            button.onLeftClick.AddListener(() => OnLeftClick());

//            InitLocalMaps("local", _LocalMedia.VideoEncodings);

//            // start local media
//            await _LocalMedia.Start();

//            // wire up device selection
//            var audioDevices = await _LocalMedia.GetAudioSourceInputs();
//            if (audioDevices.Length > 0)
//            {
//                cmbAudioDevice.AddOptions(audioDevices.Select(audioDevice => new Dropdown.OptionData(audioDevice.Name)).ToList());
//                cmbAudioDevice.onValueChanged.AddListener(async (i) =>
//                {
//                    await _LocalMedia?.ChangeAudioSourceInput(audioDevices[i]);
//                });
//                cmbAudioDevice.interactable = true;
//            }

//            if (!_AudioOnly)
//            {
//                var videoDevices = await _LocalMedia.GetVideoSourceInputs();
//                if (videoDevices.Length > 0)
//                {
//                    cmbVideoDevice.AddOptions(videoDevices.Select(videoDevice => new Dropdown.OptionData(videoDevice.Name)).ToList());
//                    cmbVideoDevice.onValueChanged.AddListener(async (i) =>
//                    {
//                        await _LocalMedia?.ChangeVideoSourceInput(videoDevices[i]);
//                    });
//                    cmbVideoDevice.interactable = true;
//                }
//            }
//        }

//        if (_Mode == Mode.Mcu)
//        {
//            // track MCU layout changes
//            _Channel.OnMcuVideoLayout += (mcuVideoLayout) =>
//            {
//                if (!_ReceiveOnly)
//                {
//                    // store new MCU layout
//                    _McuVideoLayout = mcuVideoLayout;
//                    _MainThreadQueue.Enqueue(() =>
//                    {
//                        // recalculate MCU layout
//                        _LayoutManager.Layout();
//                        return Task.CompletedTask;
//                    });
//                }
//            };

//            // float local preview
//            _LayoutManager.OnLayout += (layout) =>
//            {
//                if (_McuVideoLayout != null && _McuConnectionId != null && _McuViewId != null)
//                {
//                    FM.LiveSwitch.LayoutUtility.FloatLocalPreview(layout, _McuVideoLayout, _McuConnectionId, _McuViewId);
//                }
//            };

//            // open MCU connection
//            _MainThreadQueue.Enqueue(OpenMcuConnection);
//        }
//        else if (_Mode == Mode.Sfu)
//        {
//            _Channel.OnRemoteUpstreamConnectionOpen += (remoteConnectionInfo) =>
//            {
//                // open SFU downstream connection
//                _MainThreadQueue.Enqueue(() => OpenSfuDownstreamConnection(remoteConnectionInfo));
//            };

//            foreach (var remoteConnectionInfo in _Channel.RemoteUpstreamConnectionInfos)
//            {
//                // open SFU downstream connection
//                _MainThreadQueue.Enqueue(() => OpenSfuDownstreamConnection(remoteConnectionInfo));
//            }

//            if (!_ReceiveOnly)
//            {
//                // open SFU upstream connection
//                _MainThreadQueue.Enqueue(OpenSfuUpstreamConnection);
//            }
//        }
//        else if (_Mode == Mode.Peer)
//        {
//            _Channel.OnPeerConnectionOffer += (peerConnectionOffer) =>
//            {
//                // open peer connection (new client)
//                _MainThreadQueue.Enqueue(() => OpenPeerConnection(peerConnectionOffer));
//            };

//            foreach (var remoteClientInfo in _Channel.RemoteClientInfos)
//            {
//                // open peer connection (existing client)
//                _MainThreadQueue.Enqueue(() => OpenPeerConnection(remoteClientInfo));
//            }
//        }
    }

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        localVideoImage.AddComponent<VideoSurface>();

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

        isLocalVideoEnabled = true;
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

    public void Leave()
    {
        if (JoinBehaviour.mRtcEngine != null)
        {
            JoinBehaviour.mRtcEngine.OnJoinChannelSuccess -= OnJoinChannelSuccessHandler;
            JoinBehaviour.mRtcEngine.OnUserJoined -= OnUserJoinedHandler;
            JoinBehaviour.mRtcEngine.OnUserOffline -= OnUserOfflineHandler;

            JoinBehaviour.mRtcEngine.LeaveChannel();
            JoinBehaviour.mRtcEngine.DisableVideo();
            JoinBehaviour.mRtcEngine.DisableVideoObserver();
        }

        //        btnLeave.interactable = false;
        //        cmbAudioDevice.interactable = false;
        //        cmbVideoDevice.interactable = false;

        //        if (_LayoutManager != null)
        //        {
        //            _LayoutManager.Destroy();
        //            _LayoutManager = null;
        //        }

        //        if (_LocalMedia != null)
        //        {
        //            await _LocalMedia.Stop();
        //            _LocalMedia.Destroy();
        //            _LocalMedia = null;
        //        }

        //        if (_Client != null)
        //        {
        //            await _Client.Unregister();
        //            _Client = null;
        //        }

        SceneManager.LoadScene("Join");
    }

    //    public async void Update()
    //    {
    //        var count = _MainThreadQueue.Count;
    //        for (var i = 0; i < count; i++)
    //        {
    //            if (_MainThreadQueue.TryDequeue(out var action))
    //            {
    //                await action();
    //            }
    //        }

    //    private async Task OpenMcuConnection()
    //    {
    //        // create remote media
    //        var remoteMedia = new RemoteMedia(disableAudio: false, disableVideo: _AudioOnly, aecContext: _AecContext);

    //        // add remote view to layout (if applicable)
    //        _LayoutManager?.AddRemoteView(remoteMedia.Id, remoteMedia.View);


    //        // create streams
    //        var audioStream = new AudioStream(_LocalMedia, remoteMedia);
    //        var videoStream = _AudioOnly ? null : new VideoStream(_LocalMedia, remoteMedia);
    //        if (_Simulcast)
    //        {
    //            videoStream.SimulcastMode = SimulcastMode.RtpStreamId;
    //        }

    //        // create connection
    //        var mcuConnection = _Channel?.CreateMcuConnection(audioStream, videoStream);
    //        _RemoteViews.Add(remoteMedia.Id, remoteMedia.View);
    //        _McuConnection = mcuConnection;

    //        // log connection state
    //        mcuConnection.OnStateChange += (connection) =>
    //        {
    //            _Log.Info(connection.Id, $"MCU connection is {connection.State.ToString().ToLower()}.");

    //            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Failed)
    //            {
    //                // remove remote view from layout (if applicable)
    //                _LayoutManager?.RemoveRemoteView(remoteMedia.Id);
    //                _RemoteViews.Remove(remoteMedia.Id);
    //                _McuConnection = null;
    //                // destroy remote media
    //                _MainThreadQueue.Enqueue(() =>
    //                {
    //                    remoteMedia.Destroy();
    //                    return Task.CompletedTask;
    //                });
    //            }

    //            // retry on failure
    //            if (connection.State == ConnectionState.Failed)
    //            {
    //                _MainThreadQueue?.Enqueue(OpenMcuConnection);
    //            }
    //        };

    //        // cache these for local preview float
    //        _McuConnectionId = mcuConnection.Id;
    //        _McuViewId = remoteMedia.Id;

    //        // open connection
    //        await mcuConnection.Open();
    //    }

    //    private async Task OpenSfuUpstreamConnection()
    //    {
    //        // create streams
    //        var audioStream = new AudioStream(_LocalMedia, remoteMedia: null);
    //        var videoStream = _AudioOnly ? null : new VideoStream(_LocalMedia);
    //        if (_Simulcast)
    //        {
    //            videoStream.SimulcastMode = SimulcastMode.RtpStreamId;
    //        }
    //        // create connection
    //        var sfuUpstreamConnection = _Channel?.CreateSfuUpstreamConnection(audioStream, videoStream);
    //        _SfuUpstreamConnection = sfuUpstreamConnection;
    //        // log connection state
    //        sfuUpstreamConnection.OnStateChange += (connection) =>
    //        {
    //            _Log.Info(connection.Id, $"SFU upstream connection is {connection.State.ToString().ToLower()}.");
    //            if (connection.State == ConnectionState.Closing || connection.State == ConnectionState.Failing)
    //            {
    //                if (connection.RemoteClosed)
    //                {
    //                    Log.Info(string.Format("{0}: Media server closed the connection.", connection.Id));
    //                }
    //                _SfuUpstreamConnection = null;
    //            }
    //            // retry on failure
    //            if (connection.State == ConnectionState.Failed)
    //            {
    //                _MainThreadQueue?.Enqueue(OpenSfuUpstreamConnection);
    //            }
    //        };

    //        // open connection
    //        await sfuUpstreamConnection.Open();
    //    }

    //    private async Task OpenSfuDownstreamConnection(ConnectionInfo remoteConnectionInfo)
    //    {
    //        if (_AudioOnly && !remoteConnectionInfo.HasAudio)
    //        {
    //            // not a match
    //            return;
    //        }

    //        // create remote media
    //        var remoteMedia = new RemoteMedia(disableAudio: false, disableVideo: _AudioOnly, aecContext: _AecContext);
    //        // add remote view to layout (if applicable)
    //        _LayoutManager?.AddRemoteView(remoteMedia.Id, remoteMedia.View);

    //        // create streams
    //        var audioStream = new AudioStream(localMedia: null, remoteMedia);
    //        var videoStream = _AudioOnly ? null : new VideoStream(localMedia: null, remoteMedia);

    //        // create connection
    //        var sfuDownstreamConnection = _Channel?.CreateSfuDownstreamConnection(remoteConnectionInfo, audioStream, videoStream);
    //        _SfuDownStreamConnections.Add(remoteMedia.Id, sfuDownstreamConnection);
    //        _RemoteMediaMaps.Add(remoteMedia.Id, sfuDownstreamConnection);
    //        _RemoteViews.Add(remoteMedia.Id, remoteMedia.View);
    //        InitRemoteMaps(remoteMedia.Id, remoteConnectionInfo.VideoStream.SendEncodings);

    //        // log connection state
    //        sfuDownstreamConnection.OnStateChange += (connection) =>
    //        {
    //            _Log.Info(connection.Id, $"SFU downstream connection is {connection.State.ToString().ToLower()}. ({remoteConnectionInfo.ToJson()})");

    //            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Failed)
    //            {
    //                // remove remote view from layout (if applicable)
    //                _LayoutManager?.RemoveRemoteView(remoteMedia.Id);
    //                _SfuDownStreamConnections.Remove(remoteMedia.Id);
    //                _RemoteMediaMaps.Remove(remoteMedia.Id);
    //                _RemoteViews.Remove(remoteMedia.Id);
    //                ClearRemoteMaps(remoteMedia.Id);
    //                // destroy remote media
    //                _MainThreadQueue.Enqueue(() =>
    //                {
    //                    remoteMedia.Destroy();
    //                    return Task.CompletedTask;
    //                });
    //            }

    //            // retry on failure
    //            if (connection.State == ConnectionState.Failed)
    //            {
    //                _MainThreadQueue?.Enqueue(() => OpenSfuDownstreamConnection(sfuDownstreamConnection.RemoteConnectionInfo));
    //            }
    //        };

    //        // open connection
    //        await sfuDownstreamConnection.Open();
    //    }

    //    private async Task OpenPeerConnection(ClientInfo remoteClientInfo)
    //    {
    //        // create remote media
    //        var remoteMedia = new RemoteMedia(disableAudio: false, disableVideo: _AudioOnly, aecContext: _AecContext);

    //        // add remote view to layout (if applicable)
    //        _LayoutManager?.AddRemoteView(remoteMedia.Id, remoteMedia.View);

    //        // create streams
    //        var audioStream = new AudioStream(_LocalMedia, remoteMedia);
    //        var videoStream = _AudioOnly ? null : new VideoStream(_LocalMedia, remoteMedia);

    //        // create connection
    //        var peerConnection = _Channel?.CreatePeerConnection(remoteClientInfo, audioStream, videoStream);
    //        _PeerConnections.Add(peerConnection.Id, peerConnection);
    //        _RemoteMediaMaps.Add(remoteMedia.Id, peerConnection);
    //        _RemoteViews.Add(remoteMedia.Id, remoteMedia.View);

    //        // log connection state
    //        peerConnection.OnStateChange += (connection) =>
    //        {
    //            _Log.Info(connection.Id, $"Peer connection (offering role) is {connection.State.ToString().ToLower()}. ({remoteClientInfo.ToJson()})");

    //            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Failed)
    //            {
    //                // remove remote view from layout (if applicable)
    //                _LayoutManager?.RemoveRemoteView(remoteMedia.Id);
    //                _PeerConnections.Remove(connection.Id);
    //                _RemoteMediaMaps.Remove(remoteMedia.Id);
    //                _RemoteViews.Remove(remoteMedia.Id);
    //                // destroy remote media
    //                _MainThreadQueue.Enqueue(() =>
    //                {
    //                    remoteMedia.Destroy();
    //                    return Task.CompletedTask;
    //                });
    //            }

    //            // retry on failure
    //            if (connection.State == ConnectionState.Failed)
    //            {
    //                _MainThreadQueue?.Enqueue(() => OpenPeerConnection(remoteClientInfo));
    //            }
    //        };

    //        // open connection
    //        await peerConnection.Open();
    //    }

    //    private async Task OpenPeerConnection(PeerConnectionOffer peerConnectionOffer)
    //    {
    //        if (_AudioOnly && !peerConnectionOffer.HasAudio)
    //        {
    //            // not a match
    //            await peerConnectionOffer.Reject();
    //            return;
    //        }

    //        // create remote media
    //        var remoteMedia = new RemoteMedia(disableAudio: false, disableVideo: _AudioOnly, aecContext: _AecContext);
    //        // add remote view to layout (if applicable)
    //        _LayoutManager?.AddRemoteView(remoteMedia.Id, remoteMedia.View);

    //        // create streams
    //        var audioStream = new AudioStream(_LocalMedia, remoteMedia);
    //        var videoStream = _AudioOnly ? null : new VideoStream(_LocalMedia, remoteMedia);

    //        // create connection
    //        var peerConnection = _Channel?.CreatePeerConnection(peerConnectionOffer, audioStream, videoStream);
    //        _PeerConnections.Add(peerConnection.Id, peerConnection);
    //        _RemoteMediaMaps.Remove(remoteMedia.Id);
    //        _RemoteViews.Add(remoteMedia.Id, remoteMedia.View);

    //        // log connection state
    //        peerConnection.OnStateChange += (connection) =>
    //        {
    //            _Log.Info(connection.Id, $"Peer connection (answering role) is {connection.State.ToString().ToLower()}. ({peerConnectionOffer.RemoteClientInfo.ToJson()})");

    //            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Failed)
    //            {
    //                // remove remote view from layout (if applicable)
    //                _LayoutManager?.RemoveRemoteView(remoteMedia.Id);
    //                _PeerConnections.Remove(connection.Id);
    //                _RemoteMediaMaps.Remove(remoteMedia.Id);
    //                _RemoteViews.Remove(remoteMedia.Id);
    //                // destroy remote media
    //                _MainThreadQueue.Enqueue(() =>
    //                {
    //                    remoteMedia.Destroy();
    //                    return Task.CompletedTask;
    //                });
    //            }
    //        };

    //        // open connection
    //        await peerConnection.Open();
    //    }

    //    private void LateUpdate()
    //    {
    //        if (CAPTURE_SCREEN_WITH_TEXTURE2D)
    //        {
    //            StartCoroutine(CaptureScreenAsTexture2D());
    //        }
    //    }

    //    private IEnumerator CaptureScreenAsTexture2D()
    //    {
    //        yield return new WaitForEndOfFrame();

    //        if (_CaptureScreen)
    //        {
    //            var localTexture2DMedia = _LocalMedia as LocalTexture2DMedia;
    //            if (localTexture2DMedia != null)
    //            {
    //                localTexture2DMedia.Texture2D = ScreenCapture.CaptureScreenshotAsTexture();
    //            }
    //        }
    //    }

    //    public void ToggleSendEncoding(int value)
    //    {
    //        if (value > sendEncodings.Count - 1)
    //        {
    //            for (var i = 0; i < sendEncodings.Count; i++)
    //            {
    //                sendEncodings[i] = true;
    //            }
    //        }
    //        else
    //        {
    //            for (var i = 0; i < sendEncodings.Count; i++)
    //            {

    //                sendEncodings[i] = i == value;
    //            }
    //        }
    //        var encodings = _LocalMedia.VideoEncodings;
    //        if (encodings != null && encodings.Length > 1)
    //        {
    //            for (var i = 0; i < encodings.Length; i++)
    //            {
    //                encodings[i].Deactivated = !sendEncodings[i];
    //            }
    //        }
    //        _LocalMedia.VideoEncodings = encodings;
    //        localPanel.SetActive(false);
    //    }

    //    public void ToggleReceiveEncoding(int receiveEncoding)
    //    {
    //        var id = currentId;
    //        var connection = _SfuDownStreamConnections[id];
    //        var encodings = connection.RemoteConnectionInfo.VideoStream.SendEncodings;
    //        if (encodings != null && encodings.Length > 1 && receiveEncoding < encodings.Length)
    //        {
    //            var encoding = encodings[receiveEncoding];
    //            var config = connection.Config;
    //            config.RemoteVideoEncoding = encodings[receiveEncoding];
    //            connection.Update(config).Then((__) =>
    //            {
    //                Log.Debug("Set remote encoding for connection " + connection.Id + ": " + encoding.ToString());
    //            }).Fail((ex) =>
    //            {
    //                Log.Error("Could not set remote encoding for connection " + connection.Id + ": " + encoding.ToString());
    //            });
    //        }
    //        // update _RemoteEncodingMaps
    //        foreach (KeyValuePair<string, int[]> entry in _RemoteEncodingMaps)
    //        {
    //            if (entry.Key.Contains(id))
    //            {
    //                if (entry.Value[0] == receiveEncoding)
    //                {
    //                    _RemoteEncodingMaps[entry.Key][1] = 1;
    //                }
    //                else
    //                {
    //                    _RemoteEncodingMaps[entry.Key][1] = 0;
    //                }
    //            }
    //        }
    //        remotePanel.SetActive(false);
    //    }

    public void ToggleLocalDisableVideo()
    {
        if (JoinBehaviour.mRtcEngine != null)
        {
            isLocalVideoEnabled = !isLocalVideoEnabled;

            if (isLocalVideoEnabled)
            {
                JoinBehaviour.mRtcEngine.MuteLocalVideoStream(true);
            }
            else
            {
                JoinBehaviour.mRtcEngine.MuteLocalVideoStream(false);
            }
        }

        //ConnectionConfig config = null;
        //if (_SfuUpstreamConnection != null)
        //{
        //    config = _SfuUpstreamConnection.Config;
        //    config.LocalVideoDisabled = !config.LocalVideoDisabled;
        //    _SfuUpstreamConnection.Update(config);
        //}
        //if (_McuConnection != null)
        //{
        //    config = _McuConnection.Config;
        //    config.LocalVideoDisabled = !config.LocalVideoDisabled;
        //    _McuConnection.Update(config);
        //}
        //foreach (var peerConnection in _PeerConnections.Values)
        //{
        //    config = peerConnection.Config;
        //    config.LocalVideoDisabled = !config.LocalVideoDisabled;
        //    peerConnection.Update(config);
        //}
        //if (config != null)
        //{
        //    _LocalAVMaps[id + "DisableVideo"] = config.LocalVideoDisabled;
        //}
        //localPanel.SetActive(false);
    }

    //    public void ToggleRemoteDisableVideo()
    //    {
    //        var id = currentId;
    //        var downstream = _RemoteMediaMaps[id];
    //        var config = downstream.Config;
    //        config.RemoteVideoDisabled = !config.RemoteVideoDisabled;
    //        downstream.Update(config);
    //        _RemoteAVMaps[id + "DisableVideo"] = config.RemoteVideoDisabled;
    //        remotePanel.SetActive(false);
    //    }

    //    public void ToggleLocalDisableAudio(string id)
    //    {
    //        ConnectionConfig config = null;
    //        if (_SfuUpstreamConnection != null)
    //        {
    //            config = _SfuUpstreamConnection.Config;
    //            config.LocalAudioDisabled = !config.LocalAudioDisabled;
    //            _SfuUpstreamConnection.Update(config);
    //        }
    //        if (_McuConnection != null)
    //        {
    //            config = _McuConnection.Config;
    //            config.LocalAudioDisabled = !config.LocalAudioDisabled;
    //            _McuConnection.Update(config);
    //        }
    //        foreach (var peerConnection in _PeerConnections.Values)
    //        {
    //            config = peerConnection.Config;
    //            config.LocalAudioDisabled = !config.LocalAudioDisabled;
    //            peerConnection.Update(config);
    //        }
    //        if (config != null)
    //        {
    //            _LocalAVMaps[id + "DisableAudio"] = config.LocalAudioDisabled;
    //        }
    //        localPanel.SetActive(false);
    //    }

    //    public void ToggleRemoteDisableAudio()
    //    {
    //        var id = currentId;
    //        var downstream = _RemoteMediaMaps[id];
    //        var config = downstream.Config;
    //        config.RemoteAudioDisabled = !config.RemoteAudioDisabled;
    //        downstream.Update(config);
    //        _RemoteAVMaps[id + "DisableAudio"] = config.RemoteAudioDisabled;
    //        remotePanel.SetActive(false);
    //    }

    //    private void ToggleMuteVideo(string id)
    //    {
    //        ConnectionConfig config = null;
    //        if (_SfuUpstreamConnection != null)
    //        {
    //            config = _SfuUpstreamConnection.Config;
    //            config.LocalVideoMuted = !config.LocalVideoMuted;
    //            _SfuUpstreamConnection.Update(config);
    //        }
    //        if (_McuConnection != null)
    //        {
    //            config = _McuConnection.Config;
    //            config.LocalVideoMuted = !config.LocalVideoMuted;
    //            _McuConnection.Update(config);
    //        }
    //        foreach (var peerConnection in _PeerConnections.Values)
    //        {
    //            config = peerConnection.Config;
    //            config.LocalVideoMuted = !config.LocalVideoMuted;
    //            peerConnection.Update(config);
    //        }
    //        _LocalAVMaps[id + "MuteVideo"] = config.LocalVideoMuted;
    //        localPanel.SetActive(false);
    //    }

    //    private void ToggleMuteAudio(string id)
    //    {
    //        ConnectionConfig config = null;
    //        if (_SfuUpstreamConnection != null)
    //        {
    //            config = _SfuUpstreamConnection.Config;
    //            config.LocalAudioMuted = !config.LocalAudioMuted;
    //            _SfuUpstreamConnection.Update(config);
    //        }
    //        if (_McuConnection != null)
    //        {
    //            config = _McuConnection.Config;
    //            config.LocalAudioMuted = !config.LocalAudioMuted;
    //            _McuConnection.Update(config);
    //        }
    //        foreach (var peerConnection in _PeerConnections.Values)
    //        {
    //            config = peerConnection.Config;
    //            config.LocalAudioMuted = !config.LocalAudioMuted;
    //            peerConnection.Update(config);
    //        }
    //        _LocalAVMaps[id + "MuteAudio"] = config.LocalAudioMuted;
    //        localPanel.SetActive(false);
    //    }

    //    public void InitLocalMaps(string id, VideoEncodingConfig[] encodings)
    //    {
    //        _LocalAVMaps.Clear();
    //        _LocalAVMaps.Add(id + "MuteAudio", false);
    //        _LocalAVMaps.Add(id + "MuteVideo", false);
    //        _LocalAVMaps.Add(id + "DisableAudio", false);
    //        _LocalAVMaps.Add(id + "DisableVideo", false);

    //        this.encodings.Clear();
    //        sendEncodings.Clear();
    //        cmbSendEncodings.options.Clear();
    //        if (_Simulcast && _Mode == Mode.Sfu)
    //        {
    //            cnvSendEncoding.SetActive(true);
    //            cmbSendEncodings.captionText.text = "Select Encodings";
    //            for (var i = 0; i < encodings.Length; i++)
    //            {
    //                var encoding = encodings[i].ToString();
    //                var index = encoding.IndexOf("Bitrate");
    //                if (index != -1 && encoding.Length - index > 28)
    //                {
    //                    encoding = encoding.Substring(index, 28);
    //                }
    //                cmbSendEncodings.options.Add(new Dropdown.OptionData() { text = encoding });

    //                this.encodings.Add(encoding);
    //                sendEncodings.Add(true);
    //            }
    //            var enable_all = new Dropdown.OptionData() { text = "Enable all" };
    //            cmbSendEncodings.options.Add(enable_all);
    //            cmbSendEncodings.value = cmbSendEncodings.options.IndexOf(enable_all);
    //            cmbSendEncodings.onValueChanged.AddListener((value) =>
    //            {
    //                ToggleSendEncoding(value);
    //            });
    //        }
    //        else
    //        {
    //            cnvSendEncoding.SetActive(false);
    //            cmbSendEncodings.enabled = false;
    //        }
    //    }

    //    public void InitRemoteMaps(string id, EncodingInfo[] encodings)
    //    {
    //        _RemoteAVMaps.Add(id + "DisableAudio", false);
    //        _RemoteAVMaps.Add(id + "DisableVideo", false);
    //        for (var i = 0; i < encodings.Length; i++)
    //        {
    //            var encoding = encodings[i].ToString();
    //            var index = encoding.IndexOf("Bitrate");
    //            if (index != -1 && encoding.Length - index > 29)
    //            {
    //                encoding = encoding.Substring(index, 29);
    //            }
    //            var key = id + encoding;
    //            if (i == 0)
    //            {
    //                _RemoteEncodingMaps.Add(key, new int[] { i, 1 });
    //            }
    //            else
    //            {
    //                _RemoteEncodingMaps.Add(key, new int[] { i, 0 });
    //            }
    //        }
    //    }

    //    public void ClearRemoteMaps(string id)
    //    {
    //        _RemoteViews.Clear();
    //        var itemsToRemove = _RemoteEncodingMaps.Where(f => f.Key.Contains(id)).ToArray();
    //        foreach (var item in itemsToRemove)
    //        {
    //            _RemoteEncodingMaps.Remove(item.Key);
    //        }
    //        var itemsToRemove_AV = _RemoteAVMaps.Where(f => f.Key.Contains(id)).ToArray();
    //        foreach (var item in itemsToRemove_AV)
    //        {
    //            _RemoteAVMaps.Remove(item.Key);
    //        }
    //    }

    //public void OnLeftClick()
    //{
    //    localPanel.SetActive(false);
    //    remotePanel.SetActive(false);
    //}

    //public string GetViewId()
    //{
    //    if (RectTransformUtility.RectangleContainsScreenPoint(_LocalMedia.View, Input.mousePosition))
    //    {
    //        return "local";
    //    }
    //    else
    //    {
    //        foreach(var item in _RemoteViews)
    //        {
    //            if (RectTransformUtility.RectangleContainsScreenPoint(item.Value, Input.mousePosition))
    //            {
    //                return item.Key;
    //            }
    //        }
    //    }
    //    return null;
    //}

    //public void OnRightClick()
    //{
    //    currentId = GetViewId();
    //    Debug.Log("View clicked is " + currentId);
    //    if (currentId == "local")
    //    {
    //        //update toggle values and show local context menu
    //        muteAudio.isOn = _LocalAVMaps[currentId + "MuteAudio"];
    //        muteVideo.isOn = _LocalAVMaps[currentId + "MuteVideo"];
    //        disableAudio.isOn = _LocalAVMaps[currentId + "DisableAudio"];
    //        disableVideo.isOn = _LocalAVMaps[currentId + "DisableVideo"];
    //        var menu_transform = localPanel.GetComponent<RectTransform>();
    //        menu_transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 75);
    //        localPanel.SetActive(true);
    //    }
    //    else
    //    {
    //        //update toggle values
    //        remoteDisableAudio.isOn = _RemoteAVMaps[currentId + "DisableAudio"];
    //        remoteDisableVideo.isOn = _RemoteAVMaps[currentId + "DisableVideo"];

    //        //update encoding values since each connection might have different encodings
    //        cmbRecvEncodings.options.Clear();
    //        if (_Simulcast && _Mode == Mode.Sfu)
    //        {
    //            cnvRecvEncoding.SetActive(true);
    //            cmbRecvEncodings.captionText.text = "Select Encoding";
    //            foreach (KeyValuePair<string, int[]> entry in _RemoteEncodingMaps)
    //            {
    //                if (entry.Key.Contains(currentId))
    //                {
    //                    var item = new Dropdown.OptionData() { text = entry.Key.Replace(currentId, "") };
    //                    cmbRecvEncodings.options.Add(item);
    //                    if (entry.Value[1] == 1)
    //                    {
    //                        cmbRecvEncodings.value = cmbRecvEncodings.options.IndexOf(item);
    //                    }
    //                }
    //            }

    //            cmbRecvEncodings.onValueChanged.AddListener((value) =>
    //            {
    //                ToggleReceiveEncoding(value);
    //            });
    //        }
    //        else
    //        {
    //            cnvRecvEncoding.SetActive(false);
    //            cmbRecvEncodings.enabled = false;
    //        }
    //        //display remote menu
    //        var menu_transform = remotePanel.GetComponent<RectTransform>();
    //        menu_transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 75);
    //        remotePanel.SetActive(true);
    //    }
    //}

}

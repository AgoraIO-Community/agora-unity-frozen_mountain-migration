using FM.LiveSwitch;
using LSUnity = FM.LiveSwitch.Unity;
using Matroska = FM.LiveSwitch.Matroska;
using Opus = FM.LiveSwitch.Opus;
using Vp8 = FM.LiveSwitch.Vp8;
using Vp9 = FM.LiveSwitch.Vp9;
using Yuv = FM.LiveSwitch.Yuv;

public abstract class LocalMedia : RtcLocalMedia<UnityEngine.RectTransform>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalMedia"/> class.
    /// </summary>
    /// <param name="disableAudio">Whether to disable audio.</param>
    /// <param name="disableVideo">Whether to disable video.</param>
    /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
    public LocalMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
        : base(disableAudio, disableVideo, aecContext)
    { }

    /// <summary>
    /// Creates an audio recorder.
    /// </summary>
    /// <param name="inputFormat">The input format.</param>
    protected override AudioSink CreateAudioRecorder(AudioFormat inputFormat)
    {
        return new Matroska.AudioSink(Id + "-local-audio-" + inputFormat.Name.ToLower() + ".mkv");
    }

    /// <summary>
    /// Creates an audio source.
    /// </summary>
    /// <param name="config">The configuration.</param>
    protected override AudioSource CreateAudioSource(AudioConfig config)
    {
        return new LSUnity.AudioClipSource(config);
    }

    /// <summary>
    /// Creates an image converter.
    /// </summary>
    /// <param name="outputFormat">The output format.</param>
    protected override VideoPipe CreateImageConverter(VideoFormat outputFormat)
    {
        return new Yuv.ImageConverter(outputFormat);
    }

    /// <summary>
    /// Creates an Opus encoder.
    /// </summary>
    /// <param name="config">The configuration.</param>
    protected override AudioEncoder CreateOpusEncoder(AudioConfig config)
    {
        return new Opus.Encoder(config);
    }

    /// <summary>
    /// Creates a video recorder.
    /// </summary>
    /// <param name="inputFormat">The input format.</param>
    protected override VideoSink CreateVideoRecorder(VideoFormat inputFormat)
    {
        return new Matroska.VideoSink(Id + "-local-video-" + inputFormat.Name.ToLower() + ".mkv");
    }

    /// <summary>
    /// Creates a VP8 encoder.
    /// </summary>
    protected override VideoEncoder CreateVp8Encoder()
    {
        return new Vp8.Encoder();
    }

    /// <summary>
    /// Creates an H.264 encoder.
    /// </summary>
    protected override VideoEncoder CreateH264Encoder()
    {
        // OpenH264 requires a runtime download from Cisco
        // for licensing reasons, which is not currently
        // supported on Unity.
        return null;
    }

    /// <summary>
    /// Creates a VP9 encoder.
    /// </summary>
    protected override VideoEncoder CreateVp9Encoder()
    {
        return new Vp9.Encoder();
    }
}

public class LocalCameraMedia : LocalMedia
{
    private VideoConfig _CameraConfig = new VideoConfig(640, 480, 30);

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalCameraMedia"/> class.
    /// </summary>
    /// <param name="disableAudio">Whether to disable audio.</param>
    /// <param name="disableVideo">Whether to disable video.</param>
    /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
    public LocalCameraMedia(bool disableAudio, bool disableVideo, bool enableSimulcast, AecContext aecContext)
        : base(disableAudio, disableVideo, aecContext)
    {
        VideoSimulcastDisabled = !enableSimulcast;
        Initialize();
    }

    /// <summary>
    /// Creates a video source.
    /// </summary>
    protected override VideoSource CreateVideoSource()
    {
        return new LSUnity.WebCamTextureSource(_CameraConfig);
    }

    /// <summary>
    /// Gets the video source preview.
    /// By default, this returns ViewSink.View,
    /// but WebCamTextureSource can give us an
    /// optimized preview directly.
    /// </summary>
    public override UnityEngine.RectTransform View
    {
        get { return ((LSUnity.WebCamTextureSource)VideoSource)?.View; }
    }

    /// <summary>
    /// Creates a view sink.
    /// </summary>
    protected override ViewSink<UnityEngine.RectTransform> CreateViewSink()
    {
        // The WebCamTextureSource gives us an optimized preview directly.
        // There is no need to route frames to a separate RectTransformSink.
        return null;
    }
}

public class LocalScreenMedia : LocalMedia
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalScreenMedia"/> class.
    /// </summary>
    /// <param name="disableAudio">Whether to disable audio.</param>
    /// <param name="disableVideo">Whether to disable video.</param>
    /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
    public LocalScreenMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
        : base(disableAudio, disableVideo, aecContext)
    {
        Initialize();
    }

    /// <summary>
    /// Creates a video source.
    /// </summary>
    protected override VideoSource CreateVideoSource()
    {
        return new LSUnity.ScreenSource();
    }

    /// <summary>
    /// Creates a view sink.
    /// </summary>
    protected override ViewSink<UnityEngine.RectTransform> CreateViewSink()
    {
        // Screen capture doesn't generally need a preview.
        // If you want one, return a new RectTransformSink here.
        return null;
    }
}

public class LocalTexture2DMedia : LocalMedia
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalTexture2DMedia"/> class.
    /// </summary>
    /// <param name="disableAudio">Whether to disable audio.</param>
    /// <param name="disableVideo">Whether to disable video.</param>
    /// <param name="aecContext">The AEC context, if using software echo cancellation.</param>
    public LocalTexture2DMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
        : base(disableAudio, disableVideo, aecContext)
    {
        Initialize();
    }

    /// <summary>
    /// Creates a video source.
    /// </summary>
    protected override VideoSource CreateVideoSource()
    {
        return new LSUnity.Texture2DSource();
    }

    /// <summary>
    /// Creates a view sink.
    /// </summary>
    protected override ViewSink<UnityEngine.RectTransform> CreateViewSink()
    {
        // Texture capture doesn't generally need a preview.
        // If you want one, return a new RectTransformSink here.
        return null;
    }

    /// <summary>
    /// Gets or sets the underlying Texture2D.
    /// </summary>
    public UnityEngine.Texture2D Texture2D
    {
        get { return ((LSUnity.Texture2DSource)VideoSource).Texture2D; }
        set { ((LSUnity.Texture2DSource)VideoSource).Texture2D = value; }
    }
}

using FM.LiveSwitch;
using LSUnity = FM.LiveSwitch.Unity;
using Matroska = FM.LiveSwitch.Matroska;
using Opus = FM.LiveSwitch.Opus;
using Vp8 = FM.LiveSwitch.Vp8;
using Vp9 = FM.LiveSwitch.Vp9;
using Yuv = FM.LiveSwitch.Yuv;

public class RemoteMedia : RtcRemoteMedia<UnityEngine.RectTransform>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoteMedia"/> class.
    /// </summary>
    /// <param name="disableAudio">if set to <c>true</c> [disable audio].</param>
    /// <param name="disableVideo">if set to <c>true</c> [disable video].</param>
    /// <param name="aecContext">The aec context.</param>
    public RemoteMedia(bool disableAudio, bool disableVideo, AecContext aecContext)
        : base(disableAudio, disableVideo, aecContext)
    {
        Initialize();
    }

    /// <summary>
    /// Creates an audio recorder.
    /// </summary>
    /// <param name="inputFormat">The input format.</param>
    protected override AudioSink CreateAudioRecorder(AudioFormat inputFormat)
    {
        return new Matroska.AudioSink(Id + "-remote-audio-" + inputFormat.Name.ToLower() + ".mkv");
    }

    /// <summary>
    /// Creates an audio sink.
    /// </summary>
    /// <param name="config">The configuration.</param>
    protected override AudioSink CreateAudioSink(AudioConfig config)
    {
        return new LSUnity.AudioClipSink(config);
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
    protected override AudioDecoder CreateOpusDecoder(AudioConfig config)
    {
        return new Opus.Decoder(config);
    }

    /// <summary>
    /// Creates a video recorder.
    /// </summary>
    /// <param name="inputFormat">The input format.</param>
    protected override VideoSink CreateVideoRecorder(VideoFormat inputFormat)
    {
        return new Matroska.VideoSink(Id + "-remote-video-" + inputFormat.Name.ToLower() + ".mkv");
    }

    /// <summary>
    /// Creates a view sink.
    /// </summary>
    protected override ViewSink<UnityEngine.RectTransform> CreateViewSink()
    {
        return new LSUnity.RectTransformSink();
    }

    /// <summary>
    /// Creates a VP8 encoder.
    /// </summary>
    protected override VideoDecoder CreateVp8Decoder()
    {
        return new Vp8.Decoder();
    }

    /// <summary>
    /// Creates an H.264 encoder.
    /// </summary>
    protected override VideoDecoder CreateH264Decoder()
    {
        // OpenH264 requires a runtime download from Cisco
        // for licensing reasons, which is not currently
        // supported on Unity.
        return null;
    }

    /// <summary>
    /// Creates a VP9 encoder.
    /// </summary>
    protected override VideoDecoder CreateVp9Decoder()
    {
        return new Vp9.Decoder();
    }
}
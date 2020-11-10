using FM.LiveSwitch;
using FM.LiveSwitch.AudioProcessing;
using FM.LiveSwitch.Unity;

public class AecContext : FM.LiveSwitch.AecContext
{
    /// <summary>
    /// Creates an acoustic echo cancellation processor.
    /// </summary>
    protected override AecPipe CreateProcessor()
    {
        var config = new AudioConfig(16000, 1);
        return new AecProcessor(config, AudioClipSource.GetBufferDelay(config) + AudioClipSink.GetBufferDelay(config));
    }

    /// <summary>
    /// Creates an output mixer sink.
    /// </summary>
    /// <param name="config">The configuration.</param>
    protected override AudioSink CreateOutputMixerSink(AudioConfig config)
    {
        return new AudioClipSink(config);
    }
}

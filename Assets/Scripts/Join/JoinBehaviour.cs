using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinBehaviour : MonoBehaviour
{
    private InputField txtName;
    private InputField txtChannelId;
    private Toggle chkAudioOnly;
    private Toggle chkReceiveOnly;
    private Toggle chkCaptureScreen;
    private Toggle chkSimulcast;
    private Dropdown cmbMode;
    private Button btnJoin;

    public void Start()
    {
        txtName = GameObject.Find("txtName").GetComponent<InputField>();
        txtChannelId = GameObject.Find("txtChannelId").GetComponent<InputField>();
        chkAudioOnly = GameObject.Find("chkAudioOnly").GetComponent<Toggle>();
        chkReceiveOnly = GameObject.Find("chkReceiveOnly").GetComponent<Toggle>();
        chkCaptureScreen = GameObject.Find("chkCaptureScreen").GetComponent<Toggle>();
        chkSimulcast = GameObject.Find("chkSimulcast").GetComponent<Toggle>();
        cmbMode = GameObject.Find("cmbMode").GetComponent<Dropdown>();
        btnJoin = GameObject.Find("btnJoin").GetComponent<Button>();

        txtName.text = JoinInfo.Name;
        txtChannelId.text = JoinInfo.ChannelId;
        chkAudioOnly.isOn = JoinInfo.AudioOnly;
        chkReceiveOnly.isOn = JoinInfo.ReceiveOnly;
        chkCaptureScreen.isOn = JoinInfo.CaptureScreen;
        chkSimulcast.isOn = JoinInfo.Simulcast;

        chkAudioOnly.onValueChanged.AddListener(delegate
        {
            AudioOnlyValueChanged();
        });
        chkReceiveOnly.onValueChanged.AddListener(delegate
        {
            ReceiveOnlyValueChanged();
        });
        chkSimulcast.onValueChanged.AddListener(delegate {
            SimulcastValueChanged();
        });
        cmbMode.onValueChanged.AddListener(delegate
        {
            ModeValueChanged();
        });

        switch (JoinInfo.Mode)
        {
            default:
            case Mode.Sfu:
                cmbMode.value = 0;
                break;
            case Mode.Mcu:
                cmbMode.value = 1;
                break;
            case Mode.Peer:
                cmbMode.value = 2;
                break;
        }
    }

    public void Join()
    {
        JoinInfo.Name = txtName.text;
        JoinInfo.ChannelId = txtChannelId.text;
        JoinInfo.AudioOnly = chkAudioOnly.isOn;
        JoinInfo.ReceiveOnly = chkReceiveOnly.isOn;
        JoinInfo.CaptureScreen = chkCaptureScreen.isOn;
        JoinInfo.Simulcast = chkSimulcast.isOn;

        switch (cmbMode.value)
        {
            default:
            case 0:
                JoinInfo.Mode = Mode.Sfu;
                break;
            case 1:
                JoinInfo.Mode = Mode.Mcu;
                break;
            case 2:
                JoinInfo.Mode = Mode.Peer;
                break;
        }

        if (!string.IsNullOrEmpty(JoinInfo.ChannelId))
        {
            SceneManager.LoadSceneAsync("Channel");
        }
    }

    public void SimulcastValueChanged()
    {
        if (chkSimulcast.isOn)
        {
            chkAudioOnly.isOn = false;
            chkReceiveOnly.isOn = false;
            chkAudioOnly.enabled = false;
            chkReceiveOnly.enabled = false;
            cmbMode.value = 0;
        }
        else
        {
            chkAudioOnly.enabled = true;
            chkReceiveOnly.enabled = true;
        }
    }

    public void AudioOnlyValueChanged()
    {
        if (chkAudioOnly.isOn)
        {
            chkSimulcast.isOn = false;
            chkSimulcast.enabled = false;
        }
        else if (!chkAudioOnly.isOn && !chkReceiveOnly.isOn && cmbMode.value != 2)
        {
            chkSimulcast.enabled = true;
        }
    }

    public void ReceiveOnlyValueChanged()
    {
        if (chkReceiveOnly.isOn)
        {
            chkSimulcast.isOn = false;
            chkSimulcast.enabled = false;
        }
        else if (!chkAudioOnly.isOn && !chkReceiveOnly.isOn && cmbMode.value != 2)
        {
            chkSimulcast.enabled = true;
        }
    }

    public void ModeValueChanged()
    {
        if (cmbMode.value == 2)
        {
            chkSimulcast.isOn = false;
            chkSimulcast.enabled = false;
            chkAudioOnly.enabled = true;
            chkReceiveOnly.enabled = true;
        }
        else
        {
            chkSimulcast.enabled = true;
        }
    }

    public void Update()
    {
        btnJoin.interactable = !string.IsNullOrEmpty(txtChannelId.text);
    }
}

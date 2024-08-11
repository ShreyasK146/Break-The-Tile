
using UnityEngine;
using UnityEngine.Audio;

using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
   // [SerializeField] Slider tiltSensSlider;
   // [SerializeField] Slider buttonSensSlider;
    [SerializeField] Slider changeVolumeSlider;
    [SerializeField] Slider musicVolumeSldier;
    [SerializeField] AudioMixer audioMixer;

    //not in use...
    [SerializeField] Button buttonEnable;
    [SerializeField] Button tiltEnable;
    //[SerializeField] TextMeshProUGUI buttonSensText;
    //[SerializeField] TextMeshProUGUI tiltSensText;
    [HideInInspector] public float keySpeed = 100f;
    [HideInInspector] public float tiltSpeed = 10f;


    private void Awake()
    {
        SaveSettings.SaveData s = new SaveSettings.SaveData();
       
        s.LoadValues();


        
    }

    

   /* private void Update()
    {
        if (buttonEnable.gameObject.activeSelf)
        {
            SaveSettings.instance.buttonSensEnabled = true;
        }
        else
            SaveSettings.instance.buttonSensEnabled = false;
    }*/

    private void Start()
    {
        
        if(SaveSettings.instance != null)
        {
           /* tiltSensSlider.value = SaveSettings.instance.tiltSens;
            tiltSpeed = tiltSensSlider.value;
            buttonSensSlider.value = SaveSettings.instance.buttonSens;
            keySpeed = buttonSensSlider.value;*/
            changeVolumeSlider.value = SaveSettings.instance.gameVolume;
            audioMixer.SetFloat("MasterVolume",changeVolumeSlider.value);
            musicVolumeSldier.value = SaveSettings.instance.musicVolume;
            audioMixer.SetFloat("MusicVolume", musicVolumeSldier.value);
     

           /* if (SaveSettings.instance.buttonSensEnabled)
            {
                tiltEnable.gameObject.SetActive(false);
                tiltSensText.gameObject.SetActive(false);
                buttonEnable.gameObject.SetActive(true);
                buttonSensText.gameObject.SetActive(true);
            }
            else
            {
                buttonEnable.gameObject.SetActive(false);
                buttonSensText.gameObject.SetActive(false);
                tiltEnable.gameObject.SetActive(true);
                tiltSensText.gameObject.SetActive(true);
            }*/
        }
    }

   /* public void SaveTiltSensitivity()
    {
        tiltSpeed = tiltSensSlider.value;
        SaveSettings.instance.tiltSens = tiltSensSlider.value;
    }

    public void SaveButtonSensitivity()
    {
        keySpeed = buttonSensSlider.value;
        SaveSettings.instance.buttonSens = buttonSensSlider.value;
    }*/

    public void SaveGameVolume()
    {
        audioMixer.SetFloat("MasterVolume", changeVolumeSlider.value);
        SaveSettings.instance.gameVolume = changeVolumeSlider.value;
    }

    public void SaveMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", musicVolumeSldier.value);
        SaveSettings.instance.musicVolume = musicVolumeSldier.value;
    }



}

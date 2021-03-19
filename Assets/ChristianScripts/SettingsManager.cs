using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenTogg;
    public Dropdown resolutiondrop;
    public Slider musicvolume;

    public Resolution[] resolutions;
    public Button appluButton;
    public GameSettingd gamesettings;
    public AudioSource musicSource;
    public GameObject panneloption;

    private void OnEnable()
    {
        gamesettings = new GameSettingd();
        

        fullscreenTogg.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutiondrop.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        musicvolume.onValueChanged.AddListener(delegate { OnmusicVolume(); });
        appluButton.onClick.AddListener(delegate { OnApplyButtonClick(); });
        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutiondrop.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        LoadSettings();
    }

    public void OnFullscreenToggle()
    {
        gamesettings.fullscreen =   Screen.fullScreen = fullscreenTogg.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutiondrop.value].width, resolutions[resolutiondrop.value].height, Screen.fullScreen);
        gamesettings.resolutionIndex = resolutiondrop.value;
    }

    public void OnmusicVolume()
    {
        musicSource.volume = gamesettings.musicVolume = musicvolume.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSetiings();
        panneloption.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SaveSetiings()
    {
        string jsonData = JsonUtility.ToJson(gamesettings,true);
        File.WriteAllText(Application.persistentDataPath+ "/gamesettings.json",jsonData);
    }

    public void LoadSettings()
    {
        gamesettings = JsonUtility.FromJson<GameSettingd>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        
        musicvolume.value = gamesettings.musicVolume;
        resolutiondrop.value = gamesettings.resolutionIndex;
        fullscreenTogg.isOn = gamesettings.fullscreen;

        Screen.fullScreen = gamesettings.fullscreen;
        resolutiondrop.RefreshShownValue();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

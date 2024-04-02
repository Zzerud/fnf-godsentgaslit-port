using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsV2 : MonoBehaviour
{

    public GameObject controlPanel;
    

    public void OnEnterControlPanel()
    {
        controlPanel.SetActive(true);
    }

    [Header("Game Options")] public GameObject[] allWindows;
    [Space]
    public Button[] allTabs;

    public Color selectedTabColor;
    public Color unselectedTabColor;

    public Color disabled;
    

    [Header("Note Colors")] public ColorPicker[] colorPickers;

    public TMP_InputField[] colorFields;

    [Header("Volumes")] //public TMP_InputField instVolField;
    public TMP_Text instVolText;
    public Slider instVolSlider;
    public static float instVolume;
    public TMP_InputField voicesVolField;
    public Slider voicesVolSlider;
    public static float voicesVolume;
    public TMP_InputField missVolField;
    public Slider missVolSlider;
    public static float missVolume;
    public TMP_Text menuVolText;
    public Slider menuVolSlider;
    public static float menuVolume;
    public TMP_InputField miscVolField;
    public Slider miscVolSlider;
    public static float miscVolume;

    public static OptionsV2 instance;

    public static bool LiteMode { get; set; }
    [Header("Misc")]

    public Toggle liteModeToggle;
    public static bool Downscroll { get; set; }
    public Toggle downscrollToggle;
    public static bool Middlescroll { get; set; }
    public Toggle middleScrollToggle;
    public static bool DebugMode { get; set; }
    public Toggle debugModeToggle;

    public static bool GhostTapping { get; set; }
    public Toggle ghostTappingToggle;

    public static bool AutoPlay { get; set; }
    public Toggle autoPlayToggle;

    public static bool Splashes { get; set; }
    public Toggle splashesToggle;

    public static bool Checkpoints { get; set; }
    public Toggle checkpointsToggle;
    public static bool PostProcessing { get; set; }
    public Toggle postProcessingToggle;
    public static bool CameraShake { get; set; }
    public Toggle cameraShakeToggle;
    public static bool DebugMenu { get; set; }
    public Toggle debugMenuToggle;


    public static bool DesperateMode
    {
        get => _desperateMode;
        set
        {
            if (value)
            {
                LiteMode = true;
                _desperateMode = true;
            }
            else
            {
                _desperateMode = false;
            }
        }
    }
    public static float BtnSize { get; set; }

    private static bool _desperateMode;

    public Toggle desperateModeToggle;
    public static bool SongDuration { get; set; }
    public Toggle songDurationToggle;

    // Start is called before the first frame update
    void Start()
    {
        allTabs[0].interactable = false;
        LoadNotePrefs();
        LoadMisc();
        LoadVolumeProperties();
        HookVolumeCallbacks();
        if (!PlayerPrefs.HasKey("BtnAlpha")) PlayerPrefs.SetFloat("BtnAlpha", 45);
        if (!PlayerPrefs.HasKey("HitAlpha")) PlayerPrefs.SetFloat("HitAlpha", 5);
        if (!PlayerPrefs.HasKey("Death")) PlayerPrefs.SetFloat("Death", 0);
        instance = this;
    }
    #region Options Windows
    public void ChangeOptionsWindow(GameObject window)
    {
        foreach (GameObject optionWindow in allWindows)
            optionWindow.SetActive(false);

        window.SetActive(true);
    }

    public void ChangeOptionsTab(Button tab)
    {
        foreach (Button tabButton in allTabs)
        {
            tabButton.interactable = true;
        }

        tab.interactable = false;

    }
    #endregion
    #region Note Colors

    public void OnLeftArrowFieldUpdated(string colorString)
    {
        colorString = "#" + colorString.Replace("#", "");

        if (colorString.Length != 7) return;
        if (ColorUtility.TryParseHtmlString(colorString, out var color))
        {
            colorPickers[0].color = color;
        }

        SaveNotePrefs();
    }
    public void OnDownArrowFieldUpdated(string colorString)
    {
        colorString = "#" + colorString.Replace("#", "");

        if (colorString.Length != 7) return;
        if (ColorUtility.TryParseHtmlString(colorString, out var color))
        {
            colorPickers[1].color = color;
        }
        SaveNotePrefs();
    }
    public void OnUpArrowFieldUpdated(string colorString)
    {
        colorString = "#" + colorString.Replace("#", "");

        if (colorString.Length != 7) return;
        if (ColorUtility.TryParseHtmlString(colorString, out var color))
        {
            colorPickers[2].color = color;
        }
        SaveNotePrefs();
    }
    public void OnRightArrowFieldUpdated(string colorString)
    {
        colorString = "#" + colorString.Replace("#", "");

        if (colorString.Length != 7) return;
        if (ColorUtility.TryParseHtmlString(colorString, out var color))
        {
            colorPickers[3].color = color;
        }
        SaveNotePrefs();
    }

    public void LoadNotePrefs()
    {
        string savedCustomization = PlayerPrefs.GetString("Note Customization");

        if (!string.IsNullOrWhiteSpace(savedCustomization))
        {
            NoteCustomization noteCustomization = JsonConvert.DeserializeObject<NoteCustomization>(savedCustomization);

            for (int i = 0; i < colorPickers.Length; i++)
            {
                // ReSharper disable once PossibleNullReferenceException
                colorPickers[i].color = noteCustomization.savedColors[i];
                colorFields[i].text = ColorUtility.ToHtmlStringRGB(noteCustomization.savedColors[i]);
            }
        }
        else
        {
            for (int i = 0; i < colorPickers.Length; i++)
            {
                Color selectedColor = NoteCustomization.defaultFnfColors[i];
                
                colorPickers[i].color = selectedColor;

                colorFields[i].text = ColorUtility.ToHtmlStringRGB(selectedColor);
            }

            PlayerPrefs.SetString("Note Customization",
                JsonConvert.SerializeObject(new NoteCustomization {savedColors = NoteCustomization.defaultFnfColors}));
            PlayerPrefs.Save();
        }
    }
    
    public void SaveNotePrefs()
    {
        //ColorOnButtonInTitle.instance.Start();
        List<Color> colors = new List<Color>();
        foreach (ColorPicker picker in colorPickers)
        {
            colors.Add(picker.color);
        }

        NoteCustomization noteCustomization = new NoteCustomization
        {
            savedColors = colors.ToArray()
        };

        PlayerPrefs.SetString("Note Customization", JsonConvert.SerializeObject(noteCustomization));
    }
    #endregion
    #region Volumes

    public void LoadVolumeProperties()
    {
        string volumeData = PlayerPrefs.GetString("Volume", JsonConvert.SerializeObject(new VolumeProperties()));

        VolumeProperties volumeProperties = JsonConvert.DeserializeObject<VolumeProperties>(volumeData);

        instVolSlider.value = volumeProperties.instVol;
        voicesVolSlider.value = volumeProperties.voicesVol;
        menuVolSlider.value = volumeProperties.menuVol;
        missVolSlider.value = volumeProperties.missVol;
        miscVolSlider.value = volumeProperties.miscVol;

        //instVolField.SetTextWithoutNotify((volumeProperties.instVol).ToString(CultureInfo.InvariantCulture));
        instVolText.text = (volumeProperties.instVol).ToString(CultureInfo.InvariantCulture) + "%";
        voicesVolField.SetTextWithoutNotify((volumeProperties.voicesVol).ToString(CultureInfo.InvariantCulture));
        //menuVolField.SetTextWithoutNotify((volumeProperties.menuVol).ToString(CultureInfo.InvariantCulture));
        menuVolText.text = (volumeProperties.menuVol).ToString(CultureInfo.InvariantCulture) + "%";
        missVolField.SetTextWithoutNotify((volumeProperties.missVol).ToString(CultureInfo.InvariantCulture));
        miscVolField.SetTextWithoutNotify((volumeProperties.miscVol).ToString(CultureInfo.InvariantCulture));

        instVolume = volumeProperties.instVol/100;
        voicesVolume = volumeProperties.voicesVol/100;
        menuVolume = volumeProperties.menuVol/100;
        missVolume = volumeProperties.missVol/100;
        miscVolume = volumeProperties.miscVol/100;

        MenuV2.Instance.musicSource.volume = menuVolume;
    }
    public void HookVolumeCallbacks()
    {
        instVolSlider.onValueChanged.AddListener(OnInstSliderChange);
        voicesVolSlider.onValueChanged.AddListener(OnVoicesSliderChange);
        menuVolSlider.onValueChanged.AddListener(OnMenuSliderChange);
        missVolSlider.onValueChanged.AddListener(OnMissSliderChange);
        miscVolSlider.onValueChanged.AddListener(OnMiscSliderChange);

        //instVolField.onEndEdit.AddListener(OnInstFieldChange);
        
        voicesVolField.onEndEdit.AddListener(OnVoicesFieldChange);
       // menuVolField.onEndEdit.AddListener(OnMenuFieldChange);
        missVolField.onEndEdit.AddListener(OnMissFieldChange);
        miscVolField.onEndEdit.AddListener(OnMiscFieldChange);
    }

    public void OnInstSliderChange(float value)
    {
        //instVolField.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));
        instVolText.text = value.ToString(CultureInfo.InvariantCulture) + "%";
        instVolume = value / 100;
        
    }

    public void OnInstFieldChange(string text)
    {
        var volume = float.Parse(text);
        instVolume = volume / 100;

        instVolSlider.value = volume;
    }
    public void OnVoicesSliderChange(float value)
    {
        voicesVolField.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));

        voicesVolume = value / 100;;
    }

    public void OnVoicesFieldChange(string text)
    {
        var volume = float.Parse(text);
        voicesVolume = volume / 100;

        voicesVolSlider.value = volume;
    }
    public void OnMissSliderChange(float value)
    {
        missVolField.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));

        missVolume = value / 100;;
    }

    public void OnMissFieldChange(string text)
    {
        var volume = float.Parse(text);
        missVolume = volume / 100;

        missVolSlider.value = volume;
    }
    public void OnMenuSliderChange(float value)
    {
        //menuVolField.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));
        menuVolText.text = value.ToString(CultureInfo.InvariantCulture) + "%";
        menuVolume = value / 100;;
    }

    public void OnMenuFieldChange(string text)
    {
        var volume = float.Parse(text);
        menuVolume = volume / 100;

        menuVolSlider.value = volume;
    }
    public void OnMiscSliderChange(float value)
    {
        miscVolField.SetTextWithoutNotify(value.ToString(CultureInfo.InvariantCulture));

        miscVolume = value / 100;;
    }

    public void OnMiscFieldChange(string text)
    {
        var volume = float.Parse(text);
        miscVolume = volume / 100;

        miscVolSlider.value = volume;
    }

    public void SaveVolume()
    {
        VolumeProperties volumeProperties = new VolumeProperties
        {
            instVol = instVolume * 100,
            voicesVol = voicesVolume * 100,
            menuVol = menuVolume * 100,
            missVol = missVolume * 100,
            miscVol = miscVolume * 100
        };

        PlayerPrefs.SetString("Volume", JsonConvert.SerializeObject(volumeProperties));
        PlayerPrefs.Save();

        MenuV2.Instance.musicSource.volume = menuVolume;
    }
    
    
    #endregion
    #region Offset

    public void OpenOffset()
    {
        LoadingTransition.instance.Show(() => SceneManager.LoadScene("Calibration"));
    }
    #endregion
    #region Misc

    public void DesperateModeToggle(bool value)
    {
        if (value)
        {
            liteModeToggle.isOn = true;
            LiteMode = true;
        }
    }
    
    public void SaveMisc()
    {
        MiscOptions options = new MiscOptions
        {
            enableDownscroll = Downscroll,
            enableMiddlescroll = Middlescroll,
            enableLiteMode = LiteMode,
            enableDesperateMode = DesperateMode,
            enableSongDuration = SongDuration,
            enableDebugMode = DebugMode,
            enableGhostTapping = GhostTapping,
            enableAutoPlay = AutoPlay,
            enableSplashes = Splashes,
            enableCheckpoints = Checkpoints,
            enablePostProcessing = PostProcessing,
            enableCameraShake = CameraShake,
            enableDebugMenu = DebugMenu
        };

        PlayerPrefs.SetString("MiscOptions", JsonConvert.SerializeObject(options));
        PlayerPrefs.Save();
    }

    public void LoadMisc()
    {
        MiscOptions options = JsonConvert.DeserializeObject<MiscOptions>(PlayerPrefs.GetString("MiscOptions",JsonConvert.SerializeObject(new MiscOptions())));

        Downscroll = options.enableDownscroll;
        Middlescroll = options.enableMiddlescroll;
        LiteMode = options.enableLiteMode;
        DesperateMode = options.enableDesperateMode;
        SongDuration = options.enableSongDuration;
        DebugMode = options.enableDebugMode;
        GhostTapping = options.enableGhostTapping;
        AutoPlay = options.enableAutoPlay;
        Splashes = options.enableSplashes;
        Checkpoints = options.enableCheckpoints;
        PostProcessing = options.enablePostProcessing;
        CameraShake = options.enableCameraShake;
        DebugMenu = options.enableDebugMenu;

        downscrollToggle.SetIsOnWithoutNotify(Downscroll);
        middleScrollToggle.SetIsOnWithoutNotify(Middlescroll);
        liteModeToggle.SetIsOnWithoutNotify(LiteMode);
        desperateModeToggle.SetIsOnWithoutNotify(DesperateMode);
        songDurationToggle.SetIsOnWithoutNotify(SongDuration);
        debugModeToggle.SetIsOnWithoutNotify(DebugMode);
        ghostTappingToggle.SetIsOnWithoutNotify(GhostTapping);
        autoPlayToggle.SetIsOnWithoutNotify(AutoPlay);
        splashesToggle.SetIsOnWithoutNotify(Splashes);
        checkpointsToggle.SetIsOnWithoutNotify(Checkpoints);
        postProcessingToggle.SetIsOnWithoutNotify(PostProcessing);
        cameraShakeToggle.SetIsOnWithoutNotify(CameraShake);
        debugMenuToggle.SetIsOnWithoutNotify(DebugMenu);

        if (!PlayerPrefs.HasKey("BtnSize")) PlayerPrefs.SetFloat("BtnSize", 17);
        

    }

   
    
    #endregion
    #region Dev Tools

    public void LoadEditor(int type)
    {
        switch (type)
        {
            case 1:
                LoadingTransition.instance.Show(() => SceneManager.LoadScene("SceneEditor"));
                break;
            case 2:
                LoadingTransition.instance.Show(() => SceneManager.LoadScene("CharacterCreator"));
                break;
        }
    }
    
    #endregion
    // Update is called once per frame
    void Update()
    {
        
    }

}

[Serializable]
public class VolumeProperties
{
    public float instVol = 75f;
    public float voicesVol = 75f;
    public float menuVol = 75f;
    public float missVol = 30f;
    public float miscVol = 75f;
}

[Serializable]
public class MiscOptions
{
    public bool enableDownscroll = false;
    public bool enableMiddlescroll = false;
    public bool enableSongDuration = false;
    public bool enableLiteMode = false;
    public bool enableDesperateMode = false;
    public bool enableDebugMode = false;
    public bool enableGhostTapping = true;
    public bool enableAutoPlay = false;
    public bool enableSplashes = true;
    public bool enableCheckpoints = true;
    public bool enablePostProcessing = true;
    public bool enableCameraShake = true;
    public bool enableDebugMenu = false;
}

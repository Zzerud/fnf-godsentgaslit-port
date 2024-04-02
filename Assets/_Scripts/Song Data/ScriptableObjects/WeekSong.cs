using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Song",menuName = "Create New Song")]
public class WeekSong : ScriptableObject
{
    public string songName;
    public string sceneName;
    
    [Space]
    public TextAsset chart;
    public TextAsset chartEasy;
    public AudioClip instrumentals;
    public AudioClip vocalsP2;
    public AudioClip vocalsP1;

    [Space] public VideoClip videoRus;
    [Space] public VideoClip videoEng;
    [Space] public string videoPath;
    public bool isGfEnabled = false;
    public bool isNonsensicaAnimations = false;
    public bool isCustomIcons = false;

    public int songIndex;
    public Sprite imageInPause;


    public Color songColor;
    public Color additionalSongColor;
}
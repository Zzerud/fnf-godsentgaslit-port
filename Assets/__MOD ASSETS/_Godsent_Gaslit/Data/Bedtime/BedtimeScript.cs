using BrewedInk.CRT;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;

public class BedtimeScript : MonoBehaviour
{
    public static BedtimeScript instance { get; private set; }
    [HideInInspector]public bool isTvScene = false;

    public GameObject blackAndWhite, scene1, scene2, scene3, scene4, scene5, scene6, fog1, fog2;
    public SpriteRenderer fog4;
    public Animator tvs;

    [Space(15)]
    public PostProcessProfile a;

    [Header("Flashes")]
    public SpriteRenderer blackFlash;
    public SpriteRenderer whiteFlash;

    public VideoClip firstSceneClip, nightmare, tv, huggy, huggyjump, ending;

    public Camera t;
    private void Start()
    {
        instance = this;
        blackAndWhite.SetActive(true);
        CameraMovement.instance.volume.profile = a;

        scene1.SetActive(true);
        scene2.SetActive(false);
        scene3.SetActive(false);
        scene4.SetActive(false);

        whiteFlash.DOFade(0, 0);
        blackFlash.DOFade(1, 0);

        VideoPlayedInstance.instance.clip = firstSceneClip;
        VideoPlayedInstance.instance.player.clip = firstSceneClip;
        VideoPlayedInstance.instance.player.Prepare();
        fog4.DOFade(0, 0);


        if (Player.playAsEnemy)
        {
            foreach (SpriteRenderer sprite in Song.instance.player1NoteSprites)
            {
                sprite.enabled = false;
            }


            foreach (SpriteRenderer sprite in Song.instance.player2NoteSprites)
            {
                sprite.enabled = true;
            }

        }
        else
        {
            foreach (SpriteRenderer sprite in Song.instance.player1NoteSprites)
            {
                sprite.enabled = true;
            }
            foreach (SpriteRenderer sprite in Song.instance.player2NoteSprites)
            {
                sprite.enabled = false;
            }

        }
    }
    public void ChangeWhiteFade(string fadeType, string time)
    {
        float s = float.Parse(time, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
        switch (fadeType)
        {
            case "in":
                whiteFlash.DOFade(1, s);
                break;
            case "out":
                whiteFlash.DOFade(0, s);
                break;
            case "flash":
                whiteFlash.DOFade(1, 0);
                AddCameraZoom("0.3");
                whiteFlash.DOFade(0, s);
                break;
            case "inWithoutBlack":
                blackFlash.DOFade(0, 0);
                whiteFlash.DOFade(1, 0);
                break;
            case "inBlack":
                blackFlash.DOFade(1, s);
                break;
            case "outBlack":
                blackFlash.DOFade(0, s);
                break;
            case "inUI":
                CameraShake.instance.whiteUi.CrossFadeAlpha(1, s, false);
                break;
            case "outUI":
                CameraShake.instance.whiteUi.CrossFadeAlpha(0, s, false);
                break;


        }
    }

    public void StartGame()
    {
        VideoPlayedInstance.instance.raw.SetActive(true);
        VideoPlayedInstance.instance.player.Play();
    }
    public void EndFirstVid()
    {
        VideoPlayedInstance.instance.raw.SetActive(false);
        VideoPlayedInstance.instance.player.Stop();
        CameraShake.instance.Flash("1.001");

        //Later lol
        /*[
						"Toggle Zoom on Beat",
						"0.02, 0.02",
						"On"
					],*/
    }
    
    public void FirstPersonFight()
    {
        scene1.SetActive(false);
        scene2.SetActive(true);
        CameraShake.instance.Flash("1.001");
        MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("catnap2");
        MainEventSystem.instance.ChangeCharacterPlayer("bf2");
    }
    public void BackToNormal()
    {
        scene1.SetActive(true);
        scene2.SetActive(false);
        fog1.SetActive(false);
        fog2.SetActive(true);
        CameraShake.instance.Flash("1.001");
        MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("catnap");
        MainEventSystem.instance.ChangeCharacterPlayer("bf");
    }
    public void Gas()
    {
        MainEventSystem.instance.PlayCutSceneEnemy("gas");
        fog4.DOFade(1, 4);
        VideoPlayedInstance.instance.player.clip = nightmare;
        VideoPlayedInstance.instance.player.Prepare();
    }
    public void PlayVid()
    {
        AddCameraZoom("0.3");
        VideoPlayedInstance.instance.player.Play();
        VideoPlayedInstance.instance.raw.SetActive(true);
        MainEventSystem.instance.thing.SetActive(false);

    }
    public void ClimbCatnap()
    {
        MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("catnapclimb");
        MainEventSystem.instance.ChangeCharacterPlayer("bfCatnapClimb");
        scene1.SetActive(false);
        scene3.SetActive(true);
        //fog4.DOFade(0, 0);

        ChangeWhiteFade("outBlack", "3.5");
        VideoPlayedInstance.instance.raw.SetActive(false);
        VideoPlayedInstance.instance.player.Stop();
        VideoPlayedInstance.instance.player.clip = tv;
        VideoPlayedInstance.instance.player.Prepare();
    }

    public void TvMoment()
    {
        fog4.DOFade(0, 0);
        ChangeWhiteFade("outBlack", "6.01");
        VideoPlayedInstance.instance.raw.SetActive(false);
        VideoPlayedInstance.instance.player.Stop();
        isTvScene = true;
        AddCameraZoom("0.2");

        scene3.SetActive(false);
        scene4.SetActive(true);
        MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("tv");
        MainEventSystem.instance.thing.SetActive(true);
    }
    public void PrepareForHuggy()
    {
        ChangeWhiteFade("inBlack", "0.5");
        VideoPlayedInstance.instance.player.clip = huggy;
        VideoPlayedInstance.instance.player.Prepare();
    }
    public void Huggy()
    {
        ChangeWhiteFade("outBlack", "1.001");
        CameraShake.instance.Flash("1.001");
        scene4.SetActive(false);
        scene5.SetActive(true);
        MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("huggy");
        MainEventSystem.instance.ChangeCharacterPlayer("bfHuggy");
        MainEventSystem.instance.thing.SetActive(false);
        VideoPlayedInstance.instance.raw.SetActive(false);
        VideoPlayedInstance.instance.player.Stop();

        VideoPlayedInstance.instance.player.clip = huggyjump;
        VideoPlayedInstance.instance.player.Prepare();
    }
    public void Final()
    {
        VideoPlayedInstance.instance.raw.SetActive(false);
        VideoPlayedInstance.instance.player.Stop();
        scene5.SetActive(false);
        scene6.SetActive(true);
        MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("upnap");
        MainEventSystem.instance.ChangeCharacterPlayer("bf3");
        ChangeWhiteFade("outBlack", "0.001");
        CameraShake.instance.Flash("1.001");
    }
    public void PrepareForEnd()
    {
        ChangeWhiteFade("inBlack", "1.2");
        VideoPlayedInstance.instance.player.clip = ending;
        VideoPlayedInstance.instance.player.Prepare();
    }

    #region NeedToAllOfScripts
    public void AddCameraZoom(string to)
    {
        float s = float.Parse(to, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
        Song.instance.mainCamera.orthographicSize -= s;
    }
    [System.Serializable]
    public class Subtitle
    {
        public string textEng;
        public string textRus;
        public string color;
    }
    [Space(20)]
    [Header("Subtitles")]
    public Subtitle[] sub;
    private int indexText = 0;
    public void ToggleSubtitles()
    {
        if (Application.systemLanguage == SystemLanguage.Russian) MainEventSystem.instance.ToggleSubtitles(sub[indexText].textRus, sub[indexText].color);
        else MainEventSystem.instance.ToggleSubtitles(sub[indexText].textEng, sub[indexText].color);
        indexText++;
    }
    public void LoadBGSave(int numberSave)
    {
        switch (numberSave)
        {
            case 1:
                MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("Scarlet");
                break;
            case 2:
                MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("Sweet");
                break;
            case 3:
                MainEventSystem.instance.ChangeCharacterEnemyWithoutFlash("LoFi");
                break;
        }
        Song.instance.isLoadedEvents = true;
    }
    #endregion

}

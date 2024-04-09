using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ButtonControllManager : MonoBehaviour
{

    public RectTransform[] btnsTransforms;
    public GameObject[] open_close;
    public RectTransform transMenu;
    public GameObject[] menuBtns, menuHits;

    [Header("Buttons")]
    [Space]
    public TMP_Text sizeBtnText;
    public Slider sizeBtnSlider;
    public TMP_Text alphaBtnText;
    public Slider alphaBtnSlider;
    public Image btnPreview;

    [Header("Hits")]
    [Space]
    public Image[] hits;
    public TMP_Text alphaHitText;
    public Slider alphaHitSlider;

    private bool isMenuOpen = false;
    private float toRotateBtn;
    private Vector2 toTrans, defTrans;


    private void Start()
    {
        toTrans = transMenu.anchoredPosition;
        defTrans = transMenu.anchoredPosition;

        if (PlayerPrefs.GetInt("Toggle") == 0)
        {
            foreach (GameObject j in menuHits) j.SetActive(true);
            foreach (GameObject t in menuBtns) t.SetActive(false);
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].CrossFadeAlpha(1, 0, false);
                hits[i].CrossFadeAlpha((OptionsV2.instance.cntrSettings.hitboxesAlpha / 100), 0, false);
            }
            alphaHitText.text = OptionsV2.instance.cntrSettings.hitboxesAlpha.ToString("") + "%";
            alphaHitSlider.value = OptionsV2.instance.cntrSettings.hitboxesAlpha;
        }
        else
        {
            foreach (GameObject j in menuHits) j.SetActive(false);
            foreach (GameObject t in menuBtns) t.SetActive(true);
            btnPreview.CrossFadeAlpha(1, 0, false);
            btnPreview.CrossFadeAlpha((OptionsV2.instance.cntrSettings.buttonsAlpha / 100), 0, false);
            alphaBtnText.text = OptionsV2.instance.cntrSettings.buttonsAlpha.ToString("") + "%";
            alphaBtnSlider.value = OptionsV2.instance.cntrSettings.buttonsAlpha;

            sizeBtnSlider.value = OptionsV2.instance.cntrSettings.buttonsSize;
            sizeBtnText.text = ((OptionsV2.instance.cntrSettings.buttonsSize / sizeBtnSlider.maxValue) * 100f).ToString("") + "%";
            for (int i = 0; i < btnsTransforms.Length; i++)
            {
                btnsTransforms[i].sizeDelta = new Vector2((OptionsV2.instance.cntrSettings.buttonsSize * 10), (OptionsV2.instance.cntrSettings.buttonsSize * 10));
            }
        }        
    }
    private void OnEnable()
    {
        Start();
    }
    private void Update()
    {
        transMenu.anchoredPosition = Vector2.MoveTowards(transMenu.anchoredPosition, toTrans, 7f);
    }

    public void OnExit()
    {
        for (int i = 0; i < 4; i++)
        {
            OptionsV2.instance.cntrSettings.btnsPos[i].posX = btnsTransforms[i].anchoredPosition.x;
            OptionsV2.instance.cntrSettings.btnsPos[i].posY = btnsTransforms[i].anchoredPosition.y;
        }
        OptionsV2.instance.SaveJsonControl();
        gameObject.SetActive(false);
    }

    public void ButtonInteractable()
    {
        isMenuOpen = !isMenuOpen;
        if(isMenuOpen)
        {
            toTrans = new Vector2(43, transMenu.anchoredPosition.y);
            toRotateBtn = 180;
        }
        else
        {
            toTrans = new Vector2(defTrans.x, transMenu.anchoredPosition.y);
            toRotateBtn = 0;
        }
        for (int i = 0; i < open_close.Length; i++)
        {
            open_close[i].LeanRotate(new Vector3(0, 0, toRotateBtn), 0.5f);
        }
    }
    public void ChangeSizeButton(float value)
    {
        OptionsV2.instance.cntrSettings.buttonsSize = value;

        sizeBtnText.text = ((value / sizeBtnSlider.maxValue) * 100f).ToString("") + "%";
        for (int i = 0; i < btnsTransforms.Length; i++)
        {
            btnsTransforms[i].sizeDelta = new Vector2((value * 10), (value * 10));
        }
        OptionsV2.instance.SaveJsonControl();
    }
    public void ChangeAlphaButton(float value)
    {
        OptionsV2.instance.cntrSettings.buttonsAlpha = value;

        btnPreview.CrossFadeAlpha((value / 100f), 0, false);
        alphaBtnText.text = value.ToString("") + "%";
        OptionsV2.instance.SaveJsonControl();
    }
    public void ChangeAlphaHit(float value)
    {
        OptionsV2.instance.cntrSettings.hitboxesAlpha = value;

        foreach (Image hit in hits) hit.CrossFadeAlpha((value / 100), 0, false);

        alphaHitText.text = value.ToString("") + "%";
        OptionsV2.instance.SaveJsonControl();
    }
}

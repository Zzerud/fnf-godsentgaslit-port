using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToTranslate : MonoBehaviour
{
    /*public Vector2 sizeBig, sizeSmall;
    public Vector2 placementBig, placementSmall;*/
    public GameObject big, small;

    /*private Image img;
    public Sprite rusVersion, engVersion;
    [Header("RUS")]
    public SpriteState stateRus;
    [Header("ENG")]
    public SpriteState stateEng;

    private Button btn;*/
    private void Start()
    {
        /*img = GetComponent<Image>();
        img.sprite = Application.systemLanguage == SystemLanguage.Russian ? rusVersion : engVersion;
        btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.spriteState = Application.systemLanguage == SystemLanguage.Russian ? stateRus : stateEng;
        }*/
    }
    
    public void OnEnter()
    {
        big.SetActive(true);
        small.SetActive(false);
    }
    public void OnExit()
    {
        big.SetActive(false);
        small.SetActive(true);
    }
}

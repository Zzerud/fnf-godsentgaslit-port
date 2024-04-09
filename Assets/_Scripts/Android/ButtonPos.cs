using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPos : MonoBehaviour
{
    private RectTransform rect;
    public int id;
    public Vector2 defPos;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(Song.instance.settings.btnsPos[id].posX, Song.instance.settings.btnsPos[id].posY);

        rect.sizeDelta = new Vector2(Song.instance.settings.buttonsSize * 10, Song.instance.settings.buttonsSize * 10);
    }
}

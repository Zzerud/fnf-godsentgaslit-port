using Lean.Transition.Method;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1000)]
public class ControllManager : MonoBehaviour
{
    public ButtonArrow leftArrow;
    public ButtonArrow rightArrow;
    public ButtonArrow upArrow;
    public ButtonArrow downArrow;

    public LeanGraphicColor[] colorsEnter, colorsExit; // 0 - left, 1 - right, 2 - up, 3 - down
    public bool isBTN;
    public bool isCalibration = false;
    public static ControllManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        if (!isCalibration)
        {

            if (isBTN)
            {
                float alpha = Song.instance.settings.buttonsAlpha;
                leftArrow.GetComponent<Image>().color = new Color(ColorPicker.left.r, ColorPicker.left.g, ColorPicker.left.b, alpha / 100f);
                rightArrow.GetComponent<Image>().color = new Color(ColorPicker.right.r, ColorPicker.right.g, ColorPicker.right.b, alpha / 100f);
                upArrow.GetComponent<Image>().color = new Color(ColorPicker.up.r, ColorPicker.up.g, ColorPicker.up.b, alpha / 100f);
                downArrow.GetComponent<Image>().color = new Color(ColorPicker.down.r, ColorPicker.down.g, ColorPicker.down.b, alpha / 100f);

                colorsEnter[0].Data.Value = new Color(ColorPicker.left.r, ColorPicker.left.g, ColorPicker.left.b, ((alpha / 100) - 0.1f));
                colorsExit[0].Data.Value = new Color(ColorPicker.left.r, ColorPicker.left.g, ColorPicker.left.b, (alpha / 100));
                colorsEnter[1].Data.Value = new Color(ColorPicker.right.r, ColorPicker.right.g, ColorPicker.right.b, ((alpha / 100) - 0.1f));
                colorsExit[1].Data.Value = new Color(ColorPicker.right.r, ColorPicker.right.g, ColorPicker.right.b, (alpha / 100));
                colorsEnter[2].Data.Value = new Color(ColorPicker.up.r, ColorPicker.up.g, ColorPicker.up.b, ((alpha / 100) - 0.1f));
                colorsExit[2].Data.Value = new Color(ColorPicker.up.r, ColorPicker.up.g, ColorPicker.up.b, (alpha / 100));
                colorsEnter[3].Data.Value = new Color(ColorPicker.down.r, ColorPicker.down.g, ColorPicker.down.b, ((alpha / 100) - 0.1f));
                colorsExit[3].Data.Value = new Color(ColorPicker.down.r, ColorPicker.down.g, ColorPicker.down.b, (alpha / 100));
            }
            else
            {
                float alpha = Song.instance.settings.hitboxesAlpha;
                leftArrow.GetComponent<Image>().color = new Color(ColorPicker.left.r, ColorPicker.left.g, ColorPicker.left.b, (alpha / 100));
                rightArrow.GetComponent<Image>().color = new Color(ColorPicker.right.r, ColorPicker.right.g, ColorPicker.right.b, (alpha / 100));
                upArrow.GetComponent<Image>().color = new Color(ColorPicker.up.r, ColorPicker.up.g, ColorPicker.up.b, (alpha / 100));
                downArrow.GetComponent<Image>().color = new Color(ColorPicker.down.r, ColorPicker.down.g, ColorPicker.down.b, (alpha / 100));

                colorsEnter[0].Data.Value = new Color(ColorPicker.left.r, ColorPicker.left.g, ColorPicker.left.b, ((alpha / 100) - 0.1f));
                colorsExit[0].Data.Value = new Color(ColorPicker.left.r, ColorPicker.left.g, ColorPicker.left.b, (alpha / 100));
                colorsEnter[1].Data.Value = new Color(ColorPicker.right.r, ColorPicker.right.g, ColorPicker.right.b, ((alpha / 100) - 0.1f));
                colorsExit[1].Data.Value = new Color(ColorPicker.right.r, ColorPicker.right.g, ColorPicker.right.b, (alpha / 100));
                colorsEnter[2].Data.Value = new Color(ColorPicker.up.r, ColorPicker.up.g, ColorPicker.up.b, ((alpha / 100) - 0.1f));
                colorsExit[2].Data.Value = new Color(ColorPicker.up.r, ColorPicker.up.g, ColorPicker.up.b, (alpha / 100));
                colorsEnter[3].Data.Value = new Color(ColorPicker.down.r, ColorPicker.down.g, ColorPicker.down.b, ((alpha / 100) - 0.1f));
                colorsExit[3].Data.Value = new Color(ColorPicker.down.r, ColorPicker.down.g, ColorPicker.down.b, (alpha / 100));
            }
        }
        
    }
    public static bool GetKey(Key key)
    {
        switch (key)
        {
            case Key.left:
                return Instance.leftArrow.isPressed;
            case Key.right:
                return Instance.rightArrow.isPressed;
            case Key.up:
                return Instance.upArrow.isPressed;
            case Key.down:
                return Instance.downArrow.isPressed;
            default:
                break;
        }
        return false;
    }
    public static bool GetKeyDown(Key key)
    {
        switch (key)
        {
            case Key.left:
                //Debug.Log("left");
                return Instance.leftArrow.thisFrame;
            case Key.right:
                return Instance.rightArrow.thisFrame;
            case Key.up:
                return Instance.upArrow.thisFrame;
            case Key.down:
                return Instance.downArrow.thisFrame;
            default:
                break;
        }
        return false;
    }
    public static bool GetKeyUp(Key key)
    {
        switch (key)
        {
            case Key.left:
                //Debug.Log("left");
                return Instance.leftArrow.isUp;
            case Key.right:
                return Instance.rightArrow.isUp;
            case Key.up:
                return Instance.upArrow.isUp;
            case Key.down:
                return Instance.downArrow.isUp;
            default:
                break;
        }
        return false;
    }

    public enum Key
    {
        left,
        right,
        up,
        down
    }
}

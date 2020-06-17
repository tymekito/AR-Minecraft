using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorController : MonoBehaviour
{
    private Image image;
    private Color baseCol;
    public Color colorToSwitch;
    private void Start()
    {

        image = GetComponent<Image>();
        baseCol = image.color;
    }
    public void ButtonIsSelected()
    {
        ButtonColorController[] btns = FindObjectsOfType<ButtonColorController>();
        foreach (ButtonColorController btn in btns)
            btn.image.color = baseCol;

        image.color = colorToSwitch;
    }
}

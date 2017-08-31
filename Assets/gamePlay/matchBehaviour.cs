using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class matchBehaviour : MonoBehaviour
{
    public event EventHandler matchSelected;
    public event EventHandler matchDeselected;

    private static Color whiteColor = new Color(1, 1, 1);
    private static Color redColor = new Color(1, 0, 0);

    private bool isChoosed;
    private Image matchImage;
    private controlBehaviour controlScript;
    private GameObject thisObject;

    void Awake()
    {
        isChoosed = false;
        matchImage = GetComponent<Image>();
        controlScript = Camera.main.GetComponent<controlBehaviour>();
        thisObject = gameObject;
    }

    void OnMouseDown()
    {
        if (isChoosed)
        {
            isChoosed = false;
            matchImage.color = whiteColor;
            matchDeselected(this, null);
        }
        else
        {
            if (controlScript.CanPlayerSelectMatch())
            {
                isChoosed = true;
                matchImage.color = redColor;
                matchSelected(this, null);
            }

        }
    }

    public void ComputerSelectedMatch()
    {
        isChoosed = true;
        matchImage.color = redColor;
        matchSelected(this, null);
    }

    public void SetActive(bool isActive)
    {
        thisObject.SetActive(isActive);
    }
}

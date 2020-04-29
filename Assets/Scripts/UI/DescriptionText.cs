using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class DescriptionText : MonoBehaviour
{
    public static DescriptionText Instance;

    TextMeshProUGUI field;

    private void Awake()
    {
        MakeSingelton();
    }
    void MakeSingelton()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        field = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ActivateText(bool setActiv, string text)
    {
        field.text = setActiv ? text : "";
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class DescriptionText : MonoBehaviour
{
    public FadeOutInfo fadeOutInfo;

    public EnemieCountUI enemieCountUI;

    TextMeshProUGUI field;

    private void Awake()
    {
        DungeonMaster.Instance.descriptionText = this;
        field = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ActivateText(bool setActiv, string text)
    {
        field.text = setActiv ? text : "";
    }
}

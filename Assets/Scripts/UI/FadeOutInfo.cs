using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FadeOutInfo : MonoBehaviour
{
    [SerializeField]
    float timeTofade = 0.5f;

    [SerializeField]
    TextMeshProUGUI textField;


    Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        DungeonMaster.Instance.descriptionText.fadeOutInfo = this;
        normalColor = textField.color;
    }


    public void FadeOutText(string text)
    {
        textField.text = text;
        textField.color = normalColor;
        textField.DOFade(0, timeTofade);
    }
}

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

    TextMeshProUGUI textField;


    Color normalColor;

    // Start is called before the first frame update
    void Start()
    {
        DescriptionText.Instance.fadeOutInfo = this;
        textField = GetComponent<TextMeshProUGUI>();
        normalColor = textField.color;
    }


    public void FadeOutText(string text)
    {
        textField.text = text;
        textField.color = normalColor;
        textField.DOFade(0, timeTofade);
    }
}

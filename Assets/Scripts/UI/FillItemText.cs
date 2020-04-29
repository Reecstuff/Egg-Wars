using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas), typeof(ThisFaceCamera))]
public class FillItemText : MonoBehaviour
{
    public ItemText itemText;

    TextMeshProUGUI titelText;
    ThisFaceCamera faceCamera;

    private void Start()
    {
        SetItemText();
        if (faceCamera == null)
            faceCamera = GetComponent<ThisFaceCamera>();
    }

    public void ShowText(bool doShow, float time = 0.3f)
    {
        float startValue = 0;
        float wantToBeValue = 0;

        if(!doShow)
        {
            startValue = transform.localPosition.y;
            wantToBeValue = 0;
        }
        else
        {
            startValue = 0;
            wantToBeValue = transform.localPosition.y;
        }

        transform.localPosition = new Vector3(transform.localPosition.x, startValue, transform.localPosition.z);
        transform.DOLocalMoveY(wantToBeValue, 0.3f);
    }
    


    void SearchForTextFields()
    {
        if(titelText == null)
        {
            titelText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public void SetItemText()
    {
        titelText.text = itemText.Titel;
    }

    private void OnValidate()
    {
        if(titelText == null)
        {
            SearchForTextFields();
        }
    }
}

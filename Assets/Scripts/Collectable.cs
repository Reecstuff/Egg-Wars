using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    
    public ItemText itemText;


    [SerializeField]
    float AnimationTime = 0.3f;

    FillItemText FillitemText;
    SphereCollider sphereCollider;
    RotateAround rotateAround;

    public bool isActivated = false;

    float standardY;

    void Start()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        sphereCollider.isTrigger = true;

        rotateAround = GetComponentInChildren<RotateAround>();

        if (itemText == null)
        {
            FillitemText.itemText = itemText;
            FillitemText = GetComponentInChildren<FillItemText>();
        }
        FillitemText.gameObject.SetActive(false);
        standardY = transform.position.y;
    }

    public void ActivateCollectable(bool setActiv)
    {
        isActivated = setActiv;
        rotateAround.Rotate = setActiv;
        float wantToBeYValue = 0;

        if(setActiv)
        {
            wantToBeYValue = transform.position.y + 1;
        }
        else
        {
            wantToBeYValue = standardY;
        }

        transform.DOMoveY(wantToBeYValue, AnimationTime);
        
        FillitemText.gameObject.SetActive(setActiv);
        DescriptionText.Instance.ActivateText(setActiv, itemText.Description);
        if (setActiv)
        {
            FillitemText.ShowText(setActiv);
        }
    }

    private void OnValidate()
    {
        if(itemText != null)
        {
            FillitemText = GetComponentInChildren<FillItemText>();
            FillitemText.itemText = itemText;
        }
    }
}

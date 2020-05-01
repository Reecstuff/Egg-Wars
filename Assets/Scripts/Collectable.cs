using DG.Tweening;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    
    public ItemText itemText;


    [SerializeField]
    float AnimationTime = 0.3f;

    FillItemText FillitemText;
    SphereCollider sphereCollider;
    [SerializeField]
    RotateAround rotateAround;

    public bool isActivated = false;

    float standardY;

    void Awake()
    {
        SetUpObject();
    }

    void SetUpObject()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        sphereCollider.isTrigger = true;

        if(rotateAround == null)
            rotateAround = GetComponentInChildren<RotateAround>();

        FillitemText = GetComponentInChildren<FillItemText>();
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

        // Set Text by Item
        // Item can change
        DescriptionText.Instance.ActivateText(setActiv, itemText.Description);
        FillitemText.SetItemText(itemText);

        if (setActiv)
        {
            FillitemText.ShowText(setActiv);
        }
    }

    public void UpdateData(GameObject newWeapon)
    {
        if (!newWeapon)
            return;

        itemText = newWeapon.GetComponent<EquipAbleItem>().item;
        DescriptionText.Instance.ActivateText(true, itemText.Description);
        FillitemText.SetItemText(itemText);
        FillitemText.ShowText(true);

        GameObject Modell = rotateAround.gameObject;
        GameObject newModell = newWeapon;

        Modell.GetComponent<MeshFilter>().sharedMesh = newModell.GetComponent<MeshFilter>().sharedMesh;
        Modell.GetComponent<Renderer>().sharedMaterial = newModell.GetComponent<Renderer>().sharedMaterial;
        
    }
}

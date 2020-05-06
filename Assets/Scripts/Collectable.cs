using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
    AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
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
        DungeonMaster.Instance.descriptionText.ActivateText(setActiv, itemText.Description);
        FillitemText.SetItemText(itemText);

        if (setActiv)
        {
            FillitemText.ShowText(setActiv);
        }
    }

    public void PlayAudio()
    {
        if (audioSource.clip)
            audioSource.Play();
    }

    public bool UpdateData(EquipAbleItem newWeapon)
    {
        PlayAudio();
        if (!newWeapon)
        {
            DungeonMaster.Instance.descriptionText.ActivateText(false, "");
            Destroy(gameObject);
            return false;
        }


        itemText = newWeapon.item;
        DungeonMaster.Instance.descriptionText.ActivateText(true, itemText.Description);
        FillitemText.SetItemText(itemText);
        FillitemText.ShowText(true);

        GameObject Modell = rotateAround.gameObject;
        GameObject newModell = newWeapon.gameObject;

        Modell.GetComponent<MeshFilter>().sharedMesh = newModell.GetComponentInChildren<MeshFilter>().sharedMesh;
        Modell.GetComponent<Renderer>().sharedMaterial = newModell.GetComponentInChildren<Renderer>().sharedMaterial;
        return true;
        
    }
}

using DG.Tweening;
using UnityEngine;

public class Lootbox : MonoBehaviour
{
    [SerializeField]
    GameObject collectAblePrefab;
    
    DungeonGenerator dungeon;
    private void Start()
    {
        dungeon = GetComponentInParent<DungeonGenerator>();
    }


    public void OpenBox()
    {
        Collectable collectable = Instantiate(collectAblePrefab, dungeon.transform).GetComponent<Collectable>();

        collectable.transform.localPosition = transform.localPosition;
        // Generate Item
        // Set Collectable
        collectable.UpdateData(DungeonMaster.Instance.AllEquipableItems[Random.Range(0, DungeonMaster.Instance.AllEquipableItems.Count)]);

        Disappear(collectable.itemText);
    }

    void Disappear(ItemText text)
    {
        if(text != null)
            GetComponentInChildren<FillItemText>().SetItemText(text);
        transform.DOScale(0.1f, 0.5f);
        transform.DOLocalRotate(new Vector3(0, 280, 0), 0.5f);
        Invoke(nameof(Exterminate), 2.5f);
    }

    void Exterminate()
    {
        Destroy(this.gameObject);
    }


}

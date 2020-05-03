using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LootSense : MonoBehaviour
{
    [SerializeField]
    ItemText Lootbox;

    [SerializeField]
    ItemText Heal;


    [SerializeField]
    List<Collectable> inSenseLoot;

    Collectable closestLoot;
    float closestDistance = 10;

    Collectable bufferLoot;

    void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        inSenseLoot = new List<Collectable>();
    }

    private void Update()
    {
        if(closestLoot && inSenseLoot.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(closestLoot.itemText.Equals(Lootbox))
                {
                    bufferLoot = closestLoot;
                    inSenseLoot.Remove(closestLoot);
                    closestLoot = null;
                    
                    bufferLoot.PlayAudio();
                    bufferLoot.GetComponentInParent<Lootbox>().OpenBox();
                    bufferLoot.ActivateCollectable(false);
                }
                else if(closestLoot.itemText.Equals(Heal))
                {
                    bufferLoot = closestLoot;
                    inSenseLoot.Remove(closestLoot);
                    closestLoot = null;
                    
                    bufferLoot.PlayAudio();
                    bufferLoot.GetComponent<Heal>().HealPlayer();
                    bufferLoot.ActivateCollectable(false);
                }
                else
                {
                    closestLoot.UpdateData(DungeonMaster.Instance.player.EquipWeapon(closestLoot.itemText));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Collectable>())
        {
            inSenseLoot.Add(other.gameObject.GetComponent<Collectable>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Collectable>())
        {
            try
            {
                if (inSenseLoot.Count > 0)
                    closestLoot = inSenseLoot.Find(l => Vector3.Distance(l.transform.position, transform.position) < closestDistance);
            }
            catch (System.Exception e)
            {
            }

            if(closestLoot && !closestLoot.isActivated)
                closestLoot.ActivateCollectable(true);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Collectable>())
        {
            Collectable current = other.gameObject.GetComponent<Collectable>();
            current.ActivateCollectable(false);
            inSenseLoot.Remove(current);
        }
    }
}

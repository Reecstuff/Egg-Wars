using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LootSense : MonoBehaviour
{
    [SerializeField]
    List<Collectable> inSenseLoot;

    Collectable closestLoot;
    float closestDistance = 10;

    void Start()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
        inSenseLoot = new List<Collectable>();
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
            closestLoot = inSenseLoot.Find(l => Vector3.Distance(l.transform.position, transform.position) < closestDistance);

            if(!closestLoot.isActivated)
                closestLoot.ActivateCollectable(true);

            if (Input.GetKey(KeyCode.F))
            {
                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

                player.EquipWeapon(other.gameObject);
            }
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

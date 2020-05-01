using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public Camera cam;

    Vector3 moveInput;
    Vector3 moveVelocity;

    // For Slowing or Speeding Effect
    public float moveSpeed = 20f;
    public float standardSpeed;

    public GameObject equippedWeapon;
    public Transform weaponSlot;

    public GameObject granadePrefab;

    private float timeBtwShots;
    public float startTimeBtwShots = 1f;

    Animator animator;

    [SerializeField]
    string walkingState = "Walking";
    [SerializeField]
    string standingState = "Standing";

    [SerializeField]
    string walkingValue = "WalkingMultiplier";

    AudioSource walkingSource;
    bool shouldAnimateMoving = false;

    [SerializeField]
    AudioClip[] walkingClips;
    int currentWalkingClip = 0;

    private void Start()
    {
        walkingSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        DungeonMaster.Instance.player = this;
        animator.SetFloat(walkingValue, moveSpeed);
        standardSpeed = moveSpeed;
    }

    private void Update()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        shouldAnimateMoving = moveInput != Vector3.zero;

        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            if(Vector3.Distance(pointToLook, transform.position) > 0.2)
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetKey(KeyCode.Q) && timeBtwShots <= 0)
        {
            timeBtwShots = startTimeBtwShots;

            GameObject grenade = Instantiate(granadePrefab, transform.position, transform.rotation);
            grenade.transform.position = transform.position + grenade.transform.forward;
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = grenade.transform.forward * 10;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(walkingState) && shouldAnimateMoving)
        {
            animator.Play(walkingState);
            walkingSource.Play();
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName(standingState) && !shouldAnimateMoving)
        {
            animator.CrossFade(standingState, 0.0f);
            walkingSource.Stop();
            currentWalkingClip = currentWalkingClip == walkingClips.Length - 1 ? 0 : currentWalkingClip + 1;
            walkingSource.clip = walkingClips[currentWalkingClip];
        }
    }

    /// <summary>
    /// Tauscht die Waffe auf dem Boden mit der jetzigen aus
    /// </summary>
    /// <param name="weapon">Waffe auf dem Boden</param>
    /// <returns>Ausgerüstete Waffe</returns>
    public EquipAbleItem EquipWeapon(ItemText weapon)
    {
        // Finde die waffe aus den vorhanden Waffen
        EquipAbleItem newEquip = DungeonMaster.Instance.AllEquipableItems.FirstOrDefault(w => w.item.Equals(weapon));

        if(newEquip == null)
        {
            // Keine Waffe gefunden also return
            return null;
        }
        else
        {
            GameObject oldWeapon = equippedWeapon;

                
            Destroy(equippedWeapon);

            // Is this a Weapon
            if(oldWeapon.GetComponent<Weapon>())
            {

                // Instantiate the new Weapon
                equippedWeapon = Instantiate(newEquip.gameObject, weaponSlot.transform);
            }
            else
            {
                // Write Code to Equip on Itemslot instead of Weaponslot
                equippedWeapon = Instantiate(newEquip.gameObject, weaponSlot.transform);
            }

            // Return old Weapon back to Collectable
            return oldWeapon.GetComponent<EquipAbleItem>();
        }
    }

    /// <summary>
    /// Wenn eine Waffe oder eine Fähigkeit die Geschwindigkeit des Spielers ändert
    /// </summary>
    /// <param name="speed"></param>
    public void OnSpeedChange(float speed)
    {
        moveSpeed = speed;
        animator.SetFloat(walkingValue, moveSpeed);
    }

    public void PitchWalking(float pitch)
    {
        walkingSource.DOPitch(pitch, 0.1f);
    }
}

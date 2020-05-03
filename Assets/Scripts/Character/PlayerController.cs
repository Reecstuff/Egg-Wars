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

    public CharacterStats characterStats;

    private float timeBtwShots;
    public float startTimeBtwShots = 1f;

    public int ammoGrenade = 0;

    public bool GravityOn = false;

    Animator animator;

    [SerializeField]
    string walkingState = "Walking";
    [SerializeField]
    string standingState = "Standing";

    [SerializeField]
    string meeleState = "Meele";

    [SerializeField]
    string walkingValue = "WalkingMultiplier";


    AudioSource walkingSource;
    AudioSource speakingSource;
    bool shouldAnimateMoving = false;

    int playerGroundProtectionCount = 0;
    float playerGroundProtectionY = 0.0f;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        walkingSource = GetComponent<AudioSource>();
        speakingSource = GetComponents<AudioSource>()[1];
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
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            if (Vector3.Distance(new Vector3(pointToLook.x, transform.position.y, pointToLook.z),transform.position) > 2)
            {
                rb.angularVelocity = Vector3.zero;
                GravityOn = false;
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
            else
            {
                rb.angularVelocity = Vector3.one;
                GravityOn = true;
            }

        }

        if (Input.GetKey(KeyCode.Q) && timeBtwShots <= 0 && ammoGrenade > 0)
        {
            timeBtwShots = startTimeBtwShots;

            GameObject grenade = Instantiate(granadePrefab, transform.position, transform.rotation);
            grenade.transform.position = transform.position + grenade.transform.forward;
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = grenade.transform.forward * 10;

            ammoGrenade--;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;
        AnimationInFixedUpdate();
        OnPlayerGroundProtection();
    }

    public void SwingWeapon()
    {
        //animator.Play(meeleState);
        animator.CrossFade(meeleState, 0.01f, 0);
        // Do Sound here
    }

    void AnimationInFixedUpdate()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(meeleState))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(walkingState) && shouldAnimateMoving)
            {
                CancelInvoke(nameof(PlaySpeakSound));
                animator.Play(walkingState);
                walkingSource.Play();
            }
            else if (!shouldAnimateMoving && !animator.GetCurrentAnimatorStateInfo(0).IsName(standingState))
            {
                animator.CrossFade(standingState, 0.0f);
                walkingSource.Stop();
                Invoke(nameof(PlaySpeakSound), 2);
            }
        }
    }

    void PlaySpeakSound()
    {
        speakingSource.Play();
    }

    void OnPlayerGroundProtection()
    {
        if (playerGroundProtectionY == 0.0f || playerGroundProtectionCount >= 10)
        {
            playerGroundProtectionCount = 0;
            playerGroundProtectionY = transform.position.y;

        }

        if (!DungeonMaster.Instance.PlayerMoving && (Mathf.Abs(transform.position.y - playerGroundProtectionY) > 0.02))
        {
            transform.position = new Vector3(transform.position.x, playerGroundProtectionY, transform.position.z);
            playerGroundProtectionCount = 0;
        }
        else
        {
            playerGroundProtectionCount++;
        }
    }

    /// <summary>
    /// Tauscht die Waffe auf dem Boden mit der jetzigen aus
    /// </summary>
    /// <param name="item">Waffe auf dem Boden</param>
    /// <returns>Ausgerüstete Waffe</returns>
    public EquipAbleItem EquipWeapon(ItemText item)
    {
        // Finde die waffe aus den vorhanden Waffen
        EquipAbleItem newEquip = DungeonMaster.Instance.AllEquipableItems.FirstOrDefault(w => w.item.Equals(item));

        if(newEquip == null)
        {
            // Keine Waffe gefunden also return
            return null;
        }
        else
        {
            GameObject oldWeapon = equippedWeapon;


            // Is this a Weapon
            if(newEquip.GetComponent<Weapon>())
            {
                Destroy(equippedWeapon);
                // Instantiate the new Weapon
                equippedWeapon = Instantiate(newEquip.gameObject, weaponSlot.transform);
            }
            else
            {
                Destroy(equippedWeapon);
                // Write Code to Equip on Itemslot instead of Weaponslot
                equippedWeapon = Instantiate(newEquip.gameObject, weaponSlot.transform);
            }

            if(oldWeapon)
            {
                // Return old Weapon back to Collectable
                return oldWeapon.GetComponent<EquipAbleItem>();
            }
            else
            {
                return null;
            }

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

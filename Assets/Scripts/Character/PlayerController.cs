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

    [SerializeField]
    Transform abilitySlot;

    public GameObject equippedAbility = null;

    public GameObject equippedArmor;
    public Transform armorSlot;

    public GameObject granadePrefab;

    public CharacterStats characterStats;

    private float timeBtwShots;
    public float startTimeBtwShots = 1f;

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

    [SerializeField]
    AudioClip speakingClip;

    AudioSource walkingSource;
    AudioSource speakingSource;
    bool shouldAnimateMoving = false;

    int playerGroundProtectionCount = 0;
    public float playerGroundProtectionY = 0.0f;

    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
        walkingSource = GetComponent<AudioSource>();
        speakingSource = GetComponents<AudioSource>()[1];
        rb = GetComponent<Rigidbody>();
        DungeonMaster.Instance.player = this;

        if (equippedAbility && equippedAbility.GetComponent<Armor>())
            equippedAbility.GetComponent<Armor>().SetArmor();
    }

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat(walkingValue, moveSpeed);
        standardSpeed = moveSpeed;
    }

    private void Update()
    {

        float moveSpeedX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveSpeedZ = Input.GetAxis("Vertical") * moveSpeed;


        moveInput = new Vector3(moveSpeedX, 0f, moveSpeedZ);
        shouldAnimateMoving = moveInput != Vector3.zero;


        moveVelocity = Vector3.ClampMagnitude(moveInput, moveSpeed);

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
            UseAbility();
        }
        

       
    }

    private void UseAbility()
    {
        if (Input.GetKey(KeyCode.G) && timeBtwShots <= 0 && characterStats.ammoAbility > 0)
        {
            timeBtwShots = startTimeBtwShots;

            GameObject grenade = Instantiate(granadePrefab, transform.position, transform.rotation);
            grenade.transform.position = transform.position + grenade.transform.forward;
            grenade.GetComponent<Grenade>().StartCountdown();
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.velocity = grenade.transform.forward * 10;

            characterStats.SetAbilityAmmo(-1);
            if(characterStats.ammoAbility <= 0)
            {
                Destroy(equippedAbility);
            }
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
                CancelInvoke(nameof(SpeakingChicken));
                animator.Play(walkingState);
                walkingSource.Play();
            }
            else if (!shouldAnimateMoving && !animator.GetCurrentAnimatorStateInfo(0).IsName(standingState))
            {
                animator.CrossFade(standingState, 0.0f);
                walkingSource.Stop();
                Invoke(nameof(SpeakingChicken), 2);
            }
        }
    }

    void SpeakingChicken()
    {
        speakingSource.clip = speakingClip;
        speakingSource.Play();
    }

    public void PlaySpeakSound(AudioClip clip = null)
    {
        if (clip == null)
            speakingSource.clip = speakingClip;

        speakingSource.clip = clip;
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
            GameObject oldItem = null;

            // Is this a Weapon
            if (newEquip.GetComponent<Weapon>())
            {
                oldItem = equippedWeapon;
                Destroy(equippedWeapon);
                // Instantiate the new Weapon
                equippedWeapon = Instantiate(newEquip.gameObject, weaponSlot.transform);
                OnSpeedChange(standardSpeed * newEquip.moveSpeedMultiplier);
            }
            else if(newEquip.GetComponent<Ability>())
            {
                if(equippedAbility == null)
                {
                    equippedAbility = Instantiate(newEquip.gameObject, abilitySlot.transform);
                    equippedAbility.GetComponent<Rigidbody>().detectCollisions = false;
                    equippedAbility.GetComponent<Rigidbody>().isKinematic = true;
                    characterStats.SetAbilityAmmo(1);
                }
                else
                {
                    if(newEquip.item.Equals(equippedAbility.GetComponent<EquipAbleItem>().item))
                    {
                        characterStats.SetAbilityAmmo(1);
                    }
                    else
                    {
                        oldItem = equippedAbility;
                        // Write Code to Equip on Itemslot instead of Weaponslot
                        Destroy(equippedAbility);
                        equippedAbility = Instantiate(newEquip.gameObject, abilitySlot.transform);
                        equippedAbility.GetComponent<Rigidbody>().detectCollisions = false;
                        equippedAbility.GetComponent<Rigidbody>().isKinematic = true;
                        characterStats.SetAbilityAmmo(1);
                    }
                }
            }
            else if (newEquip.GetComponent<Armor>())
            {
                if (equippedArmor == null)
                {
                    equippedArmor = Instantiate(newEquip.gameObject, armorSlot.transform);

                    equippedArmor.GetComponent<Armor>().SetArmor();
                }
                else
                {
                    oldItem = equippedArmor;
                    Destroy(equippedArmor);

                    equippedArmor = Instantiate(newEquip.gameObject, armorSlot.transform);

                    equippedArmor.GetComponent<Armor>().SetArmor();
                }
            }


            if (oldItem)
            {
                // Return old Weapon back to Collectable
                return oldItem.GetComponent<EquipAbleItem>();
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

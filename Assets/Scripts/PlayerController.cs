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

    public float moveSpeed = 20f;

    public Weapon[] weapons;
    public GameObject equippedWeapon;
    public Transform weaponSlot;

    Animator animator;

    [SerializeField]
    string walkingState = "Walking";
    [SerializeField]
    string standingState = "Standing";

    [SerializeField]
    string walkingValue = "WalkingMultiplier";


    bool shouldAnimateMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        DungeonMaster.Instance.player = this;
        animator.SetFloat(walkingValue, moveSpeed);
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
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVelocity;

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(walkingState) && shouldAnimateMoving)
            animator.Play(walkingState);
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName(standingState) && !shouldAnimateMoving)
            animator.CrossFade(standingState, 0.0f);
    }

    /// <summary>
    /// Tauscht die Waffe auf dem Boden mit der jetzigen aus
    /// </summary>
    /// <param name="weapon">Waffe auf dem Boden</param>
    /// <returns>Ausgerüstete Waffe</returns>
    public Weapon EquipWeapon(ItemText weapon)
    {

        // Finde die waffe aus den vorhanden Waffen
        Weapon newWeapon = weapons.FirstOrDefault(w => w.item.Equals(weapon));

        if(newWeapon == null)
        {
            // Keine Waffe gefunden also return
            return null;
        }
        else
        {
            Weapon oldWeapon = equippedWeapon.GetComponent<Weapon>();
                
            Destroy(equippedWeapon);


            // Instantiate the new Weapon
            equippedWeapon = Instantiate(newWeapon.gameObject, weaponSlot.position, weaponSlot.rotation, weaponSlot.transform);

            // Return old Weapon back to Collectable
            return oldWeapon;
        }
    }

    /// <summary>
    /// Wenn eine Waffe oder eine Fähigkeit die Geschwindigkeit des Spielers ändert
    /// </summary>
    /// <param name="speed"></param>
    public void OnSpeedChange(float speed)
    {
        animator.SetFloat(walkingValue, moveSpeed);
        moveSpeed = speed;
    }
}

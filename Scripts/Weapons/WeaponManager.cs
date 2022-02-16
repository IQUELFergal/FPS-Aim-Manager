using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations.Rigging;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using System;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Rig handIK;
    [SerializeField] private Transform weaponParent;
    [SerializeField] private Transform weaponLeftGrip;
    [SerializeField] private Transform weaponRightGrip;

    private ProjectileObjectPool projectileObjectPool;

    private AimTargetManager aimTargetManager;
    [SerializeField] private Animator animator;
    private AnimatorOverrideController animatorOverride;
    private PlayerController playerController;
    private PlayerInputMapper inputManager;
    


    public PlayerInputMapper InputManager { get => inputManager; }
    public AimTargetManager AimTargetManager { get => aimTargetManager; }
    public PlayerController PlayerController { get => playerController; }
    public float AimSensitivity { get => aimSensitivity;}
    public CinemachineVirtualCamera AimCamera { get => aimCamera;}
    public ProjectileObjectPool ProjectileObjectPool { get => projectileObjectPool; }
    public Animator Animator { get => animator;}

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputMapper>();
        aimTargetManager = GetComponent<AimTargetManager>();
        animator = GetComponentInChildren<Animator>();
        animatorOverride = animator.runtimeAnimatorController as AnimatorOverrideController;
        playerController = GetComponent<PlayerController>();
        projectileObjectPool = FindObjectOfType<ProjectileObjectPool>();
        if (currentWeapon != null)
        {
            handIK.weight = 0;
        }
    }

    public void OnRightClickDown(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnRightClickDown();
        }
    }

    public void OnRightClickUp(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnRightClickUp();
        }
    }

    public void OnReload(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnReload();
        }
    }

    public void OnHolster(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentWeapon != null)
        {
            weapons.RemoveAt(weapons.IndexOf(currentWeapon));
            Destroy(currentWeapon.gameObject);
            
        }
        handIK.weight = 0.0f;
        animator.SetLayerWeight(1, 0.0f);
        animatorOverride["weapon_anim_empty"] = null;
    }

    public void OnLeftClickDown(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnLeftClickDown();
        }
    }

    public void OnLeftClickUp(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentWeapon != null)
        {
            currentWeapon?.OnLeftClickUp();
        }
    }

    private void Start()
    {
        DisablePlayerRotation();
        Weapon existingWeapon = GetComponentInChildren<Weapon>();
        if (existingWeapon != null)
        {
            EquipWeapon(existingWeapon);
        }
    }


    public void EquipWeapon(Weapon newWeapon)
    {
        if (!weapons.Contains(newWeapon))
        {
            newWeapon.transform.SetParent(weaponParent);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.transform.localScale = Vector3.one;
            weapons.Add(newWeapon);
        }
        foreach (var weapon in weapons)
        {
            weapon.gameObject.SetActive(false);
        }
        currentWeapon = newWeapon;
        currentWeapon.gameObject.SetActive(true);
        currentWeapon.OnEquip(this);
        handIK.weight = 1.0f;
        
        Invoke(nameof(SetAnimationDelayed),0.001f);
    }

    void SetAnimationDelayed()
    {
        animatorOverride["weapon_anim_empty"] = currentWeapon.WeaponAnimation;
    }

    public void SetAimCamera()
    {
        aimCamera.gameObject.SetActive(true);
        playerController.SetSensitivity(aimSensitivity);

    }

    public void SetNormalCamera()
    {
        aimCamera.gameObject.SetActive(false);
        playerController.SetSensitivity(normalSensitivity);
    }

    public void DisablePlayerRotation()
    {
        playerController.SetRotateOnMove(false);
    }
    public void EnablePlayerRotation()
    {
        playerController.SetRotateOnMove(true);
    }
    
#if UNITY_EDITOR
    [ContextMenu("Save Weapon Pose")]
    public void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(currentWeapon.WeaponAnimation);
        UnityEditor.AssetDatabase.SaveAssets();
    }
#endif
}

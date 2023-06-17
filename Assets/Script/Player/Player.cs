using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// 1. 璽砫北à︹ち传家
/// 2. ち传à︹も珇家
/// 3. 北à︹UI戈癟(EX: ﹀秖à︹が笆UI)
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    /// <summary>
    /// Anout character models
    /// </summary>
    [SerializeField] private GameObject[] character_models;// using this to switch player model
    private int model_num = 0;
    private GameObject my_model;
    private Controller mController;

    /// <summary>
    /// About weapon
    /// </summary>
    [SerializeField] private GameObject[] weapon_models;
    private int weapon_num = 0;
    private GameObject my_weapon;
    [SerializeField] private Transform weapon_holder;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        mController = GetComponent<Controller>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter(model_num);
        DontDestroyOnLoad(this.gameObject);
        //InitWeapon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeCharacter(int num)
    {
        my_model = Instantiate(character_models[num], transform);
        mController.ChangeCharacter(my_model.GetComponent<CharacterController>());
        Camera c = my_model.transform.GetChild(1).GetChild(0).GetComponent<Camera>();
        GameManager.Instance.SetPlayerCamera(c);
    }

    void InitWeapon()
    {
        weapon_holder = my_model.transform.Find("Weapon Holder");
        my_weapon = Instantiate(weapon_models[weapon_num], weapon_holder);

        var left_hand_pose = weapon_holder.transform.GetChild(0);
        var right_hand_pose = weapon_holder.transform.GetChild(1);
        var my_weapon_model = weapon_holder.transform.GetChild(2);

        left_hand_pose.position = my_weapon_model.GetChild(0).position;
        left_hand_pose.rotation = my_weapon_model.GetChild(0).rotation;

        right_hand_pose.position = my_weapon_model.GetChild(1).position;
        right_hand_pose.rotation = my_weapon_model.GetChild(1).rotation;
    }

    public Vector3 PlayerPosition()
    {
        return my_model.transform.position;
    }

}

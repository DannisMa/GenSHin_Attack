using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Anout character models
    /// </summary>
    [SerializeField] private GameObject[] character_models;// using this to switch player model
    private int model_num = 0;
    private GameObject my_model;

    /// <summary>
    /// About weapon
    /// </summary>
    [SerializeField] private GameObject[] weapon_models;
    private int weapon_num = 0;
    private GameObject my_weapon;
    [SerializeField] private Transform weapon_holder;

    // Start is called before the first frame update
    void Start()
    {
        my_model = Instantiate(character_models[model_num], transform.position, transform.rotation);
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

    // Update is called once per frame
    void Update()
    {

    }

}

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
    private Transform weapon_holder;

    // Start is called before the first frame update
    void Start()
    {
        my_model = Instantiate(character_models[model_num], transform.position, transform.rotation);
        weapon_holder = my_model.transform.Find("Weapon Holder");
        my_weapon = Instantiate(weapon_models[weapon_num], weapon_holder);
    }

    // Update is called once per frame
    void Update()
    {

    }

}

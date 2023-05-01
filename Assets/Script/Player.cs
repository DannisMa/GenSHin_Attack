using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    /// <summary>
    /// Anout character models
    /// </summary>
    [SerializeField] private GameObject[] character_models;// using this to switch player model
    [SerializeField] private Transform model_point;
    [SerializeField] private int model_num = 0;
    [SerializeField] private GameObject my_model;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
            return;

        model_point = this.transform.Find("Model_point");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;
        SpawnCharacterServerRpc(this.NetworkObject.OwnerClientId, model_num);
    }

    [ServerRpc]
    public void SpawnCharacterServerRpc(ulong id, int c_num)
    {
        if (!IsServer)
            return;
        my_model = Instantiate(character_models[model_num], model_point);
        my_model.GetComponent<NetworkObject>().SpawnWithOwnership(id, true);
    }

}

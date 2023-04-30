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
    [SerializeField] GameObject[] character_models;// using this to switch player model
    [SerializeField] Transform model_point;
    int model_num = 0;
    GameObject my_model;
    Rigidbody my_model_rd;

    //private NetworkVariable<Vector3> player_network_position = new NetworkVariable<Vector3>();
    private Vector2 move_vector = Vector2.zero;

    //ActionMap generate C# script
    PlayerControl player_control;


    // Start is called before the first frame update
    void Start()
    {
        model_point = this.transform.Find("Model_point");
        my_model = Instantiate(character_models[model_num], model_point);
        my_model_rd = my_model.GetComponent<Rigidbody>();

        player_control = new PlayerControl();
        player_control.PlayerNormal.Enable();
        player_control.PlayerNormal.Jump.performed += Jump;
        player_control.PlayerNormal.Move.performed += Move;
        player_control.PlayerNormal.Move.canceled += Move;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = player_network_position.Value;

        if (!IsOwner)
            return;
        if(move_vector != Vector2.zero)
        {
            transform.position += new Vector3(move_vector.x, 0, move_vector.y) * GameConst.player_walk_speed * Time.deltaTime;
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        //if(NetworkManager.Singleton.IsServer)
        //{
        //    Vector3 randomPosition = GetRandomPositionOnPlane();
        //    transform.position = randomPosition;
        //    player_network_position.Value = randomPosition;
        //}
        //else
        //{
        //    SubmitPositionRequestServerRpc();
        //}
        //SubmitPositionRequestServerRpc();
    }

    //[ServerRpc]
    //private void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    //{
    //    player_network_position.Value = GetRandomPositionOnPlane();
    //}
    

    private Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(UnityEngine.Random.Range(-3f, 3f), 1f, UnityEngine.Random.Range(-3f, 3f));
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if(IsOwner && ctx.performed)
        {
            my_model_rd.AddForce(Vector3.up * GameConst.player_jump_height, ForceMode.Force);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if(IsOwner)
            move_vector = ctx.ReadValue<Vector2>();
    }
}

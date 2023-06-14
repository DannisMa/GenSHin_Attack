using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDemoSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawnPoint;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        InitPlayer();
        PlayerInputSystem.Instance.PlayingMode();
    }

    private void InitPlayer()
    {
        player.transform.position = new Vector3(0,0,0);
        player.transform.GetChild(1).transform.position = respawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

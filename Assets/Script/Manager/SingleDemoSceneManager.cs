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
        player.transform.position = Vector3.zero;
        player.transform.GetChild(0).position = respawnPoint.position;
        PlayerInputSystem.Instance.PlayingMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

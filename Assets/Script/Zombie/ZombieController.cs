using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private int hp = 100;
    private float moveSpeed = 10.0f;
    private Transform player = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Player.Instance.PlayerPosition());
    }
}

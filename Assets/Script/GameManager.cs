using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private GameObject playerPrefabs;

    public GameObject MainCamera;

    private void Awake()
    {
        GameObject.Find("Main Camera").SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlayer()
    {
        Instantiate(playerPrefabs, playerSpawnPoint.position, playerSpawnPoint.rotation);
    }
}

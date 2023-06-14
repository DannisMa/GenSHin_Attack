using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map : Item
{
    [SerializeField] private Transform canvasObject;
    [SerializeField] private Button demoBtn;

    private void Awake()
    {
        canvasObject = this.transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //設定物名稱，用以顯示在UI
        name = "Map";

        demoBtn.onClick.AddListener(LoadScene);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void Effect(InputAction.CallbackContext ctx)
    {
        Debug.Log("打開地圖");
        PlayerInputSystem.Instance.BigUIMode();
        canvasObject.gameObject.SetActive(true);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("SingleDemoScene");
    }
}

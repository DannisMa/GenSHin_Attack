using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map : Item
{
    [SerializeField] private Transform canvasObject;
    // Start is called before the first frame update
    void Start()
    {
        name = "Map";
        canvasObject = this.transform.GetChild(0);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map : Item
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Map";
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public override void Effect(InputAction.CallbackContext ctx)
    {
        Debug.Log("打開地圖");
    }
}

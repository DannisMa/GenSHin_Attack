using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Item : MonoBehaviour
{
    protected new string name;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetName()
    {
        return name;
    }

    public virtual void Effect(InputAction.CallbackContext ctx)
    {
        Debug.Log("物品效果");
    }
}

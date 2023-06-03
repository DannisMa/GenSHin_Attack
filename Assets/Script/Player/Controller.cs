using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private PlayerControl playerControl;
    private CharacterController mController;
    private Transform cameraHolder;

    private Vector3 mMove = new Vector3(0, 0, 0);
    private float turnCam = 0.0f;
    private float gravityValue = -9.81f;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
        playerControl = new PlayerControl();
        playerControl.PlayerNormal.Enable();
        playerControl.PlayerNormal.Move.performed += Move;
        playerControl.PlayerNormal.Move.canceled += Move;
        playerControl.PlayerNormal.View.performed += View;
        playerControl.PlayerNormal.View.canceled += View;

        playerControl.Pause.Enable();
        playerControl.Pause.Esc.performed += Esc;
    }

    // Update is called once per frame
    void Update()
    {
        if (mController == null)
            return;
        Vector3 moveDirectionForward = mController.transform.forward * mMove.z ;
        Vector3 moveDirectionSide = mController.transform.right * mMove.x;
        Vector3 _move = moveDirectionForward +moveDirectionSide;
        if (!mController.isGrounded)
        {
            _move.y += gravityValue * Time.deltaTime * 10;
        }
        mController.Move(_move * Time.deltaTime * 10);
    }

    public void ChangeCharacter(CharacterController _c)
    {
        mController = _c;
        cameraHolder = mController.transform.GetChild(1);
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        //Debug.Log($"MoveFB : {ctx.ReadValue<Vector2>()}");
        if (mController == null)
            return;
        mMove = new Vector3(ctx.ReadValue<Vector2>().x, 0, ctx.ReadValue<Vector2>().y);
    }

    private void View(InputAction.CallbackContext ctx)
    {
        //Debug.Log($"Mouse : {ctx.ReadValue<Vector2>()}");
        float turnPlayer = ctx.ReadValue<Vector2>().x * 10;
        mController.transform.Rotate(Vector3.up * turnPlayer * Time.deltaTime);

        turnCam -= ctx.ReadValue<Vector2>().y * 0.1f;
        turnCam = Mathf.Clamp(turnCam, -70f, 70f);
        cameraHolder.localRotation = Quaternion.Euler(turnCam, 0, 0);
    }

    private void Esc(InputAction.CallbackContext obj)
    {
        if (Cursor.visible)
        {
            Debug.Log("Âê©w·Æ¹«");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Debug.Log("¸Ñ©ñ·Æ¹«");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

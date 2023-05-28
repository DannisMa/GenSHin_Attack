using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System;

public class PlayerControlor : MonoBehaviour
{
    private PlayerControl player_control;
    private Rigidbody rd;
    private CapsuleCollider collision;
    private Transform cam_holder;
    private Camera player_camera;

    private bool isGround = false;
    private Vector3 move_vector = new Vector3(0,0,0);
    private float camera_rotation_vertical = 0.0f;

    private Transform aim_pose;
    private Transform mouse_pose;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        collision = GetComponent<CapsuleCollider>();

        cam_holder = transform.Find("Camera Holder");
        player_camera = cam_holder.GetChild(0).gameObject.AddComponent<Camera>();

        mouse_pose = cam_holder.transform.Find("Mouse Pose");
        aim_pose = transform.Find("Weapon Aim Pose");
        aim_pose.position = mouse_pose.position;

        player_control = new PlayerControl();
        player_control.PlayerNormal.Enable();
        player_control.PlayerNormal.Jump.performed += Jump;
        player_control.PlayerNormal.Move_fb.performed += Move_fb;
        player_control.PlayerNormal.Move_lr.performed += Move_lr;
        player_control.PlayerNormal.View.performed += View;
    }

    private void View(InputAction.CallbackContext obj)
    {
        Vector2 temp_v = obj.ReadValue<Vector2>() * GameConst.mouseSensitivity * Time.deltaTime;
        //make character rotation left and right
        this.transform.Rotate(Vector3.up * temp_v.x);

        //make camera rotation up and down
        //cam_holder.Rotate(Vector3.left * temp_v.y); //this way hard to limit up and dowm
        camera_rotation_vertical -= temp_v.y;
        camera_rotation_vertical = Mathf.Clamp(camera_rotation_vertical, -70f, 70f);
        cam_holder.localRotation = Quaternion.Euler(camera_rotation_vertical, 0, 0);

        aim_pose.position = mouse_pose.position;
    }


    // Update is called once per frame
    void Update()
    {
        
        Vector3 mv = (rd.rotation * move_vector.normalized * GameConst.player_walk_speed * Time.deltaTime);

        if (mv != Vector3.zero)
        {
            rd.MovePosition(mv + transform.localPosition);
        }
        if (isGround)
        {
            if (move_vector == Vector3.zero)
            {
                changeAnimation(PlayerAnimationEnum.rifleAimingIdle.ToString());
            }
            else
            {
                changeAnimation(PlayerAnimationEnum.rifleWalk.ToString());
            }
        }

    }

    private void changeAnimation(string na)//new animation
    {

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGround)
        {
            rd.AddForce(Vector3.up * GameConst.player_jump_height, ForceMode.Force);
            isGround = false;
            changeAnimation(PlayerAnimationEnum.rifleJump.ToString());
        }
    }

    private void Move_fb(InputAction.CallbackContext ctx)
    {
        move_vector.z = ctx.ReadValue<Vector2>().y;
    }

    private void Move_lr(InputAction.CallbackContext ctx)
    {
        move_vector.x  = ctx.ReadValue<Vector2>().x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        { 
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            isGround = false;
    }

    void OnDestroy()
    {
        player_control.PlayerNormal.Jump.performed -= Jump;
        player_control.PlayerNormal.Move_fb.performed -= Move_fb;
        player_control.PlayerNormal.Move_lr.performed -= Move_lr;
        player_control.PlayerNormal.View.performed -= View;
        player_control.PlayerNormal.Disable();
    }
}

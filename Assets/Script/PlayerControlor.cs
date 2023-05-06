using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System;

public class PlayerControlor : NetworkBehaviour
{
    private PlayerControl player_control;
    private Rigidbody rd;
    private CapsuleCollider collision;
    private Transform cam_holder;
    private Animator anim;

    [SerializeField] private bool isGround = false;
    private Vector3 move_vector = new Vector3(0,0,0);
    private float camera_rotation_vertical = 0.0f;
    [SerializeField] private string current_animation = "";

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
            return;
        rd = GetComponent<Rigidbody>();
        collision = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        cam_holder = transform.Find("Cmaera holder");
        cam_holder.gameObject.AddComponent<Camera>();

        player_control = new PlayerControl();
        player_control.PlayerNormal.Enable();
        player_control.PlayerNormal.Jump.performed += Jump;
        player_control.PlayerNormal.Move_fb.performed += Move_fb;
        player_control.PlayerNormal.Move_lr.performed += Move_lr;
        player_control.PlayerNormal.View.performed += View;

        
    }

    private void View(InputAction.CallbackContext obj)
    {
        if (!IsOwner)
            return;
        Vector2 temp_v = obj.ReadValue<Vector2>() * GameConst.mouseSensitivity * Time.deltaTime;
        
        //make character rotation left and right
        this.transform.Rotate(Vector3.up * temp_v.x);

        //make camera rotation up and down
        //cam_holder.Rotate(Vector3.left * temp_v.y); //this way hard to limit up and dowm
        camera_rotation_vertical -= temp_v.y;
        camera_rotation_vertical = Mathf.Clamp(camera_rotation_vertical, -70f, 70f);
        cam_holder.localRotation = Quaternion.Euler(camera_rotation_vertical, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

        Vector3 mv = this.transform.localPosition + (rd.rotation * move_vector.normalized * GameConst.player_walk_speed * Time.deltaTime);
        rd.MovePosition(mv);

        if(isGround)
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
        if (na.CompareTo(current_animation) == 0)
            return;

        anim.Play(na);

        current_animation = na;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (IsOwner && ctx.performed && isGround)
        {
            rd.AddForce(Vector3.up * GameConst.player_jump_height, ForceMode.Force);
            isGround = false;
            changeAnimation(PlayerAnimationEnum.rifleJump.ToString());
        }
    }

    private void Move_fb(InputAction.CallbackContext ctx)
    {
        if (!IsOwner)
            return;
        move_vector.z = ctx.ReadValue<Vector2>().y;
    }

    private void Move_lr(InputAction.CallbackContext ctx)
    {
        if (!IsOwner)
            return;
        move_vector.x  = ctx.ReadValue<Vector2>().x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner)
            return;

        if (collision.transform.CompareTag("Ground"))
        { 
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!IsOwner)
            return;

        if (collision.transform.CompareTag("Ground"))
            isGround = false;
    }

    public override void OnDestroy()
    {
        if (!IsOwner)
            return;
        player_control.PlayerNormal.Jump.performed -= Jump;
        player_control.PlayerNormal.Move_fb.performed -= Move_fb;
        player_control.PlayerNormal.Move_lr.performed -= Move_lr;
        player_control.PlayerNormal.View.performed -= View;
        player_control.PlayerNormal.Disable();
    }
}

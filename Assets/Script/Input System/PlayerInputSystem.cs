using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem
{
    /// <summary>
    /// 控制玩家操作方式
    /// </summary>
    private static PlayerInputSystem instance = null;
    public static PlayerInputSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerInputSystem();
            }
            return instance;
        }
    }

    private static PlayerControl _inputSystem = null;
    public PlayerControl InputSystem
    {
        get
        { 
            if(_inputSystem == null)
            {
                _inputSystem = new PlayerControl();
            }
            return _inputSystem;
        }
    }

    /// <summary>
    /// 普通遊玩模式
    /// </summary>
    public void PlayingMode()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _inputSystem.PlayerNormal.Enable();
        _inputSystem.Pause.Enable();
        _inputSystem.InteractionUI.Enable();
    }

    /// <summary>
    /// 啟用暫停選單
    /// </summary>
    public void ResumeMode()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _inputSystem.PlayerNormal.Disable();
        _inputSystem.Pause.Enable();
        _inputSystem.InteractionUI.Disable();
    }

    /// <summary>
    /// 打開背包，地圖等覆蓋大部分畫面的UI
    /// </summary>
    public void BigUIMode()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _inputSystem.PlayerNormal.Disable();
        _inputSystem.Pause.Disable();
        _inputSystem.InteractionUI.Enable();
    }
}

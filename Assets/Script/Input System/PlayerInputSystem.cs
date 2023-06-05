using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem
{
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

    public void PlayingMode()
    {
        _inputSystem.PlayerNormal.Enable();
        _inputSystem.Pause.Enable();
        _inputSystem.InteractionUI.Enable();
    }

    public void ResumeMode()
    {
        _inputSystem.PlayerNormal.Disable();
        _inputSystem.Pause.Enable();
        _inputSystem.InteractionUI.Disable();
    }

    public void BigUIMode()
    {
        _inputSystem.PlayerNormal.Disable();
        _inputSystem.Pause.Enable();
        _inputSystem.InteractionUI.Enable();
    }
}

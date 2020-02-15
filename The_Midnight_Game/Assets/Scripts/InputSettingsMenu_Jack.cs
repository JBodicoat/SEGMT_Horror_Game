using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityStandardAssets.Characters.FirstPerson;

public enum PlayerAction
{
    Jump,
    PourSalt,
    LightCandle,
    GrabDrop,
    Throw,
    SizeOf
}

public class InputSettingsMenu_Jack : MonoBehaviour
{
    public Image jumpSelect;
    public Image saltSelect;
    public Image candleSelect;
    public Image grabSelect;
    public Image throwSelect;

    private PlayerAction selected = PlayerAction.Jump;

    private const PlayerAction lastPlayerAction = PlayerAction.SizeOf - 1;

    private InputDevice inputDevice;

    public FirstPersonController_Jack playerScript;

    private const ushort playerActionSizeOf = (ushort)PlayerAction.SizeOf;

    struct ControllerBinding
    {
        public PlayerAction action;
        public InputControlType controlType;
    }

    private ControllerBinding[] controllerBindings = new ControllerBinding[(int)PlayerAction.SizeOf];

    private struct Key
    {
        public KeyCode code;
        public bool pressed;
    }

    private Key[] keys = new Key[20];

    // Start is called before the first frame update
    void Start()
    {
        controllerBindings[0].action = PlayerAction.Jump;
        controllerBindings[0].controlType = InputControlType.Action1;

        controllerBindings[1].action = PlayerAction.PourSalt;
        controllerBindings[1].controlType = InputControlType.Action2;

        controllerBindings[2].action = PlayerAction.LightCandle;
        controllerBindings[2].controlType = InputControlType.Action3;

        controllerBindings[3].action = PlayerAction.GrabDrop;
        controllerBindings[3].controlType = InputControlType.LeftTrigger;

        controllerBindings[4].action = PlayerAction.Throw;
        controllerBindings[4].controlType = InputControlType.RightTrigger;

        keys[0].code = KeyCode.A;
        keys[0].pressed = false;

        keys[1].code = KeyCode.B;
        keys[1].pressed = false;

        inputDevice = InputManager.ActiveDevice;
    }

    // Update is called once per frame
    void Update()
    {
        inputDevice = InputManager.ActiveDevice;
        if(InputManager.Devices.Count > 0)
        {
            if(inputDevice.Direction.Down.WasPressed)
            {
                IncrementSelect();
            }
            else if(inputDevice.Direction.Up.WasPressed)
            {
                DecrementSelect();
            }

            InputControl button = inputDevice.AnyControl;

            if (button)
            {
                if (button.Target >= InputControlType.Action1 && button.Target <= InputControlType.RightBumper)
                    {
                    switch (selected)
                    {
                        case PlayerAction.Jump:
                            if (ChangeControlType(PlayerAction.Jump, button.Target))
                            {
                                playerScript.SetJumpControlType(button.Target);
                            }
                            break;
                        case PlayerAction.PourSalt:
                            if (ChangeControlType(PlayerAction.PourSalt, button.Target))
                            {
                                playerScript.SetSaltControlType(button.Target);
                            }
                            break;
                        case PlayerAction.LightCandle:
                            if (ChangeControlType(PlayerAction.LightCandle, button.Target))
                            {
                                playerScript.SetCandleControlType(button.Target);
                            }
                            break;
                        case PlayerAction.GrabDrop:
                            if (ChangeControlType(PlayerAction.GrabDrop, button.Target))
                            {
                                playerScript.SetGrabControlType(button.Target);
                            }
                            break;
                        case PlayerAction.Throw:
                            if (ChangeControlType(PlayerAction.Throw, button.Target))
                            {
                                playerScript.SetThrowControlType(button.Target);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                IncrementSelect();
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                DecrementSelect();
            }

            if(Input.anyKeyDown)
            {
                for(int i = 0; i < keys.Length; i++)
                {
                    if(keys[i].pressed)
                    {
                        switch (selected)
                        {
                            case PlayerAction.Jump:
                                playerScript.SetJumpKey(keys[i].code);
                                break;
                            case PlayerAction.PourSalt:
                                
                                break;
                            case PlayerAction.LightCandle:
                                
                                break;
                            case PlayerAction.GrabDrop:
                                
                                break;
                            case PlayerAction.Throw:
                                
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }

    private bool ChangeControlType(PlayerAction action, InputControlType controlType)
    {
        ushort index = 0;
        for (ushort i = 0; i < playerActionSizeOf; i++)
        {
            if (controllerBindings[i].controlType == controlType)
            {
                return false;
            }
            else if(controllerBindings[i].action == action)
            {
                index = i;
            }
        }

        controllerBindings[index].controlType = controlType;
        return true;
    }

    private void IncrementSelect()
    {
        if (++selected >= PlayerAction.SizeOf)
        {
            selected = 0;
        }
        ChangeSelection();
    }

    private void DecrementSelect()
    {
        if (--selected < 0)
        {
            selected = PlayerAction.SizeOf - 1;
        }
        ChangeSelection();
    }

    private void ChangeSelection()
    {
        switch (selected)
        {
            case PlayerAction.Jump:
                throwSelect.enabled = false;
                jumpSelect.enabled = true;
                saltSelect.enabled = false;
                break;
            case PlayerAction.PourSalt:
                jumpSelect.enabled = false;
                saltSelect.enabled = true;
                candleSelect.enabled = false;
                break;
            case PlayerAction.LightCandle:
                saltSelect.enabled = false;
                candleSelect.enabled = true;
                grabSelect.enabled = false;
                break;
            case PlayerAction.GrabDrop:
                candleSelect.enabled = false;
                grabSelect.enabled = true;
                throwSelect.enabled = false;
                break;
            case PlayerAction.Throw:
                grabSelect.enabled = false;
                throwSelect.enabled = true;
                jumpSelect.enabled = false;
                break;
        }
    }
}

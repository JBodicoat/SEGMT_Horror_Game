// Jack : 15/02/2020 - Created script
// Jack 19/03/2020 - Removed jump

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityStandardAssets.Characters.FirstPerson;

/// Types of possible actions the player can do.
public enum PlayerAction
{
    PourSalt,
    LightCandle,
    Interact,
    GrabDrop,
    Throw,
    SizeOf
}

/// Handles changing the games input and it's input menu.
public class InputSettingsManager_Jack : MonoBehaviour
{
    public FirstPersonController_Jack playerScript;

    public GameObject inputSettingsMenu;

    public Image saltSelect;
    public Image candleSelect;
    public Image interactSelect;
    public Image grabSelect;
    public Image throwSelect;

    private PlayerAction selected = 0;

    private InputDevice inputDevice;
   
    private const ushort playerActionSizeOf = (ushort)PlayerAction.SizeOf;

    private struct ControllerBinding
    {
        public PlayerAction action;
        public InputControlType controlType;
    }
    private ControllerBinding[] controllerBindings = new ControllerBinding[playerActionSizeOf];

    private struct KeyBinding
    {
        public PlayerAction action;
        public KeyCode code;
    }
    private KeyBinding[] keyBindings = new KeyBinding[playerActionSizeOf];

    // Start is called before the first frame update
    void Start()
    {
        // Set default controller bindings
        controllerBindings[0].action = PlayerAction.PourSalt;
        controllerBindings[0].controlType = InputControlType.Action2;

        controllerBindings[1].action = PlayerAction.LightCandle;
        controllerBindings[1].controlType = InputControlType.Action4;

        controllerBindings[2].action = PlayerAction.Interact;
        controllerBindings[2].controlType = InputControlType.Action3;

        controllerBindings[3].action = PlayerAction.GrabDrop;
        controllerBindings[3].controlType = InputControlType.LeftTrigger;

        controllerBindings[4].action = PlayerAction.Throw;
        controllerBindings[4].controlType = InputControlType.RightTrigger;

        // Set default keyboard + mouse bindings
        keyBindings[0].action = PlayerAction.PourSalt;
        keyBindings[0].code = KeyCode.Q;

        keyBindings[1].action = PlayerAction.LightCandle;
        keyBindings[1].code = KeyCode.F;

        keyBindings[2].action = PlayerAction.Interact;
        keyBindings[2].code = KeyCode.E;

        keyBindings[3].action = PlayerAction.GrabDrop;
        keyBindings[3].code = KeyCode.Mouse0;

        keyBindings[4].action = PlayerAction.Throw;
        keyBindings[4].code = KeyCode.Mouse1;

        inputDevice = InputManager.ActiveDevice;
    }

    // Update is called once per frame
    void Update()
    {
        inputDevice = InputManager.ActiveDevice;
        if(InputManager.Devices.Count > 0)
        {
            // Controller Inputs
            if (inputDevice.MenuWasPressed)
            {
                inputSettingsMenu.SetActive(!inputSettingsMenu.activeSelf);
            }

            if(inputSettingsMenu.activeSelf)
            { 
                if (inputDevice.Direction.Down.WasPressed)
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
                        if (ChangeControlType(selected, button.Target))
                        {
                            switch (selected)
                            {
                                case PlayerAction.PourSalt:
                                    playerScript.SetSaltControlType(button.Target);
                                    break;
                                case PlayerAction.LightCandle:
                                    playerScript.SetCandleControlType(button.Target);
                                    break;
                                case PlayerAction.GrabDrop:
                                    playerScript.SetGrabControlType(button.Target);
                                    break;
                                case PlayerAction.Throw:
                                    playerScript.SetThrowControlType(button.Target);
                                    break;
                                case PlayerAction.Interact:
                                    playerScript.SetInteractControlType(button.Target);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // Keyboard Inputs
            if (Input.GetKeyDown(KeyCode.K))
            {
                inputSettingsMenu.SetActive(!inputSettingsMenu.activeSelf);
            }

            if (inputSettingsMenu.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    IncrementSelect();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    DecrementSelect();
                }

                if (Input.anyKeyDown)
                {
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        if (Input.GetKeyDown(keyCode) && keyCode != KeyCode.Escape
                                                     && keyCode != KeyCode.UpArrow
                                                     && keyCode != KeyCode.DownArrow
                                                     && keyCode != KeyCode.K)// To be removed
                        {
                            if (ChangeKey(selected, keyCode))
                            {
                                switch (selected)
                                {
                                    case PlayerAction.PourSalt:
                                        playerScript.SetSaltKey(keyCode);
                                        break;
                                    case PlayerAction.LightCandle:
                                        playerScript.SetCandleKey(keyCode);
                                        break;
                                    case PlayerAction.GrabDrop:
                                        playerScript.SetGrabKey(keyCode);
                                        break;
                                    case PlayerAction.Throw:
                                        playerScript.SetThrowKey(keyCode);
                                        break;
                                    case PlayerAction.Interact:
                                        playerScript.SetThrowKey(keyCode);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }
                        }
                    } // End foreach
                }
            }
        }
    }

    /// <summary>
    /// Changes the InputControlType of the passed PlayerAction.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="controlType"></param>
    /// <returns>Returns true if no other actions are bound to the passed InputControlType.</returns>
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

    /// <summary>
    /// Changes the KeyCode of the passed PlayerAction.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="keyCode"></param>
    /// <returns>Returns true if no other actions are bound to the passed KeyCode.</returns>
    private bool ChangeKey(PlayerAction action, KeyCode keyCode)
    {
        ushort index = 0;
        for(ushort i = 0; i < playerActionSizeOf; i++)
        {
            if(keyBindings[i].code == keyCode)
            {
                return false;
            }
            else if(keyBindings[i].action == action)
            {
                index = i;
            }
        }

        keyBindings[index].code = keyCode;
        return true;
    }

    /// <summary>
    /// Increments the selected player action.
    /// </summary>
    private void IncrementSelect()
    {
        if (++selected >= PlayerAction.SizeOf)
        {
            selected = 0;
        }
        ChangeSelection();
    }

    /// <summary>
    /// Decrements the selected player action.
    /// </summary>
    private void DecrementSelect()
    {
        if (--selected < 0)
        {
            selected = PlayerAction.SizeOf - 1;
        }
        ChangeSelection();
    }

    /// <summary>
    /// Changes the selected player action.
    /// </summary>
    private void ChangeSelection()
    {
        switch (selected)
        {
            case PlayerAction.PourSalt:
                throwSelect.enabled = false;
                saltSelect.enabled = true;
                candleSelect.enabled = false;
                break;

            case PlayerAction.LightCandle:
                saltSelect.enabled = false;
                candleSelect.enabled = true;
                interactSelect.enabled = false;
                break;

            case PlayerAction.Interact:
                candleSelect.enabled = false;
                interactSelect.enabled = true;
                grabSelect.enabled = false;
                break;

            case PlayerAction.GrabDrop:
                interactSelect.enabled = false;
                grabSelect.enabled = true;
                throwSelect.enabled = false;
                break;

            case PlayerAction.Throw:
                grabSelect.enabled = false;
                throwSelect.enabled = true;
                saltSelect.enabled = false;
                break;
        }
    }
}

// Jack 01/04/2020 Created script

using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu_Jack : Menu
{
    private VideoSettings_LouieWilliamson videoSettingsScript;

    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Dropdown textureDropdown;
    public Toggle vsyncToggle;
    
    public Slider masterSlider;
    public Slider ambientSlider;
    public Slider sfxSlider;

    public Button applyButton;
    public Button backButton;

    private const float sliderValueChange = 0.75f;
    private int sliderMove = 0;

    public Text resolutionText;
    private const int numResolutions = 3;
    private int currentResolution = 0;

    private const string nineteen20 = "1920 x 1080";
    private const string thirteen66 = "1366 x 768";
    private const string twelve80 = "1280 x 800";

    public Text textureText;
    private const int numTextureOptions = 3;
    private int textureQuality = 0;

    private const string high = "High";
    private const string medium = "Medium";
    private const string low = "Low";

    void Start()
    {
        videoSettingsScript = FindObjectOfType<VideoSettings_LouieWilliamson>();
    }

    private void Update()
    {
        bool pressButton = false;
        sliderMove = 0;

        if(HasSelectionChanged())
        {
            for(int i = 0; i < buttons.Count; ++i)
            {
                if(buttons[i].IsSelected())
                {
                    selectedButton = i;
                    break;
                }
            }
        }

        InputDevice inputDevice = InputManager.ActiveDevice;
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
            else if(inputDevice.Direction.Right)
            {
                sliderMove += 1;
            }
            else if(inputDevice.Direction.Left)
            {
                sliderMove -= 1;
            }

            if(inputDevice.Action1.WasPressed)
            {
                pressButton = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            IncrementSelect();
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            DecrementSelect();
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            sliderMove += 1;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            sliderMove -= 1;
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            pressButton = true;
        }

        if(sliderMove != 0)
        {
            switch (selectedButton)
            {
                // Resolution dropdown
                case 1:
                    if(inputDevice.Direction.Right.WasPressed || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        SelectNextResolution();
                        
                    }
                    else if(inputDevice.Direction.Left.WasPressed || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        SelectPreviousResolution();
                    }
                    break;

                // Texture dropdown
                case 2:
                    if (inputDevice.Direction.Right.WasPressed || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        SelectNextTextureQuality();

                    }
                    else if (inputDevice.Direction.Left.WasPressed || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        SelectPreviousTextureQuality();
                    }
                    break;

                // Master volume slider
                case 4:
                    masterSlider.value += GetSliderChangeAmount();
                    break;

                // Ambient volume slider
                case 5:
                    ambientSlider.value += GetSliderChangeAmount();
                    break;

                // SFX volume slider
                case 6:
                    sfxSlider.value += GetSliderChangeAmount();
                    break;
            }
        }

        if(pressButton)
        {
            switch(selectedButton)
            {
                // Fullscreen toggle
                case 0:
                    fullscreenToggle.isOn = !fullscreenToggle.isOn;
                    break;

                // VSync toggle
                case 3:
                    vsyncToggle.isOn = !vsyncToggle.isOn;
                    break;

                // Apply button
                case 7:
                    applyButton.onClick.Invoke();
                    break;

                // Back button
                case 8:
                    backButton.onClick.Invoke();
                    break;
            }
        }
    }

    public void SelectNextResolution()
    {
        if(++currentResolution >= numResolutions)
        {
            currentResolution = 0;
        }

        UpdateResolutionText();

        videoSettingsScript.ChangeResolution(currentResolution);
    }

    public void SelectPreviousResolution()
    {
        if(--currentResolution < 0)
        {
            currentResolution = numResolutions - 1;
        }

        UpdateResolutionText();

        videoSettingsScript.ChangeResolution(currentResolution);
    }

    private void UpdateResolutionText()
    {
        switch (currentResolution)
        {
            case 0:
                resolutionText.text = nineteen20;
                break;

            case 1:
                resolutionText.text = twelve80;
                break;

            case 2:
                resolutionText.text = thirteen66;
                break;
        }
    }

    public void SelectNextTextureQuality()
    {
        if(++textureQuality >= numTextureOptions)
        {
            textureQuality = 0;
        }

        UpdateTextureQualityText();

        videoSettingsScript.ChangeTextureQuality(textureQuality);
    }

    public void SelectPreviousTextureQuality()
    {
        if(--textureQuality < 0)
        {
            textureQuality = numTextureOptions - 1;
        }

        UpdateTextureQualityText();

        videoSettingsScript.ChangeTextureQuality(textureQuality);
    }

    private void UpdateTextureQualityText()
    {
        switch(textureQuality)
        {
            case 0:
                textureText.text = high;
                break;

            case 1:
                textureText.text = low;
                break;

            case 2:
                textureText.text = medium;
                break;
        }
    }

    private float GetSliderChangeAmount()
    {
        return sliderValueChange * sliderMove * Time.deltaTime;
    }
}

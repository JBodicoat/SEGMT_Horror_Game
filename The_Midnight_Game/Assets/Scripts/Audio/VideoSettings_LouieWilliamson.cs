// Louie - Created Script - 16-03-2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings_LouieWilliamson : MonoBehaviour
{
    public enum Resolution { NineteenTwenty, ThirteenSixtySix, TwelveEighty };
    public enum TextureQuality { low, medium, high };

    //Used to determine the currently chosen settings
    private Resolution res;
    private TextureQuality tQuality;
    internal bool isFullscreen;
    private bool isVsyncOn;
    private int index;

    public Dropdown resDropdown;
    public Dropdown tqualityDropdown;

    //Resolution Constants

    //1920x1080
    private const int NineteenTwentyWidth = 1920;
    private const int NineteenTwentyHeight = 1080;

    //1366x768
    private const int ThirteenSixtyWidth = 1366;
    private const int ThirteenSixtyHeight = 768;

    //1280x800
    private const int TwelveEightyWidth = 1280;
    private const int TwelveEightyHeight = 800;

    //Texture Quality Constants
    private const int lowTextureValue = 2;
    private const int mediumTextureValue = 1;
    private const int highTextureValue = 0;

    //VSync Constants
    private const int vsyncON = 1;
    private const int vsyncOFF = 0;

    void Start()
    {
        isFullscreen = false;
        isVsyncOn = false;
        res = Resolution.NineteenTwenty;
        tQuality = TextureQuality.high;
            
    }

    /// <summary>
    /// Used to toggle on/off the VSync
    /// </summary>
    public void ToggleVSync()
    {
        isVsyncOn = !isVsyncOn;
    }

    /// <summary>
    /// Used to toggle on/off the Fullscreen boolean
    /// </summary>
    public void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
    }

    /// <summary>
    /// Used by the Resolution dropdown to change the resolution.
    /// </summary>
    public void ChangeResolution()
    {
        index = resDropdown.value;

        switch (index)
        {
            case 0:
                res = Resolution.NineteenTwenty;
                break;
            case 1:
                res = Resolution.ThirteenSixtySix;
                break;
            case 2:
                res = Resolution.TwelveEighty;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Used by the Texture Quality dropdown to change the resolution.
    /// </summary>
    public void ChangeTextureQuality()
    {
        index = tqualityDropdown.value;

        switch (index)
        {
            case 0:
                tQuality = TextureQuality.high;
                break;
            case 1:
                tQuality = TextureQuality.medium;
                break;
            case 2:
                tQuality = TextureQuality.low;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Used by the apply button to apply the changes made in the options menu.
    /// </summary>
    public void ApplySettings()
    {
        switch (tQuality)
        {
            case TextureQuality.low:
                QualitySettings.masterTextureLimit = lowTextureValue;
                break;
            case TextureQuality.medium:
                QualitySettings.masterTextureLimit = mediumTextureValue;
                break;
            case TextureQuality.high:
                QualitySettings.masterTextureLimit = highTextureValue;
                break;
            default:
                break;
        }

        switch (res)
        {
            case Resolution.NineteenTwenty:
                Screen.SetResolution(NineteenTwentyWidth, NineteenTwentyHeight, isFullscreen);
                break;
            case Resolution.ThirteenSixtySix:
                Screen.SetResolution(ThirteenSixtyWidth, ThirteenSixtyHeight, isFullscreen);
                break;
            case Resolution.TwelveEighty:
                Screen.SetResolution(TwelveEightyWidth, TwelveEightyHeight, isFullscreen);
                break;
            default:
                break;
        }

        if (isVsyncOn)
        {
            QualitySettings.vSyncCount = vsyncON;
        }
        else
        {
            QualitySettings.vSyncCount = vsyncOFF;
        }
    }
}

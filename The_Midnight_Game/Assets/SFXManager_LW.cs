using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager_LW : MonoBehaviour
{
    // Start is called before the first frame update
    public enum SFX {   Breathing, CandleBlow, ClockChiming, ClockTick, Death, Floorboards, Footstep, IceCracking,
                        MatchLighting, SaltPour, StoneOnStone, Thud1, Thud2, Whispers, Wind, WindowOpen };


    public List<AudioClip> ListOfAudioClips = new List<AudioClip>();

    private Dictionary<SFX, AudioClip> SFX_Dictionary = new Dictionary<SFX, AudioClip>();

    public GameObject SFXPrefab;

    void Start()
    {
        //Links all of the sfx clips to the corresponding enum value 
        SFX_Dictionary.Add(SFX.Breathing, ListOfAudioClips[0]);
        SFX_Dictionary.Add(SFX.CandleBlow, ListOfAudioClips[1]);
        SFX_Dictionary.Add(SFX.ClockChiming, ListOfAudioClips[2]);
        SFX_Dictionary.Add(SFX.ClockTick, ListOfAudioClips[3]);
        SFX_Dictionary.Add(SFX.Death, ListOfAudioClips[4]);
        SFX_Dictionary.Add(SFX.Floorboards, ListOfAudioClips[5]);
        SFX_Dictionary.Add(SFX.Footstep, ListOfAudioClips[6]);
        SFX_Dictionary.Add(SFX.IceCracking, ListOfAudioClips[7]);
        SFX_Dictionary.Add(SFX.MatchLighting, ListOfAudioClips[8]);
        SFX_Dictionary.Add(SFX.SaltPour, ListOfAudioClips[9]);
        SFX_Dictionary.Add(SFX.StoneOnStone, ListOfAudioClips[10]);
        SFX_Dictionary.Add(SFX.Thud1, ListOfAudioClips[11]);
        SFX_Dictionary.Add(SFX.Thud2, ListOfAudioClips[12]);
        SFX_Dictionary.Add(SFX.Whispers, ListOfAudioClips[13]);
        SFX_Dictionary.Add(SFX.Wind, ListOfAudioClips[14]);
        SFX_Dictionary.Add(SFX.WindowOpen, ListOfAudioClips[15]);
    }

    //Plays a sound effect when given a sfx from the sfx enum
    public void PlaySFX(SFX sfx)
    {
        //creates instance of sfx prefab and gets the audio source component
        AudioSource sfxAudio = Instantiate(SFXPrefab).GetComponent<AudioSource>();

        //Plays sfx once
        sfxAudio.PlayOneShot(SFX_Dictionary[sfx]);

        //Destroy instance and sfx has been played 
        Destroy(sfxAudio.gameObject, SFX_Dictionary[sfx].length);
    }
    
    //returns true if the SFX is not already being played --needs fixing--
    public bool CanPlaySound(SFX sfx)
    {
        GameObject[] audioSources = GameObject.FindGameObjectsWithTag("SFX");
        bool AlreadyPlaying = false;

        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].GetComponent<AudioSource>().clip == SFX_Dictionary[sfx])
            {
                AlreadyPlaying = true;
            }
        }
        if (AlreadyPlaying)
        {
            return false;
        } else
        {
            return true;
        }
    }
}

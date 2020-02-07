// Jack

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using InControl;

/// Handles save data for level 1.
/// 
/// Save data is retrieved from the player and the level then stored in a binary file.
public class Level1DataManager_Jack : MonoBehaviour
{
    public FirstPersonController_Jack playerScript;
    public GameObject playerPrefab;

    private static bool loadData = false;

    private void Start()
    {
        if (loadData)
        {
            playerScript = FindObjectOfType<FirstPersonController_Jack>();
            playerScript.LoadSaveData(Level1SaveData_Jack.current.playerSaveData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SaveGameData(); 
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadGameData();
        }
    } 

    /// Saves the player and level data for level 1.
    public void SaveGameData()
    {
        Level1SaveData_Jack.current = new Level1SaveData_Jack
        {
            playerSaveData = playerScript.GetSaveData()
        };
        SaveLoad_Jack.Save();
    }

    /// Loads the player and level data for level 1.
    public void LoadGameData()
    {
        loadData = true;
        SceneManager.LoadScene(1);
    }
}

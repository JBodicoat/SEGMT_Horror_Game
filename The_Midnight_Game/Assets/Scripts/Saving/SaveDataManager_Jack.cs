using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using InControl;

public class SaveDataManager_Jack : MonoBehaviour
{
    public FirstPersonController_Jack playerScript;
    public GameObject playerPrefab;

    private static bool loadData = false;

    private void Start()
    {
        if (loadData)
        {
            GameObject playerToDestory = FindObjectOfType<FirstPersonController_Jack>().gameObject;
            GameObject playerObject = Instantiate(playerPrefab);
            playerScript = playerObject.GetComponent<FirstPersonController_Jack>();
            playerScript.LoadSaveData(SaveData_Jack.current.playerSaveData);
            Destroy(playerToDestory);
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

    public void SaveGameData()
    {
        print("game saved");
        SaveData_Jack.current = new SaveData_Jack();
        SaveData_Jack.current.playerSaveData = playerScript.GetSaveData();
        SaveLoad_Jack.Save();
    }

    public void LoadGameData()
    {
        loadData = true;
        print("Game loaded");
        SceneManager.LoadScene(1);
        print("scene loaded");
    }
}

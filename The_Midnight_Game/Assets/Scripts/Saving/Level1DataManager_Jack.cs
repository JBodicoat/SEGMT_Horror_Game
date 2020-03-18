// Jack
// Jack 13/02/2020 - Added saving of tablet puzzle, player's rotation & candle
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using InControl;

/// <summary>
/// Handles save data for level 1.
/// Save data is retrieved from the player and the level then stored in a binary file.
/// </summary>
public class Level1DataManager_Jack : MonoBehaviour
{
    // Player Data
    public FirstPersonController_Jack playerScript;

    // Tablet Puzzle Data
    public GameObject tablet1;
    public GameObject tablet2;
    public GameObject tablet3;
    public TabletSlot_Jack tabletSlot1Script;
    public TabletSlot_Jack tabletSlot2Script;
    public TabletSlot_Jack tabletSlot3Script;
    public GameObject doorObject;
    public TabletDoor_Jack doorScript;

    private static bool loadData = false;

    private void Start()
    {
        if (loadData)
        {
            // Player Data
            playerScript = FindObjectOfType<FirstPersonController_Jack>();
            playerScript.LoadSaveData(Level1SaveData_Jack.current.playerSaveData);

            // ===== Tablet Puzzle Data ===== //
            // Tablet Data
            tablet1.transform.position = new Vector3(Level1SaveData_Jack.current.tablet1xPos,
                                                     Level1SaveData_Jack.current.tablet1yPos,
                                                     Level1SaveData_Jack.current.tablet1zPos);
            tablet1.transform.rotation = Quaternion.Euler(Level1SaveData_Jack.current.tablet1xRot,
                                                          Level1SaveData_Jack.current.tablet1yRot,
                                                          Level1SaveData_Jack.current.tablet1zRot);
            tablet1.layer = Level1SaveData_Jack.current.tablet1Layer;

            tablet2.transform.position = new Vector3(Level1SaveData_Jack.current.tablet2xPos,
                                                     Level1SaveData_Jack.current.tablet2yPos,
                                                     Level1SaveData_Jack.current.tablet2zPos);
            tablet2.transform.rotation = Quaternion.Euler(Level1SaveData_Jack.current.tablet2xRot,
                                                          Level1SaveData_Jack.current.tablet2yRot,
                                                          Level1SaveData_Jack.current.tablet2zRot);
            tablet2.layer = Level1SaveData_Jack.current.tablet2Layer;

            tablet3.transform.position = new Vector3(Level1SaveData_Jack.current.tablet3xPos,
                                         Level1SaveData_Jack.current.tablet3yPos,
                                         Level1SaveData_Jack.current.tablet3zPos);
            tablet3.transform.rotation = Quaternion.Euler(Level1SaveData_Jack.current.tablet3xRot,
                                                          Level1SaveData_Jack.current.tablet3yRot,
                                                          Level1SaveData_Jack.current.tablet3zRot);
            tablet3.layer = Level1SaveData_Jack.current.tablet3Layer;

            // Door Data
            doorObject.transform.position = new Vector3(Level1SaveData_Jack.current.doorXPos,
                                                          Level1SaveData_Jack.current.doorYPos,
                                                          Level1SaveData_Jack.current.doorZPos);
            doorScript.SetOpen(Level1SaveData_Jack.current.doorOpen);

            // Tablet Slot Data
            tabletSlot1Script.SetHoldingTablet(Level1SaveData_Jack.current.tabletSlot1HoldingTablet);  
            tabletSlot1Script.SetHeldTablet(GameObject.Find(Level1SaveData_Jack.current.tabletSlot1HeldTabletName));
            tabletSlot1Script.SetOrientation(Level1SaveData_Jack.current.tabletSlot1TabletOrientation);

            tabletSlot2Script.SetHoldingTablet(Level1SaveData_Jack.current.tabletSlot2HoldingTablet);
            tabletSlot2Script.SetHeldTablet(GameObject.Find(Level1SaveData_Jack.current.tabletSlot2HeldTabletName));
            tabletSlot2Script.SetOrientation(Level1SaveData_Jack.current.tabletSlot2TabletOrientation);

            tabletSlot3Script.SetHoldingTablet(Level1SaveData_Jack.current.tabletSlot3HoldingTablet);
            tabletSlot3Script.SetHeldTablet(GameObject.Find(Level1SaveData_Jack.current.tabletSlot3HeldTabletName));
            tabletSlot3Script.SetOrientation(Level1SaveData_Jack.current.tabletSlot3TabletOrientation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SaveGameData(); 
        }

        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            LoadGameData();
        }
    } 

    /// Saves the player and level data for level 1.
    public void SaveGameData()
    {
        Level1SaveData_Jack.current = new Level1SaveData_Jack();

        // Player Data
        Level1SaveData_Jack.current.playerSaveData = playerScript.GetSaveData();

        // ===== Tablet Puzzle Data ===== //
        // Tablet Data
        Level1SaveData_Jack.current.tablet1xPos = tablet1.transform.position.x;
        Level1SaveData_Jack.current.tablet1yPos = tablet1.transform.position.y;
        Level1SaveData_Jack.current.tablet1zPos = tablet1.transform.position.z;
        Level1SaveData_Jack.current.tablet1xRot = tablet1.transform.eulerAngles.x;
        Level1SaveData_Jack.current.tablet1yRot = tablet1.transform.eulerAngles.y;
        Level1SaveData_Jack.current.tablet1zRot = tablet1.transform.eulerAngles.z;
        Level1SaveData_Jack.current.tablet1Layer = tablet1.layer;

        

        Level1SaveData_Jack.current.tablet2xPos = tablet2.transform.position.x;
        Level1SaveData_Jack.current.tablet2yPos = tablet2.transform.position.y;
        Level1SaveData_Jack.current.tablet2zPos = tablet2.transform.position.z;
        Level1SaveData_Jack.current.tablet2xRot = tablet2.transform.eulerAngles.x;
        Level1SaveData_Jack.current.tablet2yRot = tablet2.transform.eulerAngles.y;
        Level1SaveData_Jack.current.tablet2zRot = tablet2.transform.eulerAngles.z;
        Level1SaveData_Jack.current.tablet2Layer = tablet2.layer;

        Level1SaveData_Jack.current.tablet3xPos = tablet3.transform.position.x;
        Level1SaveData_Jack.current.tablet3yPos = tablet3.transform.position.y;
        Level1SaveData_Jack.current.tablet3zPos = tablet3.transform.position.z;
        Level1SaveData_Jack.current.tablet3xRot = tablet3.transform.eulerAngles.x;
        Level1SaveData_Jack.current.tablet3yRot = tablet3.transform.eulerAngles.y;
        Level1SaveData_Jack.current.tablet3zRot = tablet3.transform.eulerAngles.z;
        Level1SaveData_Jack.current.tablet3Layer = tablet3.layer;

        // Door Data
        Level1SaveData_Jack.current.doorXPos = doorObject.transform.position.x;
        Level1SaveData_Jack.current.doorYPos = doorObject.transform.position.y;
        Level1SaveData_Jack.current.doorZPos = doorObject.transform.position.z;
        Level1SaveData_Jack.current.doorOpen = doorScript.IsOpen();

        // Tablet Slot Data
        Level1SaveData_Jack.current.tabletSlot1HoldingTablet = tabletSlot1Script.IsHoldingTablet();
        GameObject heldTablet1 = tabletSlot1Script.GetHeldTablet();
        if(heldTablet1)
        {
            Level1SaveData_Jack.current.tabletSlot1HeldTabletName = heldTablet1.name;
        }
        else
        {
            Level1SaveData_Jack.current.tabletSlot1HeldTabletName = "";
        }         
        Level1SaveData_Jack.current.tabletSlot1TabletOrientation = tabletSlot1Script.GetOrientation();

        Level1SaveData_Jack.current.tabletSlot2HoldingTablet = tabletSlot2Script.IsHoldingTablet();
        GameObject heldTablet2 = tabletSlot2Script.GetHeldTablet();
        if (heldTablet2)
        {
            Level1SaveData_Jack.current.tabletSlot2HeldTabletName = heldTablet2.name;
        }
        else
        {
            Level1SaveData_Jack.current.tabletSlot2HeldTabletName = "";
        }
        Level1SaveData_Jack.current.tabletSlot2TabletOrientation = tabletSlot2Script.GetOrientation();

        Level1SaveData_Jack.current.tabletSlot3HoldingTablet = tabletSlot3Script.IsHoldingTablet();
        GameObject heldTablet3 = tabletSlot3Script.GetHeldTablet();
        if (heldTablet3)
        {
            Level1SaveData_Jack.current.tabletSlot3HeldTabletName = heldTablet3.name;
        }
        else
        {
            Level1SaveData_Jack.current.tabletSlot3HeldTabletName = "";
        }
        Level1SaveData_Jack.current.tabletSlot3TabletOrientation = tabletSlot3Script.GetOrientation();

        SaveLoad_Jack.Save();
    }

    /// Loads the player and level data for level 1.
    public void LoadGameData()
    {
        loadData = true;
        SceneManager.LoadScene(1);
    }
}

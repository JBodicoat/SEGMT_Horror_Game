// Jack
// Jack 13/02/2020 - Added saving of tablet puzzle, player's rotation & candle
// Jack 23/03/2020 - Added saving of piano puzzle, library puzzle, attic puzzle, valve puzzle, burning puzzle & lantern puzzle.
//                   Removed door from stone tablets puzzle.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Handles save data for level 1.
/// Save data is retrieved from the player and the level then stored in a binary file.
/// </summary>
public class LevelDataManager_Jack : MonoBehaviour
{
    private static bool loadData = false;

    // Player Data
    public FirstPersonController_Jack playerScript;

    // Tablet Puzzle Data
    public GameObject tablet1;
    public GameObject tablet2;
    public GameObject tablet3;
    public TabletSlot_Jack tabletSlot1Script;
    public TabletSlot_Jack tabletSlot2Script;
    public TabletSlot_Jack tabletSlot3Script;

    // Piano Puzzle
    public KeyUIManager_Jack pianoKeyScript;

    // Clock Puzzle

    // Library Puzzle
    public BookController_Jack bookControllerScript;

    public GameObject book1Mesh;
    public Book_Jack book1Script;

    public GameObject book2Mesh;
    public Book_Jack book2Script;

    public GameObject book3Mesh;
    public Book_Jack book3Script;

    // Rabbit Puzzle

    // Safe Puzzle

    // Attic Puzzle
    public AtticHatch_Jack atticHatchScript;
    public LadderAnimation_Jack ladderScript;

    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;

    // Valve Puzzle
    public ValveController_Jack valveControllerScript;

    public ValvePuzzleDoor_Jack valvePuzzleDoorScript;

    public Valve_Jack valve1Script;
    public Valve_Jack valve2Script;
    public Valve_Jack valve3Script;
    public Valve_Jack valve4Script;
    public Valve_Jack valve5Script;

    // Burning Puzzle
    public GameObject woodenPanel;
    
    public GameObject bottle1;
    public GameObject bottle2;
    public GameObject bottle3;
    public GameObject bottle4;
    public GameObject bottle5;
    public GameObject bottle6;

    public BottlePlacementManager_Jack bottlePlacementManager;

    // Lantern Puzzle
    public LanternSlot_Jack lanternSlotScript;

    public GameObject lantern;

    private void Start()
    {
        if (loadData)
        {
            // Player Data
            playerScript = FindObjectOfType<FirstPersonController_Jack>();
            playerScript.LoadSaveData(LevelSaveData_Jack.current.playerSaveData);

            // ===== Tablet Puzzle Data ===== //
            // Tablet Data
            tablet1.transform.position = new Vector3(LevelSaveData_Jack.current.tablet1xPos,
                                                     LevelSaveData_Jack.current.tablet1yPos,
                                                     LevelSaveData_Jack.current.tablet1zPos);
            tablet1.transform.rotation = Quaternion.Euler(LevelSaveData_Jack.current.tablet1xRot,
                                                          LevelSaveData_Jack.current.tablet1yRot,
                                                          LevelSaveData_Jack.current.tablet1zRot);
            tablet1.layer = LevelSaveData_Jack.current.tablet1Layer;

            tablet2.transform.position = new Vector3(LevelSaveData_Jack.current.tablet2xPos,
                                                     LevelSaveData_Jack.current.tablet2yPos,
                                                     LevelSaveData_Jack.current.tablet2zPos);
            tablet2.transform.rotation = Quaternion.Euler(LevelSaveData_Jack.current.tablet2xRot,
                                                          LevelSaveData_Jack.current.tablet2yRot,
                                                          LevelSaveData_Jack.current.tablet2zRot);
            tablet2.layer = LevelSaveData_Jack.current.tablet2Layer;

            tablet3.transform.position = new Vector3(LevelSaveData_Jack.current.tablet3xPos,
                                         LevelSaveData_Jack.current.tablet3yPos,
                                         LevelSaveData_Jack.current.tablet3zPos);
            tablet3.transform.rotation = Quaternion.Euler(LevelSaveData_Jack.current.tablet3xRot,
                                                          LevelSaveData_Jack.current.tablet3yRot,
                                                          LevelSaveData_Jack.current.tablet3zRot);
            tablet3.layer = LevelSaveData_Jack.current.tablet3Layer;

            // Tablet Slot Data
            tabletSlot1Script.SetHoldingTablet(LevelSaveData_Jack.current.tabletSlot1HoldingTablet);  
            tabletSlot1Script.SetHeldTablet(GameObject.Find(LevelSaveData_Jack.current.tabletSlot1HeldTabletName));
            tabletSlot1Script.SetOrientation(LevelSaveData_Jack.current.tabletSlot1TabletOrientation);

            tabletSlot2Script.SetHoldingTablet(LevelSaveData_Jack.current.tabletSlot2HoldingTablet);
            tabletSlot2Script.SetHeldTablet(GameObject.Find(LevelSaveData_Jack.current.tabletSlot2HeldTabletName));
            tabletSlot2Script.SetOrientation(LevelSaveData_Jack.current.tabletSlot2TabletOrientation);

            tabletSlot3Script.SetHoldingTablet(LevelSaveData_Jack.current.tabletSlot3HoldingTablet);
            tabletSlot3Script.SetHeldTablet(GameObject.Find(LevelSaveData_Jack.current.tabletSlot3HeldTabletName));
            tabletSlot3Script.SetOrientation(LevelSaveData_Jack.current.tabletSlot3TabletOrientation);

            // ===== Piano Puzzle Data ===== //
            pianoKeyScript.SetPuzzleSolved(LevelSaveData_Jack.current.pianoPuzzleSolved);

            // ===== Library Puzzle Data ===== //
            bookControllerScript.SetPuzzleSolved(LevelSaveData_Jack.current.libraryPuzzleSolved);

            if(LevelSaveData_Jack.current.libraryPuzzleSolved)
            {
                book1Mesh.transform.localPosition = new Vector3(book1Mesh.transform.localPosition.x, 
                                                            book1Mesh.transform.localPosition.y, 
                                                            LevelSaveData_Jack.current.book1LocalZPos);
                book1Script.SetPulledOut(true);

                book2Mesh.transform.localPosition = new Vector3(book2Mesh.transform.localPosition.x, 
                                                            book2Mesh.transform.localPosition.y,
                                                            LevelSaveData_Jack.current.book2LocalZPos);
                book2Script.SetPulledOut(true);

                book3Mesh.transform.localPosition = new Vector3(book3Mesh.transform.localPosition.x, 
                                                            book3Mesh.transform.localPosition.y, 
                                                            LevelSaveData_Jack.current.book3LocalZPos);
                book3Script.SetPulledOut(true);
            }

            // ===== Attic Puzzle Data ===== //
            atticHatchScript.SetHatchOpen(LevelSaveData_Jack.current.atticPuzzleSolved);
            ladderScript.SetLadderDown(LevelSaveData_Jack.current.atticPuzzleSolved);

            ball1.transform.position = new Vector3(LevelSaveData_Jack.current.ball1XPos, 
                                                   LevelSaveData_Jack.current.ball1YPos, 
                                                   LevelSaveData_Jack.current.ball1ZPos);
            ball2.transform.position = new Vector3(LevelSaveData_Jack.current.ball2XPos, 
                                                   LevelSaveData_Jack.current.ball2YPos, 
                                                   LevelSaveData_Jack.current.ball2ZPos);
            ball3.transform.position = new Vector3(LevelSaveData_Jack.current.ball3XPos, 
                                                   LevelSaveData_Jack.current.ball3YPos, 
                                                   LevelSaveData_Jack.current.ball3ZPos);

            // ===== Valve Puzzle Data ===== //
            valveControllerScript.SetPuzzleSolved(LevelSaveData_Jack.current.valvePuzzleSolved);
            valvePuzzleDoorScript.SetDoorOpen(LevelSaveData_Jack.current.valvePuzzleSolved);

            valve1Script.SetLightOn(LevelSaveData_Jack.current.valve1LightOn);
            valve2Script.SetLightOn(LevelSaveData_Jack.current.valve2LightOn);
            valve3Script.SetLightOn(LevelSaveData_Jack.current.valve3LightOn);
            valve4Script.SetLightOn(LevelSaveData_Jack.current.valve4LightOn);
            valve5Script.SetLightOn(LevelSaveData_Jack.current.valve5LightOn);

            // ===== Burning Puzzle Data  ===== //
            if(LevelSaveData_Jack.current.bottle1PickedUp)
            {
                Destroy(bottle1);
            }
            if(LevelSaveData_Jack.current.bottle2PickedUp)
            {
                Destroy(bottle2);
            }
            if(LevelSaveData_Jack.current.bottle3PickedUp)
            {
                Destroy(bottle3);
            }
            if(LevelSaveData_Jack.current.bottle4PickedUp)
            {
                Destroy(bottle4);
            }
            if(LevelSaveData_Jack.current.bottle5PickedUp)
            {
                Destroy(bottle5);
            }
            if(LevelSaveData_Jack.current.bottle6PickedUp)
            {
                Destroy(bottle6);
            }

            if(LevelSaveData_Jack.current.woodenPanelDestroyed)
            {
                Destroy(woodenPanel);
            }
            else
            {
                bottlePlacementManager.SetPlacedBottles(LevelSaveData_Jack.current.bottlesPlaced);
            }

            // ===== Lantern Puzzle Data ===== //
            if(LevelSaveData_Jack.current.lanternPuzzleSolved)
            {
                lanternSlotScript.CompletePuzzle();
            }

            lantern.transform.position = new Vector3(LevelSaveData_Jack.current.lanternXPos, 
                                                     lantern.transform.position.y, 
                                                     LevelSaveData_Jack.current.lanternZPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            print("saved");
            SaveGameData();
        }

        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            print("loaded");
            LoadGameData();
        }
    } 

    /// Saves the player and level data for level 1.
    public void SaveGameData()
    {
        LevelSaveData_Jack.current = new LevelSaveData_Jack();

        // Player Data
        LevelSaveData_Jack.current.playerSaveData = playerScript.GetSaveData();

        // ===== Tablet Puzzle Data ===== //
        // Tablet Data
        LevelSaveData_Jack.current.tablet1xPos = tablet1.transform.position.x;
        LevelSaveData_Jack.current.tablet1yPos = tablet1.transform.position.y;
        LevelSaveData_Jack.current.tablet1zPos = tablet1.transform.position.z;
        LevelSaveData_Jack.current.tablet1xRot = tablet1.transform.eulerAngles.x;
        LevelSaveData_Jack.current.tablet1yRot = tablet1.transform.eulerAngles.y;
        LevelSaveData_Jack.current.tablet1zRot = tablet1.transform.eulerAngles.z;
        LevelSaveData_Jack.current.tablet1Layer = tablet1.layer;

        LevelSaveData_Jack.current.tablet2xPos = tablet2.transform.position.x;
        LevelSaveData_Jack.current.tablet2yPos = tablet2.transform.position.y;
        LevelSaveData_Jack.current.tablet2zPos = tablet2.transform.position.z;
        LevelSaveData_Jack.current.tablet2xRot = tablet2.transform.eulerAngles.x;
        LevelSaveData_Jack.current.tablet2yRot = tablet2.transform.eulerAngles.y;
        LevelSaveData_Jack.current.tablet2zRot = tablet2.transform.eulerAngles.z;
        LevelSaveData_Jack.current.tablet2Layer = tablet2.layer;

        LevelSaveData_Jack.current.tablet3xPos = tablet3.transform.position.x;
        LevelSaveData_Jack.current.tablet3yPos = tablet3.transform.position.y;
        LevelSaveData_Jack.current.tablet3zPos = tablet3.transform.position.z;
        LevelSaveData_Jack.current.tablet3xRot = tablet3.transform.eulerAngles.x;
        LevelSaveData_Jack.current.tablet3yRot = tablet3.transform.eulerAngles.y;
        LevelSaveData_Jack.current.tablet3zRot = tablet3.transform.eulerAngles.z;
        LevelSaveData_Jack.current.tablet3Layer = tablet3.layer;

        // Tablet Slot Data
        LevelSaveData_Jack.current.tabletSlot1HoldingTablet = tabletSlot1Script.IsHoldingTablet();
        GameObject heldTablet1 = tabletSlot1Script.GetHeldTablet();
        if(heldTablet1)
        {
            LevelSaveData_Jack.current.tabletSlot1HeldTabletName = heldTablet1.name;
        }
        else
        {
            LevelSaveData_Jack.current.tabletSlot1HeldTabletName = "";
        }         
        LevelSaveData_Jack.current.tabletSlot1TabletOrientation = tabletSlot1Script.GetOrientation();

        LevelSaveData_Jack.current.tabletSlot2HoldingTablet = tabletSlot2Script.IsHoldingTablet();
        GameObject heldTablet2 = tabletSlot2Script.GetHeldTablet();
        if (heldTablet2)
        {
            LevelSaveData_Jack.current.tabletSlot2HeldTabletName = heldTablet2.name;
        }
        else
        {
            LevelSaveData_Jack.current.tabletSlot2HeldTabletName = "";
        }
        LevelSaveData_Jack.current.tabletSlot2TabletOrientation = tabletSlot2Script.GetOrientation();

        LevelSaveData_Jack.current.tabletSlot3HoldingTablet = tabletSlot3Script.IsHoldingTablet();
        GameObject heldTablet3 = tabletSlot3Script.GetHeldTablet();
        if (heldTablet3)
        {
            LevelSaveData_Jack.current.tabletSlot3HeldTabletName = heldTablet3.name;
        }
        else
        {
            LevelSaveData_Jack.current.tabletSlot3HeldTabletName = "";
        }
        LevelSaveData_Jack.current.tabletSlot3TabletOrientation = tabletSlot3Script.GetOrientation();

        // ===== Piano Puzzle Data ===== //    
        LevelSaveData_Jack.current.pianoPuzzleSolved = pianoKeyScript.GetPuzzleSolved();

        // ===== Library Puzzle Data ===== //
        LevelSaveData_Jack.current.libraryPuzzleSolved = bookControllerScript.GetPuzzleSolved();
        LevelSaveData_Jack.current.book1LocalZPos = book1Mesh.transform.localPosition.z;
        LevelSaveData_Jack.current.book2LocalZPos = book2Mesh.transform.localPosition.z;
        LevelSaveData_Jack.current.book3LocalZPos = book3Mesh.transform.localPosition.z;

        // ===== Attic Puzzle Data ===== //
        LevelSaveData_Jack.current.atticPuzzleSolved = atticHatchScript.IsHatchOpen();

        LevelSaveData_Jack.current.ball1XPos = ball1.transform.position.x;
        LevelSaveData_Jack.current.ball1YPos = ball1.transform.position.y;
        LevelSaveData_Jack.current.ball1ZPos = ball1.transform.position.z;

        LevelSaveData_Jack.current.ball2XPos = ball2.transform.position.x;
        LevelSaveData_Jack.current.ball2YPos = ball2.transform.position.y;
        LevelSaveData_Jack.current.ball2ZPos = ball2.transform.position.z;

        LevelSaveData_Jack.current.ball3XPos = ball3.transform.position.x;
        LevelSaveData_Jack.current.ball3YPos = ball3.transform.position.y;
        LevelSaveData_Jack.current.ball3ZPos = ball3.transform.position.z;

        // ===== Valve Puzzle Data ===== //
        LevelSaveData_Jack.current.valvePuzzleSolved = valveControllerScript.GetPuzzleSolved();

        LevelSaveData_Jack.current.valve1LightOn = valve1Script.IsLightOn();
        LevelSaveData_Jack.current.valve2LightOn = valve2Script.IsLightOn();
        LevelSaveData_Jack.current.valve3LightOn = valve3Script.IsLightOn();
        LevelSaveData_Jack.current.valve4LightOn = valve4Script.IsLightOn();
        LevelSaveData_Jack.current.valve5LightOn = valve5Script.IsLightOn();

        // ===== Burning Puzzle Data  ===== //
        LevelSaveData_Jack.current.woodenPanelDestroyed = woodenPanel;

        LevelSaveData_Jack.current.bottlesPlaced = bottlePlacementManager.GetNumPlacedBottles();

        LevelSaveData_Jack.current.bottle1PickedUp = bottle1;
        LevelSaveData_Jack.current.bottle2PickedUp = bottle2;
        LevelSaveData_Jack.current.bottle3PickedUp = bottle3;
        LevelSaveData_Jack.current.bottle4PickedUp = bottle4;
        LevelSaveData_Jack.current.bottle5PickedUp = bottle5;
        LevelSaveData_Jack.current.bottle6PickedUp = bottle6;

        // ===== Lantern Puzzle Data ===== //
        LevelSaveData_Jack.current.lanternPuzzleSolved = lanternSlotScript;

        LevelSaveData_Jack.current.lanternXPos = lantern.transform.position.x;
        LevelSaveData_Jack.current.lanternZPos = lantern.transform.position.z;

        SaveLoad_Jack.Save();
    }

    /// Loads the player and level data for level 1.
    public void LoadGameData()
    {
        loadData = true;
        SceneManager.LoadScene(1);
    }
}

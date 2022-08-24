using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomNavigationButtons : MonoBehaviour
{
    public GameObject characterRoot;
    public GameObject inventoryRoot;
    public GameObject materialsRoot;

    public GameEvent refreshMaterialsUI;
    public GameEvent refreshInventoryUI;
    public GameEvent refreshEquipmentUI;
    

    public void OpenCharacter()
    {
        PlayUiSound();
        if (characterRoot.activeSelf)
            characterRoot.SetActive(false);
        else
        {
            CloseAll();
            characterRoot.SetActive(true);
        }
    }
    public void OpenInventory()
    {
        PlayUiSound();
        if (inventoryRoot.activeSelf)
            inventoryRoot.SetActive(false);
        else
        {
            CloseAll();
            inventoryRoot.SetActive(true);
            refreshInventoryUI.Raise();
            refreshEquipmentUI.Raise();
        }
    }
    public void OpenMaterials()
    {
        PlayUiSound();
        if (materialsRoot.activeSelf)
            materialsRoot.SetActive(false);
        else
        {
            CloseAll();
            materialsRoot.SetActive(true);
            refreshMaterialsUI.Raise();
        }
    }

    private void CloseAll()
    {
        if (characterRoot!=null)
            characterRoot.SetActive(false);
        if (inventoryRoot != null)
            inventoryRoot.SetActive(false);
        if (materialsRoot != null)
            materialsRoot.SetActive(false);
    }

    private void PlayUiSound()
    {
        AudioManager.instance.Play("UIOpenedClosed");
    }
}

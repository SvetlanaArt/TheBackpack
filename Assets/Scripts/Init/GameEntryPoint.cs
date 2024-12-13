using BackpackUnit.Backpack;
using BackpackUnit.UI;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] InventiryUI inventiryUI;
    [SerializeField] BackpackInputHandler backpackInputHandler;
    [SerializeField] BackpackInventory backpackInventory;
    

    void Awake()
    {
        inventiryUI.Init();
        backpackInventory.Init(inventiryUI);
        backpackInputHandler.Init(inventiryUI);
    }

}

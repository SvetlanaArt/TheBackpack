using BackpackUnit.UI;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] InventiryUI inventiryUI;
    [SerializeField] BackpackInputHandler backpackInputHandler;
    

    void Awake()
    {
        inventiryUI.Init();
        backpackInputHandler.Init(inventiryUI);
    }

}

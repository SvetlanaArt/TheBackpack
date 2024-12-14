using BackpackUnit.Backpack;
using BackpackUnit.Server;
using BackpackUnit.UI;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [Header("Server connection")]
    [SerializeField] string url;
    [SerializeField] string token;
    [Header("Services")]
    [SerializeField] InventiryUI inventiryUI;
    [SerializeField] BackpackInputHandler backpackInputHandler;
    [SerializeField] BackpackInventory backpackInventory;

    PostRequest postRequest;

    void Awake()
    {
        postRequest = new PostRequest(url, token);
        inventiryUI.Init();
        backpackInventory.Init(inventiryUI, postRequest);
        backpackInputHandler.Init(inventiryUI);
    }

}

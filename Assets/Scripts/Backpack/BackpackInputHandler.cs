using BackpackUnit.Core;
using UnityEngine;
using UnityEngine.EventSystems;


public class BackpackInputHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private IInventoryView inventoryView;


    public void Init(IInventoryView inventoryView)
    {
        this.inventoryView = inventoryView;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        inventoryView.Open();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inventoryView.TryRemoveItemAndClose(eventData);
    }
}

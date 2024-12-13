using UnityEngine;
using UnityEngine.EventSystems;


public class Backpack : MonoBehaviour, IPointerDownHandler 
{
    [SerializeField] GameObject InventoryUI;

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryUI.SetActive(true);
    }

}

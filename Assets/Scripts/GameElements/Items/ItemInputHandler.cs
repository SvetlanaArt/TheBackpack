using UnityEngine;
using UnityEngine.EventSystems;

namespace BackpackUnit.Items
{
    [RequireComponent(typeof(Item))]
    public class ItemInputHandler: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("DragDrop")]
        [SerializeField] float pickupDistance;
        [SerializeField] float movingSpeed;

        DragDrop dragDrop;
        Item item;

        bool isPickedUp = false;

        private void Start()
        {
            item = GetComponent<Item>();
            Rigidbody rigidbodyObj = GetComponent<Rigidbody>();

            dragDrop = new DragDrop(transform, pickupDistance, rigidbodyObj);

            isPickedUp = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Input.GetMouseButton(0))
                return;
            if (isPickedUp)
                return;

            item.Catch();

            isPickedUp = true;
            dragDrop.PickUp();
            dragDrop.StartMovingAsync(movingSpeed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
                return;

            dragDrop.StopMoving();
            isPickedUp = false;

            if (!item.TryPutToBackpack(eventData))
            {
                item.Drop();
            }
        }

        private void OnDestroy()
        {
            dragDrop?.Dispose();
        }

    }
}



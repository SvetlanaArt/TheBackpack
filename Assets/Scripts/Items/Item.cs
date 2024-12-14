using System;
using BackpackUnit.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BackpackUnit.Items
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IThrowable
    {
        [SerializeField] ItemData itemData;
        [Header("DragDrop")]
        [SerializeField] float pickupDistance;
        [SerializeField] float movingSpeed;
        [Header("Animation")]
        [SerializeField] float putIntoBackpackSpeed;
        [Header("Physics")]
        [SerializeField] float throwForce;

        DragDrop dragDrop;
        ItemAnimation itemAnimation;
        ItemPhysics itemPhysics;
        Camera mainCamera;

        Transform itemsParent;
        bool isPickedUp;

        string id;

        private void Start()
        {
            id = id = Guid.NewGuid().ToString();

            mainCamera = Camera.main;

            Rigidbody rigidbodyObj = GetComponent<Rigidbody>();
            Collider colliderObj = GetComponent<Collider>();

            dragDrop = new DragDrop(transform, pickupDistance, rigidbodyObj);
            itemAnimation = new ItemAnimation(transform);
            itemPhysics = new ItemPhysics(rigidbodyObj, colliderObj);

            itemsParent = transform.parent;
            isPickedUp = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Input.GetMouseButton(0))
                return;
            if (isPickedUp)
                return;
           
            itemPhysics.EnablePhysics(false);

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

            if (!TryPutToBackpack(eventData))
            {
                itemPhysics.EnablePhysics(true);
            }
        }

        private bool TryPutToBackpack(PointerEventData eventData)
        {
            if (TryToFindBackpack(eventData, out ICollectItems backpack))
            {
                Vector3 insertionLocalPosition = backpack.AddItem(transform, this, itemData);
                itemAnimation.PutIntoRun(putIntoBackpackSpeed, insertionLocalPosition);
                itemPhysics.SetAvailable(false);
                return true;
            }
            return false;
        }

        private bool TryToFindBackpack(PointerEventData eventData, out ICollectItems backpack)
        {
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);
            RaycastHit[] hits = Physics.RaycastAll(ray, 50);
            backpack = null;
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out backpack))
                {
                    return true;
                }
            }
            return false;
        }

        public void ThrowAway(Vector3 position, Vector3 removalPosition)
        {
            ThrowAwayAsync(position, removalPosition);
        }

        private async void ThrowAwayAsync(Vector3 position, Vector3 removalPosition)
        {
            await itemAnimation.ThrowRun(position, putIntoBackpackSpeed, removalPosition);
            transform.parent = itemsParent;
            itemPhysics.SetAvailable(true);
            itemPhysics.EnablePhysics(true);
            itemPhysics.ThrowWithForce(throwForce);
        }

        private void OnDestroy()
        {
            dragDrop?.Dispose();
        }

        public string GetId()
        {
            return id;
        }
    }
}



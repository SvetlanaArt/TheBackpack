using System;
using BackpackUnit.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BackpackUnit.Items
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IThrowable
    {
        [SerializeField] ItemData itemData;
        [Header("Animation")]
        [SerializeField] float putIntoBackpackSpeed;
        [Header("Physics")]
        [SerializeField] float throwForce;

        ItemAnimation itemAnimation;
        ItemPhysics itemPhysics;

        Camera mainCamera;
        Transform itemsParent;
        string id;

        private void Start()
        {
            id = id = Guid.NewGuid().ToString();

            mainCamera = Camera.main;

            Rigidbody rigidbodyObj = GetComponent<Rigidbody>();
            Collider colliderObj = GetComponent<Collider>();

            itemAnimation = new ItemAnimation(transform);
            itemPhysics = new ItemPhysics(rigidbodyObj, colliderObj);

            itemsParent = transform.parent;
        }

        public bool TryPutToBackpack(PointerEventData eventData)
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
            Drop();
            itemPhysics.ThrowWithForce(throwForce);
        }

        public void Catch()
        {
            itemPhysics.EnablePhysics(false);
        }

        public void Drop()
        {
            itemPhysics.EnablePhysics(true);
        }

        public string GetId()
        {
            return id;
        }
    }
}



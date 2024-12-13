
using BackpackUnit.Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BackpackUnit.Items
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] ItemData itemData;
        [SerializeField] float pickupDistance;
        [SerializeField] float pickupSpeed;
        [SerializeField] float insertionDistance;

        DragDrop dragDrop;
        Rigidbody rigidbodyObj;
        Collider colliderObj;
        Camera mainCamera;
        
        bool isPickedUp;

        private void Start()
        {
            mainCamera = Camera.main;
            rigidbodyObj = GetComponent<Rigidbody>();
            colliderObj = GetComponent<Collider>();
            dragDrop = new DragDrop(transform, pickupDistance, rigidbodyObj.worldCenterOfMass);

            isPickedUp = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isPickedUp)
                return;

            EnablePhysics(false);

            dragDrop.PickUp();
            
            dragDrop.StartMovingAsync(pickupSpeed);
        }

        private void EnablePhysics(bool isEnabled)
        {
            rigidbodyObj.useGravity = isEnabled;
            rigidbodyObj.isKinematic = !isEnabled;
            colliderObj.isTrigger = !isEnabled;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Input.GetMouseButton(0))
                return;

            dragDrop.StopMoving();

            if (!TryPutToBackpack(eventData))
            {
                EnablePhysics(true);
            }
        }

        private bool TryPutToBackpack(PointerEventData eventData)
        {
            Ray ray = mainCamera.ScreenPointToRay(eventData.position);
            RaycastHit[] hits = Physics.RaycastAll(ray, 50);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out ICollectItems backpack))
                {
                    backpack.AddItem(transform, itemData);
                    Sequence backpackSequence = DOTween.Sequence();
                    backpackSequence.Append(transform.DOLocalRotate(Vector3.zero, 0.3f))
                                    .Append(transform.DOLocalMove(new Vector3(0, insertionDistance, 0), 0.3f)) 
                                    .Append(transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.InQuad));
                    SetAvailable(false);
                    return true;
                }
            }
            return false;
        }

        private void SetAvailable(bool isAvailable)
        {
            colliderObj.enabled = isAvailable;
        }

        private void OnDestroy()
        {
           dragDrop?.Dispose();
        }

    }

}



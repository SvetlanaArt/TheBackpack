using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace BackpackUnit.Items
{
    public class DragDrop :IDisposable
    {
        const float SPEED_RATIO = 10f;

        Transform transform;
        Camera mainCamera;

        CancellationTokenSource source;

        Vector3 positionCorrection;
        Rigidbody rigidbody;
        Plane dragPlane;

        public DragDrop(Transform positioningObject, float dragDistance, Rigidbody rigidbody)
        {
            dragPlane = new Plane(Vector3.up, new Vector3(0, dragDistance, 0));
            mainCamera = Camera.main;
            transform = positioningObject;
            this.rigidbody = rigidbody;
        }

        public void PickUp()
        {
            positionCorrection = transform.position - rigidbody.worldCenterOfMass;
            transform.position = GetPositionOnDragPlane() ;
        }

        public async void StartMovingAsync(float speed)
        {
            source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            try
            {
                while (true)
                {
                    Move(speed);
                    await UniTask.Yield(PlayerLoopTiming.Update, token);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Stop dragging");
            }
        }

        private void Move(float speed)
        {
            Vector3 targetPosition = GetPositionOnDragPlane();

            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime * SPEED_RATIO);
        }

        private Vector3 GetPositionOnDragPlane()
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); 

            if (dragPlane.Raycast(ray, out float enter))
            {
                return ray.GetPoint(enter) + positionCorrection; 
            }

            return transform.position;
        }

        public void StopMoving()
        {
            source?.Cancel();
        }

        public void Dispose()
        {
            source?.Cancel();
            source?.Dispose();
        }
    }
}

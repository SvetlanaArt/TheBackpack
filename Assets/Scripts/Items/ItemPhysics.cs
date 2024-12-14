using UnityEngine;

namespace BackpackUnit.Items
{
    public class ItemPhysics
    {
        Rigidbody rigidbody;
        Collider collider;

        public ItemPhysics(Rigidbody rigidbody, Collider collider)
        {
            this.rigidbody = rigidbody;
            this.collider = collider;
        }

        public void EnablePhysics(bool isEnabled)
        {
            rigidbody.useGravity = isEnabled;
            rigidbody.isKinematic = !isEnabled;
            collider.isTrigger = !isEnabled;
        }

        public void SetAvailable(bool isAvailable)
        {
            collider.enabled = isAvailable;
        }

        public void ThrowWithForce(float force)
        {
            Vector3 throwDirection = new Vector3(-1, 0, 1).normalized;
            rigidbody.AddForce(throwDirection * force, ForceMode.Impulse);
        }

    }
}



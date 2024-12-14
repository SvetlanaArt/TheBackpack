using UnityEngine;

namespace BackpackUnit.Core
{
    public interface IThrowable
    {
        public string GetId();

        public void ThrowAway(Vector3 position);
    }
}
using System;
using BackpackUnit.Core;
using UnityEngine;

namespace BackpackUnit.Core
{
    public interface ICollectItems
    {
        public Vector3 AddItem(Transform itemTransform, IThrowable item, IItemInfo itemInfo);
    }
}
using BackpackUnit.Core;
using UnityEngine;
using UnityEngine.UI;

namespace BackpackUnit.UI
{
    public class PocketUI: MonoBehaviour
    {
        [SerializeField] ItemType type;
        [SerializeField] Image image;

        public ItemType GetItemType()
        {
            return type;
        }

        public void SetImage(Sprite sprite)
        {
            image.enabled = true;
            image.sprite = sprite;
            image.type = Image.Type.Simple;
            image.preserveAspect = true;
        }

        public void RemoveImage()
        {
            image.enabled = false;
            image.sprite = null;
        }   
    }
}


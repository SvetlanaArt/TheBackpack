using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace BackpackUnit.Items
{
    public class ItemAnimation
    {
        Transform transform;

        public ItemAnimation(Transform transform)
        {
            this.transform = transform;
        }

        public void PutIntoRun(float speed, Vector3 insertiomLocalPosition)
        {
            Sequence putToBackpackSequence = DOTween.Sequence();
            putToBackpackSequence.Append(transform.DOLocalRotate(Vector3.zero, speed))
                                 .Append(transform.DOLocalMove(insertiomLocalPosition, speed))
                                 .Append(transform.DOLocalMove(Vector3.zero, speed).SetEase(Ease.InQuad));
        }

        public async Task ThrowRun(Vector3 position, float speed, Vector3 removalLocalPosition)
        {
            Sequence takeOutSequence = DOTween.Sequence();

            takeOutSequence.Append(transform.DOLocalMove(removalLocalPosition, speed))
                           .Append(transform.DOMove(position, speed).SetEase(Ease.InQuad));
            await takeOutSequence.AsyncWaitForCompletion();
        }

    }
}



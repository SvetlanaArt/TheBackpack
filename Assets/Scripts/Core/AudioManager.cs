using UnityEngine;

namespace BackpackUnit.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField][Range(0f, 1f)] float volume = 1f;

        public void PlaySound(AudioClip sound)
        {
            PlaySound(sound, volume);
        }

        void PlaySound(AudioClip sound, float volume)
        {
            if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound,
                                            Camera.main.transform.position,
                                            volume);
            }
        }
    }

}


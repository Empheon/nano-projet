using UnityEngine;

namespace Utils
{
    public class PlayAkEvent : MonoBehaviour
    {
        [SerializeField] private AK.Wwise.Event akEvent;

        public void Play()
        {
            akEvent.Post(gameObject);
        }

        public void Stop()
        {
            akEvent.Stop(gameObject);
        }
    }
}
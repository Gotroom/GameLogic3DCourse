using UnityEngine;

namespace Ermolaev_3D
{
    public abstract class BaseMenu : MonoBehaviour
    {
        protected bool IsShow { get; set; }
        protected Interface Interface;
        protected virtual void Awake()
        {
            Interface = FindObjectOfType<Interface>();
        }

        public abstract void Hide();
        public abstract void Show();
    }
}
using UnityEngine;

namespace Ermolaev_3D
{
    public class GrabPoint : MonoBehaviour
    {
        [HideInInspector] public bool IsVisible => _parentRenderer.enabled;

        public bool IsLeft = false;

        private Renderer _parentRenderer;

        private void Start()
        {
            _parentRenderer = gameObject.GetComponentInParent<Renderer>();
        }
    }
}
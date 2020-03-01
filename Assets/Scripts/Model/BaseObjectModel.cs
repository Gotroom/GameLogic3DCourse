using UnityEngine;

namespace Ermolaev_3D
{
    public class BaseObjectModel : MonoBehaviour
    {
        private int _layer;
        private bool _isVisible;
        private Color _color;

        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                AskLayer(Transform, _layer);
            }
        }

        public bool Visible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                AskVisible(transform, value);
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                AskColor(transform, value);
            }
        }

        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        private void AskVisible(Transform obj, bool value)
        {
            if (obj.gameObject.TryGetComponent<Renderer>(out var component))
            {
                component.enabled = value;
            }
            if (obj.childCount <= 0)
            {
                return;
            }
            foreach (Transform transform in obj)
            {
                AskVisible(transform, value);
            }
        }

        private void AskColor(Transform obj, Color value)
        {
            if (obj.gameObject.TryGetComponent<Renderer>(out var component))
            {
                foreach (var material in component.materials)
                {
                    material.color = value;
                }
            }
            if (obj.childCount <= 0)
            {
                return;
            }
            foreach (Transform transform in obj)
            {
                AskColor(transform, value);
            }
        }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }
    }
}


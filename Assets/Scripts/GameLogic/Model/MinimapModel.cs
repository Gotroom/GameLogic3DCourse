using UnityEngine;
using System.Collections;

namespace Ermolaev_3D
{
    public class MinimapModel : BaseObjectModel
    {
        private const float Y_AXIS_OFFSET = 10.0f;
        private Transform _player;

        private void Start()
        {
            _player = Camera.main.transform;
            transform.parent = null;

            var rt = Resources.Load<RenderTexture>("Minimap");

            GetComponent<Camera>().targetTexture = rt;
        }

        private void LateUpdate()
        {
            var newPosition = _player.position;
            newPosition.y = newPosition.y + Y_AXIS_OFFSET;
            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(90, _player.eulerAngles.y, 0);
        }
    }
}


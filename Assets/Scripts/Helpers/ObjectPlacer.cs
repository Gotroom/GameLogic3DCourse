using UnityEngine;
using UnityEngine.AI;


namespace Ermolaev_3D
{
    public class ObjectPlacer : MonoBehaviour
    {
        public int Count = 10;

        [SerializeField] private string _resourceName;
        private Transform _root;
        private int _minDistance = 25;
        private int _maxDistance = 150;

        private void Start()
        {
            CreateObj();
        }

        public void CreateObj()
        {
            _root = new GameObject(_resourceName).transform;
            for (var i = 1; i <= Count; i++)
            {
                var dis = Random.Range(_minDistance, _maxDistance);
                var randomPoint = Random.insideUnitSphere * dis;

                NavMesh.SamplePosition(_root.position + randomPoint, out var hit, dis, NavMesh.AllAreas);
                Vector3 positionToPlace = hit.position;

                Instantiate(Resources.Load(_resourceName), positionToPlace, Quaternion.identity, _root);
            }
        }
    }
}


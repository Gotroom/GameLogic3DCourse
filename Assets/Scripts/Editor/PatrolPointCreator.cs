#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


namespace Ermolaev_3D
{
    [CustomEditor(typeof(PatrolPoint))]
    public class PatrolPointCreator : UnityEditor.Editor
    {
        private const float AXIS_Y_POINT_OFFSET = 1.0f;
        private PatrolPoint _testTarget;

        private void OnEnable()
        {
            _testTarget = (PatrolPoint)target;
        }


        private void OnSceneGUI()
        {
            if (Event.current.button == 0 && Event.current.type == EventType.MouseDown)
            {
                Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
                    SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

                if (Physics.Raycast(ray, out var hit))
                {
                    var position = hit.point;
                    position.y = AXIS_Y_POINT_OFFSET;
                    _testTarget.InstantiateObj(position);
                    SetObjectDirty(_testTarget.gameObject);
                }
            }

            Selection.activeGameObject = FindObjectOfType<PatrolPoint>().gameObject;

        }

        public void SetObjectDirty(GameObject obj)
        {
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }
        }
    }
}
#endif

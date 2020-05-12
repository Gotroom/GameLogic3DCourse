using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ermolaev_3D
{

	[CreateAssetMenu(fileName = "ScenesHolder", menuName =
        "CreateScriptableObject/ScenesHolder", order = 1)]
	public class ScenesHolderScriptableObject : BaseScriptableObject
	{
        [System.Serializable]
        public struct SceneDate
        {
            public Object MainMenu;
            public Object Game;
        }

        public SceneDate Scenes;
	}
}
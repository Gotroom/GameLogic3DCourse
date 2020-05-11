using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ermolaev_3D
{
    public class SceneManagerHelper : Singleton<SceneManagerHelper>
    {

        [HideInInspector] public ScenesHolderScriptableObject.SceneDate Scenes;

        private void Awake()
        {
            var tmp = Resources.Load<ScenesHolderScriptableObject>("ScenesHolder");
            Scenes = tmp.Scenes;
        }

        public override bool Check()
        {
            if (Scenes.Game != null && Scenes.MainMenu != null)
                return true;
            return false;
        }
    }
}
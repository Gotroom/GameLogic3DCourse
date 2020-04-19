using Helper;

public class SceneManagerHelper :  Singleton<SceneManagerHelper>
{
    [System.Serializable]
    public struct SceneDate
    {
        public SceneField MainMenu;
        public SceneField Game;
    }

    public SceneDate Scenes;
}

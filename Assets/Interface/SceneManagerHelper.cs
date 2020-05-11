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

    public override bool Check()
    {
        if (Scenes.Game != null && Scenes.MainMenu != null)
            return true;
        return false;
    }
}

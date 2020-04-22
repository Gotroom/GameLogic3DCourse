public interface IControl
{
    UnityEngine.GameObject Instance { get; }
    UnityEngine.UI.Selectable Control { get; }
}

public interface IControlText : IControl
{
    UnityEngine.UI.Text GetText { get; } 
}

public interface IControlOptions : IControl
{
    System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData> GetOptions { get; }
}

public interface IControlImage: IControl
{
    UnityEngine.UI.Image GetImage { get; }
}

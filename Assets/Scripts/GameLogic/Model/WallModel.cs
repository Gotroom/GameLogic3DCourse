namespace Ermolaev_3D
{
    public sealed class WallModel : BaseObjectModel, ISelectable
    {
        public string GetMessage()
        {
            return Name;
        }
    }
}
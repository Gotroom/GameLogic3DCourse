namespace Ermolaev_3D
{
    public abstract class BaseController
    {
        protected UIInterface UiInterface;
        protected BaseController()
        {
            UiInterface = new UIInterface();
        }

        public bool IsActive { get; private set; }

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(params BaseObjectModel[] obj)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch(params BaseObjectModel[] obj)
        {
            if (!IsActive)
            {
                On(obj);
            }
            else
            {
                Off();
            }
        }
    }
}

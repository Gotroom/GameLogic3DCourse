using UnityEngine;
using UnityEngine.UI;

namespace Ermolaev_3D
{
    public class InterfaceResources : MonoBehaviour
    {

        public ButtonUi ButtonPrefab { get; private set; }
        public Canvas MainCanvas { get; private set; }
        public LayoutGroup MainPanel { get; private set; }
        public SliderUI ProgressbarPrefab { get; private set; }
        private void Awake()
        {
            ButtonPrefab = Resources.Load<ButtonUi>("Button");
            MainCanvas = FindObjectOfType<Canvas>();
            ProgressbarPrefab = Resources.Load<SliderUI>("ProgressBar");
            MainPanel = MainCanvas.GetComponentInChildren<LayoutGroup>();
        }
    }

}
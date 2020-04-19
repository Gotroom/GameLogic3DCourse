using UnityEngine;


namespace Ermolaev_3D
{
    public class ScreenshotTaker : MonoBehaviour
    {

        private Camera _camera;
        private ViewFinderUI _viewFinder;
        private bool _isTakingScreenshot = false;

        private RenderTexture _temporaryTexture;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _viewFinder = Object.FindObjectOfType<ViewFinderUI>();
        }

        private void Update()
        {
            if (!_viewFinder.gameObject.activeInHierarchy && !_isTakingScreenshot)
            {
                _viewFinder.SetActive(true);
            }
        }

        private void OnPostRender()
        {
            if (_isTakingScreenshot)
            {                
                _isTakingScreenshot = false;

                if (!System.IO.Directory.Exists(Application.dataPath + "/Photo/"))
                    System.IO.Directory.CreateDirectory(Application.dataPath + "/Photo/");
                ScreenCapture.CaptureScreenshot(Application.dataPath + $"/Photo/Photo_{System.DateTime.Now.ToFileTime()}.png");
            }
        }

        public void TakeScreenShot()
        {
            _viewFinder.SetActive(false);
            _isTakingScreenshot = true;
        }
    }
}
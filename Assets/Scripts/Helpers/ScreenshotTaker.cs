using UnityEngine;


namespace Ermolaev_3D
{
    public class ScreenshotTaker : MonoBehaviour
    {

        private Camera _camera;
        private bool _isTakingScreenshot = false;

        private RenderTexture _temporaryTexture;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void OnPostRender()
        {
            if (_isTakingScreenshot)
            {
                _isTakingScreenshot = false;

                var texture = _camera.targetTexture;
                Texture2D photo = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                photo.ReadPixels(rect, 0, 0);

                byte[] photoByteArray = photo.EncodeToPNG();
                if (!System.IO.Directory.Exists(Application.dataPath + "/Photo/"))
                    System.IO.Directory.CreateDirectory(Application.dataPath + "/Photo/");
                System.IO.File.WriteAllBytes(Application.dataPath + $"/Photo/Photo_{System.DateTime.Now.ToFileTime()}.png", photoByteArray);

                RenderTexture.ReleaseTemporary(texture);
                if (_temporaryTexture != null)
                {
                    _camera.targetTexture = _temporaryTexture;
                    _temporaryTexture = null;
                }
                else
                {
                    _camera.targetTexture = null;
                }
            }
        }

        public void TakeScreenShot()
        {
            if (_camera.targetTexture)
            {
                _temporaryTexture = _camera.targetTexture;
            }
            _camera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.width);
            _isTakingScreenshot = true;
        }
    }
}
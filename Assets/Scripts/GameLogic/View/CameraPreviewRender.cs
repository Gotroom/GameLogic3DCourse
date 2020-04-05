using UnityEngine;
using System.Collections;

public class CameraPreviewRender : MonoBehaviour
{
    void Start()
    {
        var rt = Resources.Load<RenderTexture>("CameraTexture");

        GetComponent<Camera>().targetTexture = rt;
    }
}

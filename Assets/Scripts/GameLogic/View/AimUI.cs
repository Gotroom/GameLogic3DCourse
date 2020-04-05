using UnityEngine;
using System.Collections;


namespace Ermolaev_3D
{
    public abstract class AimUI : MonoBehaviour
    {
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
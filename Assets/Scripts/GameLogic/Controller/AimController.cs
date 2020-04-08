﻿using UnityEngine;


namespace Ermolaev_3D
{
    public class AimController : BaseController, IInitializable
    {

        public void Initialization()
        {
            UiInterface.ViewFinderUI.SetActive(false);
        }

        public void ShowAiming(AimableWeaponModel weapon)
        {
            if (weapon is PhotoCamera camera)
            {
                camera.TakeAim();
                UiInterface.ViewFinderUI.SetActive(true);
                UiInterface.SetActiveNonAim(false);
            }
        }

        public void HideAiming(AimableWeaponModel weapon)
        {
            if (weapon is PhotoCamera camera)
            {
                camera.CancelTakingAim();
                UiInterface.ViewFinderUI.SetActive(false);
                UiInterface.SetActiveNonAim(true);
            }
        }
    }
}
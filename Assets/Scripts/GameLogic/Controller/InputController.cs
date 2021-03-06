﻿using UnityEngine;

namespace Ermolaev_3D
{
    public sealed class InputController : BaseController, IExecutable
    {
        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cancel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _save = KeyCode.F5;
        private KeyCode _load = KeyCode.F9;

        private KeyCode[] _changeWeaponKeys =
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
        };

        private Vector2 _mouseScroll;
        private int _mouseButton = (int)MouseButton.LeftButton;
        private int _alternativeMouseButton = (int)MouseButton.RightButton;

        private bool _isTakingAim = false;

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _mouseScroll = Input.mouseScrollDelta;
        }

        public void Execute()
        {
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            if (Input.GetKeyDown(_save))
            {
                ServiceLocator.Resolve<SaveLoadController>().Save();
            }

            if (Input.GetKeyDown(_load))
            {
                ServiceLocator.Resolve<SaveLoadController>().Load();
            }

            for (int i = 0; i < _changeWeaponKeys.Length; i++)
            {
                if (Input.GetKeyDown(_changeWeaponKeys[i]))
                {
                    SelectWeapon(i);
                }
            }

            if (_mouseScroll.y > Input.mouseScrollDelta.y)
            {
                if (_isTakingAim)
                {
                    if (ServiceLocator.Resolve<WeaponController>().IsActive)
                    {
                        ServiceLocator.Resolve<WeaponController>().ProcessWheelScroll(false);
                    }
                }
                else
                {
                    SelectPreviousWeapon();
                }
            }
            else if (_mouseScroll.y < Input.mouseScrollDelta.y)
            {
                if (_isTakingAim)
                {
                    if (ServiceLocator.Resolve<WeaponController>().IsActive)
                    {
                        ServiceLocator.Resolve<WeaponController>().ProcessWheelScroll(true);
                    }
                }
                else
                {
                    SelectNextWeapon();
                }
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetMouseButtonDown(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Draw();
                }
            }

            if (Input.GetMouseButtonUp(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Release();
                }
            }

            if (Input.GetMouseButtonDown(_alternativeMouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    if (_isTakingAim)
                    {
                        ServiceLocator.Resolve<WeaponController>().CancelTakingAim();
                        _isTakingAim = false;
                    }
                    else
                    {
                        ServiceLocator.Resolve<WeaponController>().TakeAim();
                        _isTakingAim = true;
                    }
                }
            }

            if (Input.GetKeyDown(_cancel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                ServiceLocator.Resolve<WeaponController>().ReloadClip();
            }
        }

        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeapon(i);
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        private void SelectPreviousWeapon()
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetPreviousWeapon();
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        private void SelectNextWeapon()
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetNextWeapon();
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }
    }
}

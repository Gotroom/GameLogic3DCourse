﻿using UnityEngine;


namespace Ermolaev_3D
{
    public sealed class Bow : DrawableWeaponModel
    {
        private Animator _animator;

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }

        public override void Fire()
        {
            return;
        }

        public override void Draw()
        {
            _animator.SetBool("IsCocked", true);
        }

        public override void Release()
        {
            _animator.SetBool("IsCocked", false);
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            var euler = _barrel.rotation.eulerAngles;
            Debug.Log(euler);
            euler.x += -90;
            Quaternion rotation = Quaternion.Euler(euler.x, euler.y, euler.z);
            var temAmmunition = Instantiate(Ammunition, _barrel.position, rotation);//todo Pool object
            temAmmunition.AddForce(_barrel.forward * _force);
            Clip.CountAmmunition--;
            _isReady = false;
            Invoke(nameof(ReadyShoot), _rechargeTime); // todo таймер контроллер 
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Ermolaev_3D
{
    public abstract class WeaponModel : BaseObjectModel
    {
        private int _maxCountAmmunition = 40;
        private int _minCountAmmunition = 20;
        private int _countClip = 5;
        public Ammunition Ammunition;
        public Clip Clip;

        public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999;
        [SerializeField] protected float _rechargeTime = 0.2f;
        [SerializeField] protected int _poolCount = 300;
        private Queue<Clip> _clips = new Queue<Clip>();

        protected ObjectPool _bulletPool;
        protected bool _isReady = true;

        public int CountClip => _clips.Count;

        protected override void Awake()
        {
            base.Awake();
            _bulletPool = new ObjectPool(Ammunition, _poolCount);
        }

        private void Start()
        {
            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = Random.Range(_minCountAmmunition, _maxCountAmmunition) });
            }

            ReloadClip();
        }

        public abstract void Fire();

        protected void UseAmmunitionFromPool()
        {
            UseAmmunitionFromPool(_barrel);
        }


        protected void UseAmmunitionFromPool(Transform transform)
        {
            UseAmmunitionFromPool(transform.position, transform.rotation, transform.forward);
        }

        protected void UseAmmunitionFromPool(Vector3 position, Quaternion rotation, Vector3 direction)
        {
            Ammunition tempAmmo = _bulletPool.GetFromPool() as Ammunition;
            tempAmmo.transform.position = transform.position;
            tempAmmo.transform.rotation = rotation;
            tempAmmo.gameObject.SetActive(true);
            tempAmmo.AddForce(direction * _force);
        }

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        public void ReloadClip()
        {
            if (CountClip <= 0) return;
            Clip = _clips.Dequeue();
        }
    }
}
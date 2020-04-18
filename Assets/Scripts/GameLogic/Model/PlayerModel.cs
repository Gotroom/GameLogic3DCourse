using UnityEngine;
using System;

namespace Ermolaev_3D
{
    public sealed class PlayerModel : MovableObjectModel, IDamagable
    {
        public event Action<float> HealthChanged;

        [SerializeField] private float _health = 100.0f;

        private static readonly int _forward = Animator.StringToHash("Forward");
        private static readonly int _turn = Animator.StringToHash("Turn");
        private static readonly int _crouch = Animator.StringToHash("Crouch");
        private static readonly int _onGround = Animator.StringToHash("OnGround");
        private static readonly int _jump = Animator.StringToHash("Jump");
        private static readonly int _jumpLeg = Animator.StringToHash("JumpLeg");

        private Animator _animator;

        public PlayerModel(CharacterController instance) : base(instance)
        {
            _animator = _characterController.GetComponent<Animator>();
        }

        protected override void CharecterMove()
        {
            base.CharecterMove();
            UpdateAnimator();
        }

        protected override void GamingGravity()
        {
            base.GamingGravity();
        }

        protected override void LookRotation(Transform character, Transform camera)
        {
            base.LookRotation(character, camera);
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat(_forward, _input.y);
            _animator.SetFloat(_turn, _input.x);
            _animator.SetBool(_onGround, _characterController.isGrounded);
            _animator.SetFloat(_jump, _gravityForce);
            _animator.SetFloat(_jumpLeg, _input.y);
        }

        public float Health
        {
            get { return _health; }
        }

        public void SetDamage(CollisionInfo info)
        {
            if (_health > 0.0f)
            {
                _health -= info.Damage;
                HealthChanged.Invoke(_health);
            }

            if (_health <= 0.0f)
            {
                HealthChanged.Invoke(0.0f);
            }
        }
    }
}
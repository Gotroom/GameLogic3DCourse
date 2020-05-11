using System;
using UnityEngine;
using UnityEngine.AI;

namespace Ermolaev_3D
{
    public sealed class Bot : BaseEnemyModel
    {
        public Vision Vision;
        public WeaponModel Weapon; //todo с разным оружием 
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        public bool UsesWeapon = false;
        public float DetectionCooldown = 5.0f;
        private BotStates _stateBot;
        private Vector3 _point;
        private Animator _animator;
        private float _waitTime = 3;
        private float _stoppingDistance = 2.0f;
        private float _detectionTime = 0.0f;

        private static readonly int _idle = Animator.StringToHash("IsIdle");
        private static readonly int _run = Animator.StringToHash("IsRunning");
        private static readonly int _walk = Animator.StringToHash("IsWalking");
        private static readonly int _site = Animator.StringToHash("IsSite");

        private BotStates BotStates
        {
            get => _stateBot;
            set
            {
                _stateBot = value;
                switch (value)
                {
                    case BotStates.None:
                        if (_animator)
                        {
                            _animator.SetBool(_idle, true);
                            _animator.SetBool(_run, false);
                            _animator.SetBool(_walk, false);
                            _animator.SetBool(_site, false);
                        }
                        else
                        {
                            Color = Color.white;
                        }
                        break;
                    case BotStates.Patrol:
                        if (_animator)
                        {
                            _animator.SetBool(_idle, false);
                            _animator.SetBool(_run, false);
                            _animator.SetBool(_walk, true);
                            _animator.SetBool(_site, false);
                        }
                        else
                        {
                            Color = Color.green;
                        }
                        break;
                    case BotStates.Inspection:
                        if (_animator)
                        {
                            _animator.SetBool(_idle, false);
                            _animator.SetBool(_run, false);
                            _animator.SetBool(_walk, false);
                            _animator.SetBool(_site, true);
                        }
                        else
                        {
                            Color = Color.yellow;
                        }
                        break;
                    case BotStates.Detected:
                        if (_animator)
                        {
                            _animator.SetBool(_idle, false);
                            _animator.SetBool(_run, true);
                            _animator.SetBool(_walk, false);
                            _animator.SetBool(_site, false);
                        }
                        else
                        {
                            Color = Color.red;
                        }
                        break;
                    case BotStates.Died:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }

            }
        }

        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null)
            {
                bodyBot.OnApplyDamageChange += SetDamage;
                bodyBot.WasSelected += GetMessage;
            }

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null)
            {
                headBot.OnApplyDamageChange += SetDamage;
                headBot.WasSelected += GetMessage;
            }
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null)
            {
                bodyBot.OnApplyDamageChange -= SetDamage;
                bodyBot.WasSelected -= GetMessage;
            }

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null)
            {
                headBot.OnApplyDamageChange -= SetDamage;
                headBot.WasSelected -= GetMessage;
            }
        }

        public void Tick()
        {
            if (!Agent) return;
            if (BotStates == BotStates.Died) return;

            if (BotStates != BotStates.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (BotStates != BotStates.Inspection)
                    {
                        if (BotStates != BotStates.Patrol)
                        {
                            BotStates = BotStates.Patrol;
                            _point = Patrol.GenericPoint(transform, false);
                            MovePoint(_point);
                            Agent.stoppingDistance = 0;
                        }
                        else
                        {
                            if (Vector3.Distance(_point, transform.position) <= 1)
                            {
                                BotStates = BotStates.Inspection;
                                Invoke(nameof(ResetStateBot), _waitTime);
                            }
                        }
                    }
                }
                var pos = Target.position;
                pos.y += 0.75f;
                if (Vision.VisionM(transform, Target))
                {
                    SetDetected();
                }
            }
            else
            {
                if (Agent.stoppingDistance != _stoppingDistance)
                {
                    Agent.stoppingDistance = _stoppingDistance;
                }
                if (Vision.VisionM(transform, Target))
                {
                    if (UsesWeapon)
                    {
                        if (Weapon.Clip.CountAmmunition <= 0)
                            Weapon.ReloadClip();
                        Weapon.Fire();
                    }
                    else
                    {

                    }
                    _detectionTime = 0.0f;
                }
                else
                {
                    MovePoint(Target.position);
                    _detectionTime += Time.deltaTime;
                    if (_detectionTime >= DetectionCooldown)
                    {
                        ResetStateBot();
                    }
                }
            }
        }

        public void ManualDestroy()
        {
            Destroy(gameObject);
        }

        private void SetDetected()
        {
            BotStates = BotStates.Detected;
            _detectionTime = 0.0f;
        }

        private void ResetStateBot()
        {
            if (BotStates != BotStates.Died)
            {
                BotStates = BotStates.None;
                Agent.ResetPath();
            }
        }

        public override void SetDamage(CollisionInfo info)
        {
            base.SetDamage(info);
            if (HealthPoints > 0)
            {
                if (BotStates != BotStates.Detected)
                {
                    SetDetected();
                }
                return;
            }

            if (HealthPoints <= 0)
            {
                BotStates = BotStates.Died;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }
                }
            }
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }

        public override string GetMessage()
        {
            return $"{Name} : {HealthPoints}";
        }
    }
}
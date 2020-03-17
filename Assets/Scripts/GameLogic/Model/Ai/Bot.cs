using System;
using UnityEngine;
using UnityEngine.AI;

namespace Ermolaev_3D
{
    public sealed class Bot : BaseEnemyController
    {
        public Vision Vision;
        public WeaponModel Weapon; //todo с разным оружием 
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        public float DetectionCooldown = 5.0f;
        private float _waitTime = 3;
        private BotStates _stateBot;
        private Vector3 _point;
        private float _stoppingDistance = 2.0f;
        private float _detectionTime = 0.0f;

        private BotStates BotStates
        {
            get => _stateBot;
            set
            {
                _stateBot = value;
                switch (value)
                {
                    case BotStates.None:
                        Color = Color.white;
                        break;
                    case BotStates.Patrol:
                        Color = Color.green;
                        break;
                    case BotStates.Inspection:
                        Color = Color.yellow;
                        break;
                    case BotStates.Detected:
                        Color = Color.red;
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
                    Weapon.Fire();
                    _detectionTime = 0.0f;
                }
                else
                {
                    MovePoint(Target.position);
                    _detectionTime += Time.deltaTime;
                    if (_detectionTime >= DetectionCooldown)
                    {
                        print("lost");
                        ResetStateBot();
                    }
                }
            }
        }

        private void SetDetected()
        {
            BotStates = BotStates.Detected;
            _detectionTime = 0.0f;
        }

        private void ResetStateBot()
        {
            BotStates = BotStates.None;
            Agent.ResetPath();
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
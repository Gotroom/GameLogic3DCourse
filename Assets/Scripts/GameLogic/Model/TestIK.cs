using UnityEngine;

namespace Ermolaev_3D
{
    [RequireComponent(typeof(Animator))]

    public class TestIK : MonoBehaviour
    {
        public bool IsActive = false;
        public GameObject GrabingObject = null;
        public Transform LookObj = null;

        public LayerMask RaycastLayer;

        public Vector3 LeftFootOffset;
        public Vector3 RightFootOffset;

        [SerializeField] private float _weightLeftFoot;
        [SerializeField] private float _weightRightFoot;

        private Animator _animator;
        private Transform _rightFoot;
        private Transform _leftFoot;
        private Vector3 _leftFootPosition;
        private Vector3 _rightFootPosition;
        private Quaternion _rightFootRotation;
        private Quaternion _leftFootRotation;

        private GrabPoint _leftGrabPoint = null;
        private GrabPoint _rightGrabPoint = null;

        private void OnValidate()
        {
            GetAnimationBones();
            var grabPoints = GrabingObject.GetComponentsInChildren<GrabPoint>();
            foreach (var point in grabPoints)
            {
                if (point.IsLeft)
                {
                    _leftGrabPoint = point;
                }
                else
                {
                    _rightGrabPoint = point;
                }
            }
        }

        private void Update()
        {
            if (_rightFoot == null || _leftFoot == null || _animator == null)
            {
                GetAnimationBones();
            }
            if (Time.frameCount % 2 == 0)
            {
                var rightPos = _rightFoot.TransformPoint(Vector3.zero);
                if (Physics.Raycast(rightPos, Vector3.down, out var rightHit, 1, RaycastLayer))
                {
                    _rightFootPosition = Vector3.Lerp(_rightFoot.position, rightHit.point, 0.5f);
                    _rightFootRotation = Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation;
                }
            }
            else
            {
                var leftPos = _leftFoot.TransformPoint(Vector3.zero);
                if (Physics.Raycast(leftPos, Vector3.down, out var leftHit, 1, RaycastLayer))
                {
                    _leftFootPosition = Vector3.Lerp(_leftFoot.position, leftHit.point, 0.5f);
                    _leftFootRotation = Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation;
                }
            }
        }

        void GetAnimationBones()
        {
            _animator = GetComponent<Animator>();
            _rightFoot = _animator.GetBoneTransform(HumanBodyBones.RightFoot);
            _leftFoot = _animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        }

        void OnAnimatorIK()
        {
            if (_animator)
            {

                if (IsActive)
                {
                    _weightRightFoot = _animator.GetFloat("Right_Leg");
                    _weightLeftFoot = _animator.GetFloat("Left_Leg");

                    FootIK();

                    if (LookObj != null)
                    {
                        _animator.SetLookAtWeight(1);
                        _animator.SetLookAtPosition(LookObj.position);
                    }

                    if (GrabingObject != null && _rightGrabPoint != null && _rightGrabPoint.IsVisible)
                    {
                        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightGrabPoint.transform.position);
                        _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightGrabPoint.transform.rotation);
                    }

                    if (GrabingObject != null && _leftGrabPoint != null && _leftGrabPoint.IsVisible)
                    {
                        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftGrabPoint.transform.position);
                        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftGrabPoint.transform.rotation);
                    }
                }
                else
                {
                    _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    _animator.SetLookAtWeight(0);
                }
            }

            void FootIK()
            {
                _weightRightFoot = _animator.GetFloat("Right_Leg");
                _weightLeftFoot = _animator.GetFloat("Left_Leg");

                _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _weightLeftFoot);
                _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _weightRightFoot);

                _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _weightLeftFoot);
                _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _weightRightFoot);

                _animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition + new Vector3(0, LeftFootOffset.y, 0));
                _animator.SetIKPosition(AvatarIKGoal.RightFoot, _rightFootPosition + new Vector3(0, RightFootOffset.y, 0));

                _animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);
                _animator.SetIKRotation(AvatarIKGoal.RightFoot, _rightFootRotation);

            }
        }
    }
}

﻿using UnityEngine;

namespace Ermolaev_3D
{
    [System.Serializable]
    public sealed class Vision
    {
        public float ActiveDis = 10;
        public float ActiveAng = 35;
        public LayerMask Layers;

        public bool VisionM(Transform player, Transform target)
        {
            return Distance(player, target) && Angle(player, target) && !CheckBloked(player, target);
        }

        private bool CheckBloked(Transform player, Transform target)
        {
            if (!Physics.Linecast(player.position, target.position, out var hit, Layers)) return true;
            return hit.transform != target;
        }

        private bool Angle(Transform player, Transform target)
        {
            var angle = Vector3.Angle(player.forward, target.position - player.position);
            return angle <= ActiveAng;
        }

        private bool Distance(Transform player, Transform target)
        {
            var pos = target.position;
            pos.y += 2;
            var dist = Vector3.Distance(player.position, pos); //todo оптимизация
            return dist <= ActiveDis;
        }
    }
}

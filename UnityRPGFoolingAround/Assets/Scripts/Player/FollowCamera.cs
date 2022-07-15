using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraFollower
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                /*Transform gizNextSpot =
                    i == transform.childCount - 1 ?
                    transform.GetChild(0) : transform.GetChild(i + 1);*/
                Transform gizNextSpot = transform.GetChild(GetNextIndex(i));

                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(GetWayPoint(i), 0.4f);

                Gizmos.DrawLine(GetWayPoint(i), gizNextSpot.position);
            }
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i)
        {
            return (i + 1) % transform.childCount;
        }
    }
}

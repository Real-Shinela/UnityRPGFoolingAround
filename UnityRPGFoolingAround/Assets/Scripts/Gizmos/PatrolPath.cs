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
                Transform gizSpot = transform.GetChild(i);
                /*Transform gizNextSpot =
                    i == transform.childCount - 1 ?
                    transform.GetChild(0) : transform.GetChild(i + 1);*/
                Transform gizNextSpot = transform.GetChild((i + 1) % transform.childCount);

                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(gizSpot.position, 0.4f);

                Gizmos.DrawLine(gizSpot.position, gizNextSpot.position);
            }
        }
    }
}

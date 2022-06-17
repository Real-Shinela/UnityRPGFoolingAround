using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) MoveToCursor();
        UpdateAnimation();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit);
        if (hasHit) GetComponent<NavMeshAgent>().destination = hit.point;
    }

    private void UpdateAnimation()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<NavMeshAgent>().velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}

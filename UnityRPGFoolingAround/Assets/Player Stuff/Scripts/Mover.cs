using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    private void Start()
    {
        GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        MoveForwards();

        UpdateAnimation();
    }

    private void MoveForwards()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horInput, 0f, verInput);
        Vector3 moveDestination = transform.position + movement;
        GetComponent<NavMeshAgent>().destination = moveDestination;
    }

    private void UpdateAnimation()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<NavMeshAgent>().velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }


    /*[SerializeField] Transform target;

    void Update()
    {
        if (Input.GetMouseButton(0)) MoveToCursor();
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
    }*/
}

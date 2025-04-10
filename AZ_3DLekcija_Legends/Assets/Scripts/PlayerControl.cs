using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed = 7;
    
    private CharacterController characterController;
    private Animator animator;
    private Vector3 targetPosition;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }
    void Update()
    {
        float distToTarget = Vector3.Distance(targetPosition, transform.position);
        if (distToTarget > 0.5f)
        {
            Vector3 direction = Vector3.Normalize(targetPosition - transform.position);
            characterController.Move(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, layerMask))
            {
                Debug.Log("Hit: " + hit.collider.name);
                targetPosition = hit.point;
                transform.LookAt(targetPosition);
            }
        }
    }
}

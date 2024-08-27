using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    [HideInInspector] Transform target;

    [SerializeField] float moveSpeed;

    [SerializeField] float rotateSpeed;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void FixedUpdate()
    {
        if (target == null)
            return;
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        transform.LookAt(transform.position);
    } 
}

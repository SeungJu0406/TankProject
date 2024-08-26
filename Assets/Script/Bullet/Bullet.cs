using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float speed;

    private void OnEnable()
    {
        rigidbody.velocity = Vector3.zero;
    }
    private void Update()
    {
        Fly();
    }
    void Fly()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}

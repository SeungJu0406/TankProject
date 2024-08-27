using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] public Vector3 speed;
    private void OnEnable()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;   
    }
    void Fire()
    {
        rigidbody.AddForce(speed, ForceMode.Impulse); 
    }
    public void SetSpeed(Vector3 speed)
    {
        this.speed = speed;
    }
}

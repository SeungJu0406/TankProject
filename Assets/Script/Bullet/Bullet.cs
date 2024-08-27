using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Vector3 speed;
    bool isfire;
    private void OnEnable()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        isfire = true;     
    }
    private void Update()
    {
        if (isfire)
        {
            Fire();
            isfire = false;
        }
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

using UnityEngine;

public class Blueberry : MonoBehaviour
{
    private BlueberryPool pool;
    private float timer = 0f;
    private float lifeTime = 3f; 

    // Called once when spawned
    public void Init(BlueberryPool poolRef)
    {
        pool = poolRef;
        timer = 0f;

        // Reset physics
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            pool.ReturnBlueberry(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Return to pool on impact
        pool.ReturnBlueberry(gameObject);
    }
}

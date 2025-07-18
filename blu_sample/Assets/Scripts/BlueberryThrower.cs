using UnityEngine;

public class BlueberryThrower : MonoBehaviour
{
    public BlueberryPool blueberryPool;   
    public Transform throwPoint;         

    public float throwForce = 10f;        // You can change this in the inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))  // Press space to throw
        {
            // Get a blueberry from the pool
            Debug.Log("blueberry thrown");
            GameObject bb = blueberryPool.GetBlueberry();

        bb.transform.position = throwPoint.position;
        bb.transform.rotation = throwPoint.rotation;

        // Tell it which pool it belongs to
        bb.GetComponent<Blueberry>().Init(blueberryPool);

        // Reset and throw
        Rigidbody rb = bb.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }
    }
}

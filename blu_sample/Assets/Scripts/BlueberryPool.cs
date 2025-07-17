using System.Collections.Generic;
using UnityEngine;

public class BlueberryPool : MonoBehaviour
{
    public GameObject blueberryPrefab; 
    public int poolSize = 10;           
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // Fill the pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject blueberry = Instantiate(blueberryPrefab);
            blueberry.SetActive(false);
            pool.Enqueue(blueberry);
        }
    }

    public GameObject GetBlueberry()
    {
        if (pool.Count > 0)
        {
            GameObject bb = pool.Dequeue();
            bb.SetActive(true);
            return bb;
        }

        GameObject newBB = Instantiate(blueberryPrefab);
        newBB.SetActive(true);
        return newBB;
    }

    public void ReturnBlueberry(GameObject bb)
    {
        bb.SetActive(false);
        pool.Enqueue(bb);
    }
}

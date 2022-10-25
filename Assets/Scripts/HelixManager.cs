using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixManager : MonoBehaviour
{
    public GameObject[] helixRings;
    public float ySpawn = 0;
    public float ringsDistance = 5;

    public int numberOfRings;

    // Start is called before the first frame update
    void Start()
    {
        numberOfRings = GameManager.currentLevelIndex + 5;

        // Spawn helix rings
        for (int i = 0; i < numberOfRings; i++)
        {
            if (i == 0)
                SpawnRing(i);
            else
                SpawnRing(Random.Range(1, helixRings.Length - 1));

        }

        // Spawn last ring
        SpawnRing(helixRings.Length - 1);
    }

    public void SpawnRing(int ringIndex)
    {
        GameObject ring = Instantiate(helixRings[ringIndex], transform.up * ySpawn, Quaternion.identity);
        ring.transform.parent = transform;
        ySpawn -= ringsDistance;
    }
}

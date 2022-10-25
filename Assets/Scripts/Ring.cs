using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private GameObject[] players;

    // Start is called before the first frame update
    void Start()
    {
        // players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            Transform player = p.transform;
            if (transform.position.y > player.position.y)
            {
                GameManager.numberOfPassedRings++;
                GameManager.score++;
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("whoosh");
            }
        }
        
    }
}

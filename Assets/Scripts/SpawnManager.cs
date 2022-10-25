using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject helixTower;
    private float xPos = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn additional one ball after completing level 2 and additional two balls after completing level 3
        for (int i = 1; i < GameManager.currentLevelIndex && i <= 2; i++)
        {
            StartCoroutine(SpawnExtraBalls(xPos));
            xPos -= 2 * xPos;
        }
    }

    // Countdown timer to spawn balls
    IEnumerator SpawnExtraBalls(float x)
    {
        Vector3 posApart = new Vector3(x, 0, 0);
        Vector3 direction = ((player.transform.position + posApart) - helixTower.transform.position).normalized;
        Vector3 pos = direction * (player.transform.position - helixTower.transform.position).magnitude;
        yield return new WaitForSeconds(2);
        Instantiate(player, pos, player.transform.rotation);
    }
}

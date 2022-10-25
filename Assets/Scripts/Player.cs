using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public AudioManager audioManager;
    public float bounceForce = 6.0f;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioManager.Play("bounce");

        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);
        string materialName = collision.gameObject.GetComponent<MeshRenderer>().material.name;

        if (materialName == "Safe (Instance)")
        {

        }else if (materialName == "Unsafe (Instance)")
        {
            GameManager.gameOver = true;
            audioManager.Play("game over");
        } else if (materialName == "Last Ring (Instance)" && !GameManager.levelCompleted)
        {
            GameManager.levelCompleted = true;
            audioManager.Play("win level");
        }
    }
}

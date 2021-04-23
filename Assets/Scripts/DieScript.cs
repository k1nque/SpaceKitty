using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    public GameObject respawn;
    public PlayerControl player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = respawn.transform.position;
            player.HealthDown();
        }
    }
}

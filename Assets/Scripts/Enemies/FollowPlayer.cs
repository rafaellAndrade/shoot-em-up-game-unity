using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    public float speed;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    
    void Update()
    {
        // Script apenas para localizar o player e se movimentar até ele
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}

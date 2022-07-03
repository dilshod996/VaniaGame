using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    Rigidbody2D myrigid;
    [SerializeField] float bulletSpeed = 2f;
    PlayerMovement player;
    float xSpeed;
    void Start()
    {
        myrigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        myrigid.velocity = new Vector2(xSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        Debug.Log("Error");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed =1f;
    Rigidbody2D myrig;
    BoxCollider2D myTurnCollider;
    // Start is called before the first frame update
    void Start()
    {
        myrig = GetComponent<Rigidbody2D>();
        myTurnCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()        
    {
        myrig.velocity = new Vector2(movementSpeed, 0f);   
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        movementSpeed = -movementSpeed;
        Flipping();
        Debug.Log("I reach the position");
    }
    void Flipping()
    {
        transform.localScale = new Vector2(-Mathf.Sign(myrig.velocity.x), 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public int id = 0;
    public int power;
    public BallCategory ballCategory;
    public float speed = 4.0f;

    private const float cameraSize = 6.0f;
    private const float screenRatio = 16.0f / 9.0f;
    private float screenY = cameraSize;
    private float screenX = cameraSize * screenRatio;

    private Vector2 direction;

    private Rigidbody2D rigidBodyComponent;
    private CircleCollider2D colliderComponent;

    void Start()
    {
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

        rigidBodyComponent = this.gameObject.AddComponent<Rigidbody2D>();
        rigidBodyComponent.bodyType = RigidbodyType2D.Kinematic;

        colliderComponent = this.gameObject.AddComponent<CircleCollider2D>();
        colliderComponent.isTrigger = true;
    }


    void Update()
    {
        MoveBall();
    }


    private void MoveBall()
    {
        transform.Translate(direction * speed * Time.deltaTime);


        if (transform.position.y > screenY)
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0f));
        }

        if (transform.position.y < -screenY)
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f));
        }

        if (transform.position.x > screenX)
        {
            direction = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f));
        }

        if (transform.position.x < -screenX)
        {
            direction = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionBall = collision.gameObject.GetComponent<Ball>();

        if (collisionBall.ballCategory == this.ballCategory) //stick together
        {
            if (this.id < collisionBall.id)
            {
                collisionBall.speed = 0;
                this.power += collisionBall.power;
                collision.transform.parent = this.transform;
                var collisionToThisDistance = collision.transform.position - this.transform.position;
                collision.transform.position = this.transform.position + collisionToThisDistance;
            }
        }
        else //collide
        {
            if (this.power > collisionBall.power)
            {
                Destroy(collision.gameObject);
            }

            if (this.power == collisionBall.power)
            {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}

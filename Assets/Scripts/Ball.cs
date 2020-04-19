using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public string id;
    public int power;
    public float speed = 4.0f;
    public BallCategory ballCategory;

    private Vector2 direction;

    Rigidbody2D rigidBodyComponent;
    CircleCollider2D colliderComponent;

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


        if (transform.position.y > 6.0f)
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0f));
        }

        if (transform.position.y < -6.0f)
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f));
        }

        if (transform.position.x > (6.0f / 9.0f) * 16.0f)
        {
            direction = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f));
        }

        if (transform.position.x < (-6.0f / 9.0f) * 16.0f)
        {
            direction = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f));
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionBall = collision.gameObject.GetComponent<Ball>();

        if (collisionBall.ballCategory == this.ballCategory) //stick together
        {
            // creates joint
            FixedJoint2D joint = this.gameObject.AddComponent<FixedJoint2D>();
            // sets joint position to point of contact
            //joint.anchor = collision.GetContacts( .contacts[0].point;
            // conects the joint to the other object
            joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();//collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
            // Stops objects from continuing to collide and creating more joints
            joint.enableCollision = false;
        }
        else //collide
        {
            if (collisionBall.power < this.power)
            {
                Destroy(collision.gameObject);
            }

            if (collisionBall.power == this.power)
            {
                Destroy(this.gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}

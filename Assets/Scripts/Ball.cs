using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    public int id = 0;
    public int power = 0;
    public BallCategory ballCategory;
    public float speed = 4.0f;
    private bool isChild = false;

    private Vector2 direction;
    private Rigidbody2D rigidBodyComponent;
    private CircleCollider2D colliderComponent;

    private const float cameraSize = 6.0f;
    private const float screenRatio = 16.0f / 9.0f;
    private float screenY = cameraSize;
    private float screenX = cameraSize * screenRatio;


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


    /***Compound Collider
     * https://answers.unity.com/questions/410711/trigger-in-child-object-calls-ontriggerenter-in-pa.html?_ga=2.69813719.634029242.1587235353-1415008325.1582214036
     * https://docs.unity3d.com/Manual/class-Rigidbody.html
     *
     * In Compound Collider, only the root parent has rigid body. So have to destroy children objects' rigid body.
     * But the children objects must have collider. So keep the collider, but make it do nothing by the set isChild true.
    ***/
    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherBall = other.gameObject.GetComponent<Ball>();

        if (!this.isChild && !otherBall.isChild)
        {
            if (otherBall.ballCategory == this.ballCategory) //stick together
            {
                otherBall.speed = 0;
                otherBall.isChild = true;
                Destroy(otherBall.rigidBodyComponent);

                this.power += otherBall.power;
                other.transform.parent = this.transform;

                var otherToThisDistance = other.transform.position - this.transform.position;
                other.transform.position = this.transform.position + otherToThisDistance;

                var render = otherBall.GetComponent<SpriteRenderer>();
                render.color = Color.yellow;
            }
            else //collide
            {
                if (this.power > otherBall.power)
                {
                    Destroy(other.gameObject);
                }

                if (this.power == otherBall.power)
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}

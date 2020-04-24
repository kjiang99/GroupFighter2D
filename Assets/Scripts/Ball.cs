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


    private float cameraSize;
    private const float screenRatio = 16.0f / 9.0f;
    private float screenY;
    private float screenX;

    private Vector2 direction;
    //private Vector2 targetPosition;

    private Rigidbody2D rigidBodyComponent;
    private CircleCollider2D colliderComponent;


    [SerializeField]
    private AudioClip[] ballSounds;
    private AudioSource audioSourceComponent;

    void Start()
    {
        cameraSize = Camera.main.orthographicSize;
        screenY = cameraSize;
        screenX = cameraSize * screenRatio;

        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        //targetPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));

        rigidBodyComponent = this.GetComponent<Rigidbody2D>();

        colliderComponent = this.gameObject.AddComponent<CircleCollider2D>();
        colliderComponent.isTrigger = true;

        audioSourceComponent = this.GetComponent<AudioSource>();
    }


    void Update()
    {
        MoveBall();
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
        AudioClip stickClip = ballSounds[0];
        AudioClip collisionClip = ballSounds[1];

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

                audioSourceComponent.PlayOneShot(stickClip);
            }
            else //collide
            {
                if (this.power > otherBall.power)
                {
                    audioSourceComponent.PlayOneShot(collisionClip);
                    Destroy(other.gameObject);
                }

                if (this.power == otherBall.power)
                {
                    AudioSource.PlayClipAtPoint(collisionClip, Camera.main.transform.position); //Both balls destroied, so need to PlayClipAtPoint
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
            }
        }
    }


    private void MoveBall()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y > screenY)
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0f));
        if (transform.position.y < -screenY)
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f));
        if (transform.position.x > screenX)
            direction = new Vector2(Random.Range(-1.0f, 0f), Random.Range(-1.0f, 1.0f));
        if (transform.position.x < -screenX)
            direction = new Vector2(Random.Range(0f, 1.0f), Random.Range(-1.0f, 1.0f));
    }


    //private void MoveBall()
    //{
    //    float distance = Vector2.Distance(transform.position, targetPosition);


    //    if (distance > 0.001f)
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    //    }
    //    else
    //    {
    //        targetPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
    //    }
    //}
}

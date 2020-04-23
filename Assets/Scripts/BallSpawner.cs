using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ball1;

    [SerializeField]
    private GameObject ball3;


    private List<Ball> ballGameObjects = new List<Ball>();


    void Start()
    {
        SpawnBalls();
        //StartCoroutine("SpawnBalls");
    }

    void Update()
    {

    }


    private void SpawnBalls()
    {
        for (int i = 0; i < 24; i++)
        {
            Vector2 positionToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            var newBallObject = Instantiate(ball1, positionToSpawn, Quaternion.identity);

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.transform.parent = this.transform;
            newBall.id = i;
            newBall.power = 1;
            newBall.speed = 4.0f;
            newBall.ballCategory = BallCategory.red;


            var spriteRender = newBall.GetComponent<SpriteRenderer>();
            spriteRender.color = Color.red;

            ballGameObjects.Add(newBall);
        }


        for (int i = 0; i < 4; i++)
        {
            Vector2 positionToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            var newBallObject = Instantiate(ball3, positionToSpawn, Quaternion.identity);

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.transform.parent = this.transform;
            newBall.speed = 4.0f;
            newBall.id = i + 1000;
            newBall.power = 3;
            newBall.ballCategory = BallCategory.green;


            var spriteRender = newBall.GetComponent<SpriteRenderer>();
            spriteRender.color = Color.green;

            ballGameObjects.Add(newBall);
        }
    }
}

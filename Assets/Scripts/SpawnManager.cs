using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ballContainer;

    [SerializeField]
    private GameObject ball1;

    [SerializeField]
    private GameObject ball3;


    private List<GameObject> ballGameObjects = new List<GameObject>();


    void Start()
    {
        StartCoroutine("SpawnBalls");
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
            newBallObject.transform.parent = ballContainer.transform;

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.id = i;
            newBall.power = 1;
            newBall.speed = 4.0f;
            newBall.ballCategory = BallCategory.red;

            var render = newBallObject.GetComponent<SpriteRenderer>();
            render.color = Color.red;

            ballGameObjects.Add(newBallObject);
        }


        for (int i = 0; i < 4; i++)
        {
            Vector2 positionToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            var newBallObject = Instantiate(ball3, positionToSpawn, Quaternion.identity);
            newBallObject.transform.parent = ballContainer.transform;

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.id = i + 1000;
            newBall.power = 3;
            newBall.speed = 4.0f;; 
            newBall.ballCategory = BallCategory.green;

            var render = newBallObject.GetComponent<SpriteRenderer>();
            render.color = Color.green;

            ballGameObjects.Add(newBallObject);
        }
    }
}

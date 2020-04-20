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


    private const float cameraSize = 6.0f;
    private const float screenRatio = 16.0f / 9.0f;
    private float screenY = cameraSize;
    private float screenX = cameraSize * screenRatio;

    private List<GameObject> _ballGameObjects = new List<GameObject>();


    void Start()
    {
        StartCoroutine("SpawnBalls");
    }

    void Update()
    {

    }


    private void SpawnBalls()
    {
        for (int i = 0; i < 12; i++)
        {
            Vector2 positionToSpawn = new Vector2(Random.Range(-screenX, screenX), Random.Range(-screenY, screenY));
            var newBallObject = Instantiate(ball1, positionToSpawn, Quaternion.identity);
            newBallObject.transform.parent = ballContainer.transform;

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.id = i;
            newBall.power = 1;
            newBall.speed = 4.0f;
            newBall.ballCategory = BallCategory.red;

            var render = newBallObject.GetComponent<SpriteRenderer>();
            render.color = Color.red;

            _ballGameObjects.Add(newBallObject);
        }


        for (int i = 0; i < 4; i++)
        {
            Vector2 positionToSpawn = new Vector2(Random.Range(-screenX, screenX), Random.Range(-screenY, screenY));
            var newBallObject = Instantiate(ball3, positionToSpawn, Quaternion.identity);
            newBallObject.transform.parent = ballContainer.transform;

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.id = i + 1000;
            newBall.power = 3;
            newBall.speed = 4.0f;; 
            newBall.ballCategory = BallCategory.green;

            var render = newBallObject.GetComponent<SpriteRenderer>();
            render.color = Color.green;

            _ballGameObjects.Add(newBallObject);
        }
    }
}

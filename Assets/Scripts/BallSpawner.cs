using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private Sprite[] ballSprites;  //https://www.youtube.com/watch?v=LJrc1NHY23w

    [SerializeField]
    private Text leftText;

    [SerializeField]
    private Text rightText;

    private List<Ball> balls = new List<Ball>();

    private int redCount = 0;
    private int greenCount = 0;


    void Start()
    {
        SpawnBalls();
        //StartCoroutine("SpawnBalls");

        redCount = balls.Where(x => x.ballCategory == BallCategory.red).Count();
        greenCount = balls.Where(x => x.ballCategory == BallCategory.green).Count();
    }

    void Update()
    {
        CalculateScore();
    }


    private void SpawnBalls()
    {
        for (int i = 0; i < 24; i++)
        {
            Vector2 positionToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            var newBallObject = Instantiate(ballPrefab, positionToSpawn, Quaternion.identity);

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.transform.parent = this.transform;
            newBall.speed = 4.0f;
            newBall.id = i;
            newBall.power = 1;
            newBall.ballCategory = BallCategory.red;


            var spriteRender = newBall.GetComponent<SpriteRenderer>();
            spriteRender.sprite = ballSprites[0];
            spriteRender.color = Color.red;

            balls.Add(newBall);
        }


        for (int i = 0; i < 4; i++)
        {
            Vector2 positionToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            var newBallObject = Instantiate(ballPrefab, positionToSpawn, Quaternion.identity);

            var newBall = newBallObject.GetComponent<Ball>();
            newBall.transform.parent = this.transform;
            newBall.speed = 4.0f;
            newBall.id = i + 1000;
            newBall.power = 3;
            newBall.ballCategory = BallCategory.green;


            var spriteRender = newBall.GetComponent<SpriteRenderer>();
            spriteRender.sprite = ballSprites[1];
            spriteRender.color = Color.green;

            balls.Add(newBall);
        }
    }


    private void CalculateScore()
    {
        int redCurrent = balls.Where(x => x != null && x.ballCategory == BallCategory.red).Count();
        int greenCurrent = balls.Where(x => x != null && x.ballCategory == BallCategory.green).Count();

        if (redCurrent < redCount)
        {
            leftText.text = $"Red(Power 1): {redCurrent}";
            redCount = redCurrent;
        }

        if (greenCurrent < greenCount)
        {
            rightText.text = $"Green(Power 3): {greenCurrent}";
            greenCount = greenCurrent;
        }
    }
}

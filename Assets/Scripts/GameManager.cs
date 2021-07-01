using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public Transform startingPoint;
    public float lerpSpeed;
    public float score;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;

        // Set this to true just to disable all inputs
        playerControllerScript.isGameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        if (!playerControllerScript.isGameOver)
        {
            if (playerControllerScript.isDoubleSpeed)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            Debug.Log("Score: " + score);
        }
    }

    /*
        First we determine what the start and end positions are for our movement. 
        Next, we determine what the length of the journey is. 
        After that, we determine the distance we have covered so far, and what the fraction of the distance over the journey length is. 
        We then do that calculation within a while loop to move the player forwards.
        We can adjust the value of the lerpSpeed variable to determine how fast the movement happens.
        Within this method we are also adjusting the speed of the player’s animation using the Speed_Multiplier variable we created earlier.
        After the movement has been completed, we set the gameOver variable to false to allow the player to move.
    */
    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float jorneyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOrJorney = distanceCovered / jorneyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOrJorney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOrJorney = distanceCovered / jorneyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOrJorney);
            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScript.isGameOver = false;
    }
}

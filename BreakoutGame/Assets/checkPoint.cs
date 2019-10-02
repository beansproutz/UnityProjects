using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class checkPoint : MonoBehaviour
{
    // Variables 
    public int score, numScarlet, numGrey;
    public TextMeshPro scoreText, scarletNumText, greyNumText, winLoseText;
    public Rigidbody2D ball;
    public TextMeshPro winLostText;
    public bool inPlay;
    public Transform paddle;
    public float constantSpeed = 7;

    // For added audio hehe
    public AudioClip boopClip;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        ball.AddForce(new Vector2(1, 1));

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlay)
        {
            ball.velocity = constantSpeed * (ball.velocity.normalized);
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        
        GameObject obj = collisionInfo.gameObject;
        audioSource.PlayOneShot(boopClip, 0.3f);

        // If you get stuck in a horizontal bounce, pivot slightly to help player
        if (ball.velocity.x == 0)
            ball.AddForce(new Vector2(-3, 3));

        // If you get stuck in a vertical bounce, pivot slightly to help player
        if (ball.velocity.y == 0)
            ball.AddForce(new Vector2(3, 3));

        // If you get stuck in a horizontal bounce but against the right wall, pivot leftwards
        if (ball.velocity.y == 0 && obj.CompareTag("rightWall"))
            ball.AddForce(new Vector2(-3, -3));

        // If what ball collided with is a grey or scarlet block, then delete it and check for points
        if (obj != null && (obj.CompareTag("grey") || obj.CompareTag("scarlet")))
        {
            Destroy(obj);
            if (obj.CompareTag("grey"))
            {
                score++;
                numGrey--;
            }

            else
            {
                numScarlet--;
                if (numScarlet == (2 * numGrey))
                    score += 2;
                else
                    score++;
            }

            scoreText.text = "Score: " + score;
            scarletNumText.text = "Scarlet blocks remaining: " + numScarlet;
            greyNumText.text = "Grey blocks remaining: " + numGrey;
        }

        // You win when all blocks are cleared
        if (numScarlet == 0 && numGrey == 0)
        {
            winLoseText.text = "You win! :^)";
            inPlay = false;
        }

        // Lose if ball hits floor
        if (obj != null && obj.CompareTag("lost"))
        {
            winLoseText.text = "You lose :(";
            ball.velocity = Vector2.zero;
            inPlay = false;
        }

    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class moveBall : MonoBehaviour
{
    public int cylinderNum, cubeNum, totalScore;
    public float speed;
    public Text totalScoreView;

    // For added audio hehe
    public AudioClip pingClip;
    AudioSource audioSource;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        GameObject obj = collisionInfo.gameObject;
        audioSource.PlayOneShot(pingClip, 0.3f);
        obj.SetActive(false);

        if (obj != null && obj.CompareTag("cylinder"))
        {
            cylinderNum--;
            totalScore = totalScore + cubeNum;
        }

        else if (obj != null && obj.CompareTag("cube"))
        {
            cubeNum--;
            totalScore++;
        }
        setScoreText();
    }

    void setScoreText()
    {
        // Scenes: 0 (main menu), 1 (instructions), 2 (game scene), 3 (winner scene), 4 (loser scene)
        totalScoreView.text = "Score: " + totalScore;

        // Goto: 3 (winner scene)
        if (totalScore >= 25 && cylinderNum == 0 && cubeNum == 0)
        {
            SceneManager.LoadScene(3);
        }

        // Goto: 4 (loser scene)
        else if (totalScore < 25 && cylinderNum == 0 && cubeNum == 0)
        {
            SceneManager.LoadScene(4);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckPoints : MonoBehaviour
{
    public GameObject car;
    public int points, laps;
    AudioSource crashSource;
    public AudioClip crashClip;
    public Text totalScoreView;

    // Start is called before the first frame update
    void Start()
    {
        totalScoreView.text = "20";
        crashSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.fallDetection();
        this.scoreSystem();
    }

    
    void OnTriggerEnter(Collider collider)
    {
        GameObject obj = collider.gameObject;
        if (obj.CompareTag("offRoad"))
        {
            points--;
        }

        if (obj.CompareTag("flag"))
        {
            laps++;
        }

        totalScoreView.text = "" + points;
    }

    void fallDetection()
    {
        if (car.transform.position.y < -1)
        {
            crashSource.PlayOneShot(crashClip, 0.3f);
            car.transform.position = new Vector3(77.94f, 0.25f, 74.76f);
            car.transform.rotation = Quaternion.identity;
            
            SceneManager.LoadScene(3);
        }
    }

    // Scenes: 0 (main menu), 1 (game), 2 (win), 3 (lose)
    void scoreSystem()
    {
        if (points == 0)
        {
            SceneManager.LoadScene(3);
        }

        if (laps >= 3)
        {
            if (points > 15)
                SceneManager.LoadScene(2);
            else
                SceneManager.LoadScene(3);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatform : MonoBehaviour
{
    public float speed;
    public float lim = 16.6f;


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);
        if (transform.position.x < -lim)
        {
            transform.position = new Vector2(-lim, transform.position.y);
        }

        if (transform.position.x > lim)
        {
            transform.position = new Vector2(lim, transform.position.y);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D cdr)
    {
        if(cdr.tag == "Finish")
        {
            Destroy(gameObject);
        }

        if(cdr.tag == "Player")
        {
            Destroy(gameObject);
            plus();
        }
    }
    void plus()
    {
        BallMove.Instance.cin += 1;
    }
}

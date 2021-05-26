using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlateformMove : MonoBehaviour
{
    bool moving;
    int x;
    Transform tr;
    Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && BallMove.Instance.state == 1)
        {
            moving = true;
            target = Input.GetTouch(0).position;
            target = Camera.main.ScreenToWorldPoint(target);
        }
        else
            moving = false;
    }
    void FixedUpdate()
    {
        if(moving)
        {
            target = new Vector2(target.x, tr.position.y);
            tr.position = Vector2.MoveTowards(tr.position, target, Time.deltaTime * 10);
        }
    }
    void smooth(Vector2 point)
    {
        
    }
    void OnTriggerEnter2D(Collider2D cd)
    {
        if(cd.tag == "fifty")
        {
            cd.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(cd);
            pu50();
        }
        if (cd.tag == "hundred")
        {
            cd.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(cd);
            pu100();
        }
        if (cd.tag == "twofifty")
        {
            cd.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(cd);
            pu250();
        }
        if (cd.tag == "fivehundred")
        {
            cd.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(cd);
            pu500();
        }
        if (cd.tag == "big")
        {
            Destroy(cd);
            StartCoroutine(bigball());
        }
        if(cd.tag == "fast")
        {
            Destroy(cd);
            StartCoroutine(faster());
        }
        if(cd.tag == "fire")
        {
            Destroy(cd);
            StartCoroutine(BallMove.Instance.fire());
        }
    }
    void pu50()
    {
        BallMove.Instance.scr += 50;
        BallMove.Instance.score.text = BallMove.Instance.scr.ToString();
    }
    void pu100()
    {
        BallMove.Instance.scr += 100;
        BallMove.Instance.score.text = BallMove.Instance.scr.ToString();
    }
    void pu250()
    {
        BallMove.Instance.scr += 250;
        BallMove.Instance.score.text = BallMove.Instance.scr.ToString();
    }
    void pu500()
    {
        BallMove.Instance.scr += 500;
        BallMove.Instance.score.text = BallMove.Instance.scr.ToString();
    }
    IEnumerator bigball()
    {
        BallMove.Instance.gameObject.transform.localScale = new Vector3 (2,2,1);
        yield return new WaitForSeconds(3f);
        BallMove.Instance.gameObject.transform.localScale = new Vector3(1,1,1);
    }
    IEnumerator faster()
    {
        BallMove.Instance.gameObject.GetComponent<Rigidbody2D>().velocity *= new Vector2(1.6f,1.6f);
        yield return new WaitForSeconds(3f);
        BallMove.Instance.gameObject.GetComponent<Rigidbody2D>().velocity *= new Vector2(0.8f, 0.8f);
    }
}

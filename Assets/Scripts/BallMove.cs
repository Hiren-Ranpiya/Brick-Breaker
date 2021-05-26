using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallMove : singlor<BallMove>
{
    public GameObject line;
    public GameObject coinpre;
    public GameObject[] powers;

    [SerializeField] private Text coins;
    [SerializeField] private float speed;
    [SerializeField] private Canvas endCanvas;
    [SerializeField] private Canvas can;
    [SerializeField] private Text winorloose;
    [SerializeField] public Text score;
    [SerializeField] private Text fcoins;
    [SerializeField] private Text fscore;
    [SerializeField] private int totalBricks;
    [SerializeField] private Button next;

    int curlev;
    int life = 0;
    int broke = 0;
    float ang;
    Rigidbody2D rb;
    GameObject liner;

    public int scr;
    public int cin;
    public int state;
    // Start is called before the first frame update
    void Start()
    {
        curlev = SceneManager.GetActiveScene().buildIndex;
        state = 0;
        life = 0;
        endCanvas.enabled = false;
        scr = 0;
        cin = 0;
        broke = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(1);

        if (state == 0 && Input.touchCount == 1)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began) 
            {
                liner = Instantiate(line) as GameObject;
                liner.transform.position = new Vector2(transform.position.x,transform.position.y+0.2f);
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                var dir = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                ang = Mathf.Atan2(dir.y, dir.x);
                var angle = ang* Mathf.Rad2Deg -90f;
                liner.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended)
            { 
                rb.velocity = new Vector2(speed*Mathf.Cos(ang),speed*Mathf.Sin(ang));
                Destroy(liner);
                state = 1;
            }
        }

        coins.text = cin.ToString();
    }
    void coingenerator(Vector3 pos )
    {
        int j = Random.Range(1, 9);
        if (j == 1 || j == 3)
        {
            int k = Random.Range(1, 5);
            for (int i = 0; i < k; i++)
            {
                GameObject coin = Instantiate(coinpre) as GameObject;
                coin.transform.position = pos;
                float h = Random.Range(8, 20);
                h = h * 2 / 100;
                pos.x += h;
            }
        }
        if (j == 2)
        {
            int k = Random.Range(0,8);
            GameObject powerup = Instantiate(powers[k]) as GameObject;
            powerup.transform.position = pos;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "brick")
        {
            coingenerator(collision.collider.transform.position);
            Destroy(collision.gameObject);
            broke += 1;
            scr += 10;
            score.text = scr.ToString();
            if (broke >= totalBricks)
                result(true);
        }

        if(collision.collider.tag == "Finish")
        {
            life += 1;
            result(false);
        }
    }
    public IEnumerator fire()
    {
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(3f);
        GetComponent<Collider2D>().isTrigger = false;
    }
    void OnTriggerEnter2D(Collider2D cd)
    {
        if(cd.tag == "brick")
        {
            coingenerator(cd.transform.position);
            Destroy(cd.gameObject);
            broke += 1;
            scr += 10;
            score.text = scr.ToString();
            if (broke >= totalBricks)
                result(true);
        }
        if (cd.tag == "Finish")
        {
            life += 1;
            result(false);
        }
    }
    void result(bool win)
    {
        state = 2;
        Debug.Log("state change");
        rb.velocity = new Vector2(0, 0);
        endCanvas.enabled = true;
        if(win)
        {
            winorloose.text = "level complete";
        }
        else
        {
            winorloose.text = "level fail";
            next.enabled = false;
        }
        fscore.text = score.text;
        fcoins.text = coins.text;
      //  Time.timeScale = 0;
    }
    public void home()
    {
        SceneManager.LoadScene(0);
    }
    public void nextlev()
    {
        SceneManager.LoadScene(curlev + 1);
    }
    public void replay()
    {
        SceneManager.LoadScene(curlev);
    }
}

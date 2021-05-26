using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int x;
    // Start is called before the first frame update
    void Start()
    {
        x = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(x - 1);
        }
    }
    
    public void back()
    {
        SceneManager.LoadScene(0);
    }

    public void levelopener(int lev)
    {
        PlayerPrefs.SetInt("curlev", lev + 1);
        SceneManager.LoadScene(lev+1);
    }
}

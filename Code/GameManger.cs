using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    public bool isGameover = false;
    public Text timetext;
    public GameObject gameover;
    float set = 0;
    public bool pause = false;

    public int time = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameover && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        addTime();
        if (Input.GetKeyDown(KeyCode.C) && pause)
        {
            Time.timeScale = 1;
            Debug.Log("restart");
            pause = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && !pause)
        {
            Time.timeScale = 0;
            Debug.Log("pause");
            pause = true;
        }
    }

    public void addTime()
    {
        if (!isGameover)
        {
            set += Time.deltaTime;
            timetext.text = "TIME : " + (int)set;
        }
    }
    public void Dead()
    {
        isGameover = true;
        gameover.SetActive(true);
    }
}

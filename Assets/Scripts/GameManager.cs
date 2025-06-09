using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float playTime;
    public float maxPlayTime = 2 * 10f; // num * min
    public PlayerController pc;
    public static GameManager instance;
    public ObjPooling pools;
    public float hp;
    public float maxHP = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    public LevelManager levelManager;
    public bool isStop = true;
    public Result result;
    public GameObject clear;

    private void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        hp = maxHP;

        levelManager.Select(0);
        Resume();

        AudioManager.instance.PlayBGM(true);
        AudioManager.instance.PlaySFX(AudioManager.SFX.SELECT);
    }

    public IEnumerator Delay()
    {
        isStop = true;
        yield return new WaitForSeconds(1f);
        result.gameObject.SetActive(true);
        result.Lose();
        Stop();
        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.LOSE);

    }
    public void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public IEnumerator WinDelay()
    {
        isStop = true;
        clear.SetActive(true );
        yield return new WaitForSeconds(1f);
        result.gameObject.SetActive(true);
        result.Win();
        Stop();
        AudioManager.instance.PlayBGM(false);
        AudioManager.instance.PlaySFX(AudioManager.SFX.WIN);

    }
    public void GameWin()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (isStop)
        {
            return;
        }
        playTime += Time.deltaTime;

        if(playTime > maxPlayTime )
        {
            playTime = maxPlayTime;
            StartCoroutine(WinDelay());
        }
    }

    public void GetExp()
    {
        if (isStop) return;
        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            levelManager.Show();
        }
    }

    public void Stop()
    {
        isStop = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isStop=false;
        Time.timeScale = 1f;
    }
}

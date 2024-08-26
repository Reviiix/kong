using System;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]

public class RestartTimer : MonoBehaviour
{
    private TMP_Text counter;
    private static readonly WaitForSeconds WaitOneSecond = new (1);
    
    private void Awake()
    {
        counter = GetComponent<TMP_Text>();
        counter.enabled = false;
    }

    public void Begin(int seconds, Action complete)
    {
        StartCoroutine(CountDown(seconds, complete));
    }

    private IEnumerator CountDown(int seconds, Action complete)
    {
        const string go = "go";
        
        yield return WaitOneSecond;

        counter.text = seconds.ToString();
        counter.enabled = true;

        for (var i = seconds; i > 0; i--)
        {
            counter.text = i.ToString();
            yield return WaitOneSecond;
        }
        counter.text = go;
        yield return WaitOneSecond;
        
        counter.enabled = false;
        complete();
    }

    public void GameOver()
    {
        const string gameOver = "GAME OVER";
        counter.text = gameOver;
        counter.enabled = true;
    }
}

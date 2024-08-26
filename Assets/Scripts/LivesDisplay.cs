using System;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour
{
    [SerializeField] private Image[] lives;
    private int livesLenght;

    private void Awake()
    {
        livesLenght = lives.Length;
    }
    
    private void OnEnable()
    {
        PlayerLives.OnLivesChange += OnLiveChange;
    }
    
    private void OnDisable()
    {
        PlayerLives.OnLivesChange -= OnLiveChange;
    }

    private void OnLiveChange(int livesRemaining)
    {
        for (var i = 0; i < livesLenght; i++)
        {
            lives[i].enabled = i <= livesRemaining-1;
        }
    }
}

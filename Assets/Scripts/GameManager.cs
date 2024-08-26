using Player;
using PureFunctions.UnitySpecific;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    [FormerlySerializedAs("restartCounter")] [SerializeField] private RestartTimer restartTimer;
    [SerializeField] private Transform spawnPoint;
    private const int RestartTime = 3;
    
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        PlayerManager.Instance.EnableMovement();
    }
    
    public void EndRound(int lives)
    {
        if (lives > 0)
        {
            RestartGame(RestartTime);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        restartTimer.GameOver();
    }

    private void RestartGame(int restartTime)
    {
        restartTimer.Begin(restartTime, () =>
        {
            PlayerManager.Instance.Respawn(spawnPoint.localPosition);
        });
    }
}

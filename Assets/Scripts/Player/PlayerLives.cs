using System;

namespace Player
{
    [Serializable]
    public class PlayerLives
    {
        public static Action<int> OnLivesChange;
        private const int MaximumLives = 3;
        private int lives = MaximumLives;
        public int Lives
        {
            get => lives;
            private set
            {
                lives = value switch
                {
                    > MaximumLives => MaximumLives,
                    < 0 => 0,
                    _ => lives
                };

                lives = value;
                OnLivesChange?.Invoke(lives);
            }
        }

        public void RemoveLife(Action<int> livesRemainingCallBack = null)
        {
            Lives--;
            livesRemainingCallBack?.Invoke(Lives);
        }
        
        public void AddLife(Action<int> livesRemainingCallBack = null)
        {
            Lives++;
            livesRemainingCallBack?.Invoke(Lives);
        }
    }
}

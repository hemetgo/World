using System;

public class GameEvents
{
    public class Game
    {
		public static Action GameOver;
		public static Action Victory;
	}

	public class Player
    {
        public static Action<Health> OnHealthChanged;
        public static Action OnTakeDamage;
    }

    public class Enemy
    {
        public static Action<EnemyController> OnEnemyDie;

        public static Action OnAllEnemiesDie;
        public static Action<WavePhase> OnWaveChangePhase;
    }
}

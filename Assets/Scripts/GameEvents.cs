using System;

public class GameEvents
{
    public class Player
    {
        public static Action<Health> OnHealthChanged;
    }

    public class Enemy
    {
        public static Action<EnemyController> OnEnemyDie;
        public static Action OnWaveStart;
        public static Action OnAllEnemiesDie;
    }
}

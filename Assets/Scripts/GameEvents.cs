using System;

public class GameEvents
{
    public class Player
    {
        public static Action<Health> OnHealthChanged;
    }
}

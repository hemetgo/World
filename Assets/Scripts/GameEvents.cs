using System;

public class GameEvents
{
    public class Game
    {
		public static Action GameOver;
		public static Action Victory;
	}

    public class Item
    {
        public static Action<bool> OnOverlapDrop;
        public static Action OnTryCatchItem;
    }

	public class Player
    {
        public static Action<Health> OnHealthChanged;
        public static Action OnTakeDamage;

        public static Action<HandWeapon> OnFire;
        public static Action<HandWeapon> OnChangeWeapon;

        public static Action<HandWeapon> OnRechargingStart;
        public static Action<HandWeapon> OnRechargingStop;
        public static Action<HandWeapon> OnRechargingComplete;
    }

    public class Enemy
    {
        public static Action<EnemyController> OnEnemyDie;

        public static Action OnAllEnemiesDie;
        public static Action<WavePhase> OnWaveChangePhase;
    }

    public class UI
    {
        public static Action<ItemSettings, ItemData> OnOpenItemDetails;
    }
}

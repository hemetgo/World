using System;
using System.Numerics;

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

        public static Action<HandItem> EquipItem;

        public static Action<HandFireWeapon> OnFire;
        public static Action<HandFireWeapon> OnAmmoUpdate;

        public static Action<HandFireWeapon> OnNoAmmo;
        public static Action<HandFireWeapon> StartReloading;
        public static Action<HandFireWeapon> StopReloading;
        public static Action<HandFireWeapon> CompleteReloading;
    }

    public class Inputs
    {
        public static Action<InputState> OnReload;
        public static Action<InputState> OnFire;
        public static Action OnScrollUp;
        public static Action OnScrollDown;
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

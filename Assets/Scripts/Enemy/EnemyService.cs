using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyService 
{
	public static List<EnemyController> Enemies { get; set; } = new List<EnemyController>();

	public static bool HaveEnemies => Enemies.Count > 0;

    public static void RegisterEnemy(EnemyController enemy)
    { 
        Enemies.Add(enemy); 
    }

    public static void UnregisterEnemy(EnemyController enemy)
    {
		if (!Enemies.Contains(enemy))
			return;

        Enemies.Remove(enemy);

		GameEvents.Enemy.OnEnemyDie?.Invoke(enemy);

		if (Enemies.Count == 0)
		{
			GameEvents.Enemy.OnAllEnemiesDie?.Invoke();
		}
    }

	public static EnemyController FindClosestEnemy(Vector3 position)
	{
		if (Enemies.Count == 0)
		{
			return null;
		}

		EnemyController closestEnemy = Enemies
			.OrderBy(enemy => Vector3.Distance(position, enemy.transform.position))
			.FirstOrDefault();

		return closestEnemy;
	}

	public static List<EnemyController> FindEnemiesOnRange(Vector3 position, float range)
	{
		List<EnemyController> result = new List<EnemyController>();

		foreach(EnemyController enemy in Enemies)
		{
			if (Vector3.Distance(enemy.transform.position, position) < range)
			{
				result.Add(enemy);
			}
		}

		return result;
	}

}
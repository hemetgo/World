using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyService 
{
    public static List<Enemy> Enemies = new List<Enemy>();

	public static bool HaveEnemies => Enemies.Count > 0;

    public static void RegisterEnemy(Enemy enemy)
    { 
        Enemies.Add(enemy); 
    }

    public static void UnregisterEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
    }

	public static Enemy FindClosestEnemy(Vector3 position)
	{
		if (Enemies.Count == 0)
		{
			return null;
		}

		Enemy closestEnemy = Enemies
			.OrderBy(enemy => Vector3.Distance(position, enemy.transform.position))
			.FirstOrDefault();

		return closestEnemy;
	}

}
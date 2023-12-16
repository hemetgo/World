using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage;

    public void Setup(Vector3 spawnPoint, Vector3 targetPosition, int damage, float moveSpeed)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        transform.position = spawnPoint;
        transform.LookAt(targetPosition);
        rigidbody.velocity = transform.forward * moveSpeed;

        _damage = damage;
    }

    private void OnHit()
    {
        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.Health.TakeDamage(_damage);
            OnHit();
		}
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
			OnHit();
		}
	}
}

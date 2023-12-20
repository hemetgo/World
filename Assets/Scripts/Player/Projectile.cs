using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] GameObject _hitVFX;

    public void Setup(Vector3 spawnPoint, Vector3 targetPosition, int damage, float moveSpeed)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        transform.position = spawnPoint;
        transform.LookAt(targetPosition);
        rigidbody.velocity = transform.forward * moveSpeed;

        _damage = damage;
    }

    private void OnHit(GameObject vfx)
    {
        Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out EnemyController enemy))
        {
            enemy.Health.TakeDamage(_damage);
            OnHit(enemy.Health.BloodVFX);
		}
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
			OnHit(_hitVFX);
		}
	}
}

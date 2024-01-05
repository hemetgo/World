using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] FireWeaponSettings _fireWeaponSettings;
    [SerializeField] GameObject _hitVFX;

    public void Setup(Vector3 spawnPoint, Vector3 direction, FireWeaponSettings weaponSettings)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

		transform.position = spawnPoint;
        transform.LookAt(transform.position + direction);
        rigidbody.velocity = transform.forward * weaponSettings.ProjectileSpeed;

		_fireWeaponSettings = weaponSettings;
    }

	private void OnHit()
    {
		Destroy(gameObject);
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
            enemy.Health.TakeDamage(_fireWeaponSettings.ResultDamage);
            OnHit();
		}
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
			OnHit(_hitVFX);
		}
	}
}

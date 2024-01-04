using UnityEngine;
using TMPro;

public class DamageFeedback : MonoBehaviour
{
	[SerializeField] Color _defaultColor;
	[SerializeField] Color _criticalColor;

	public float upwardForce = 5f; 
	public float sidewaysForce = 5f; 

	void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();
		rb.velocity = new Vector3(Random.Range(-sidewaysForce, sidewaysForce), upwardForce, 0f);
		transform.eulerAngles = new Vector3(90, 0, 0);
	}

	public void Setup(ResultDamage damage)
    {
		TextMeshPro textMesh = GetComponent<TextMeshPro>();
		textMesh.text = damage.Value.ToString();
		textMesh.color = _defaultColor;

		if (damage.IsCritical)
		{
			textMesh.color = _criticalColor;
			textMesh.fontSize *= 1.5f;
		}
    }
}

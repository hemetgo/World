using UnityEngine;

public class InputHelper : MonoBehaviour
{
    public static Vector2 MovementInput => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    public static Vector3 MouseWorldPosition
    {
        get
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;

			// Faça um Raycast e verifique se colidiu com algo
			if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Ground")))
			{
				// Obtenha a posição do ponto de colisão no espaço do mundo
				Vector3 worldMousePosition = hit.point;
				return worldMousePosition;
			}

            return Vector3.zero;
		}
    }

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
			GameEvents.Inputs.OnFire?.Invoke(true);
		if (Input.GetButtonUp("Fire1"))
			GameEvents.Inputs.OnFire?.Invoke(false);

		if (Input.GetButtonDown("Reload"))
			GameEvents.Inputs.OnReload?.Invoke(true);
		if (Input.GetButtonUp("Reload"))
			GameEvents.Inputs.OnReload?.Invoke(false);

		if (Input.mouseScrollDelta.y > 0)
			GameEvents.Inputs.OnScrollUp?.Invoke();
		if (Input.mouseScrollDelta.y < 0)
			GameEvents.Inputs.OnScrollDown?.Invoke();
	}

	public static Vector3 GetRelativeMouseWorldPosition(Transform transform)
    {
        Vector3 result = MouseWorldPosition;
        result.y = transform.position.y;
        return result;
    }
}

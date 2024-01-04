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
		if (Input.GetButton("Fire1"))
			GameEvents.Inputs.OnFire?.Invoke(InputState.Holding);
		if (Input.GetButtonUp("Fire1"))
			GameEvents.Inputs.OnFire?.Invoke(InputState.Up);

		if (Input.GetButtonDown("Reload"))
			GameEvents.Inputs.OnReload?.Invoke(InputState.Down);
		if (Input.GetButtonUp("Reload"))
			GameEvents.Inputs.OnReload?.Invoke(InputState.Up);

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

public enum InputState
{
	Down, Holding, Up
}
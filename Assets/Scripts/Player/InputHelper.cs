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

			// Fa�a um Raycast e verifique se colidiu com algo
			if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Ground")))
			{
				// Obtenha a posi��o do ponto de colis�o no espa�o do mundo
				Vector3 worldMousePosition = hit.point;
				return worldMousePosition;
			}

            return Vector3.zero;
		}
    }

	private void OnEnable()
	{
		GameEvents.Game.Pause += OnPause;
	}

	private void OnDisable()
	{
		GameEvents.Game.Pause -= OnPause;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Pause"))
			GameEvents.Inputs.OnPause?.Invoke(InputState.Down);
		if (Input.GetButtonUp("Pause"))
			GameEvents.Inputs.OnPause?.Invoke(InputState.Up);

		if (!PauseManager.IsPaused)
		{
			if (Input.GetButton("Fire1"))
				GameEvents.Inputs.OnFire?.Invoke(InputState.Holding);
			if (Input.GetButtonUp("Fire1"))
				GameEvents.Inputs.OnFire?.Invoke(InputState.Up);

			if (Input.GetButtonDown("Reload"))
				GameEvents.Inputs.OnReload?.Invoke(InputState.Down);
			if (Input.GetButtonUp("Reload"))
				GameEvents.Inputs.OnReload?.Invoke(InputState.Up);

			if (Input.GetButtonDown("Interact"))
				GameEvents.Inputs.OnInteract?.Invoke(InputState.Down);
			if (Input.GetButtonUp("Interact"))
				GameEvents.Inputs.OnInteract?.Invoke(InputState.Up);

			if (Input.mouseScrollDelta.y > 0)
				GameEvents.Inputs.OnScrollUp?.Invoke();
			if (Input.mouseScrollDelta.y < 0)
				GameEvents.Inputs.OnScrollDown?.Invoke();
		}
	}

	public static void OnPause(bool pause)
	{
		GameEvents.Inputs.OnFire?.Invoke(InputState.Up);
		GameEvents.Inputs.OnReload?.Invoke(InputState.Up);
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
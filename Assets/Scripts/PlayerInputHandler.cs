using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	// General components needed
	[SerializeField] private Camera MainCamera; // Needed to convert the position of the mouse relative to the camera, to world coordinates
	[SerializeField] private PlayerInput PlayerInputComponent; // Component made by the "new input system"

	// Movement
	[SerializeField] public Vector2 Movement;

	// Aiming
	public Vector2 AimCoord; // Stores the world location where the mouse or controller is pointing to
	public Vector2 AimVector;// {get; private set;} // Stores the vector that points to where we are aiming
	public float AimAngle; //{get; private set;} // We convert the AimVector to location and we put the angle where we are aiming here in degrees


	private void Start()
	{
		
	}


	public void OnLook(InputAction.CallbackContext context)
	{
		AimCoord = MainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
		AimVector = AimCoord - (Vector2)transform.position;
		AimAngle = Mathf.Atan2(AimVector.y, AimVector.x) * Mathf.Rad2Deg;
	}


	public void OnMove(InputAction.CallbackContext context)
	{
		Movement = context.ReadValue<Vector2>();
	}


	public void OnFire(InputAction.CallbackContext context)
	{
		Debug.Log("Fire!");
	}


	public void OnSecondary(InputAction.CallbackContext context)
	{
        Debug.Log("Secondary!");
	}

	public void OnControlsChanged()
	{
		// Nothing
	}
}

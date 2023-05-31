using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
	// General components needed
	[SerializeField] private Camera MainCamera; // Needed to convert the position of the mouse relative to the camera, to world coordinates
	[SerializeField] private PlayerInput PlayerInputComponent; // Component made by the "new input system"
	
	private Rigidbody2D Rigid;
	private PlayerData Data;

	// Movement
	[SerializeField] public Vector2 Movement;

	// Aiming
	public Vector2 AimCoord; // Stores the world location where the mouse or controller is pointing to
	public Vector2 AimVector;// Stores the vector that points to where we are aiming
	public float AimAngle;  // We convert the AimVector to location and we put the angle where we are aiming here in degrees
	// private Quaternion rotation;

	
	[SerializeField] private GameObject Cursor;
	private Transform CursorTransform;
	[SerializeField] private GameObject RealCursor;
	
	[SerializeField] private float cursorLimitX = 2f;
	[SerializeField] private float cursorLimitY = 2f;
	

	// Control flow
	public bool canMove = true;
	public bool canLook = true;
	public bool canAttack = true;
	
	public bool isDashing = false;
	
	public bool CursorLimit = true;


	// Combat
	private Vector3 WeaponVector;
	[SerializeField] private LayerMask EnemyLayer;
	
	Vector2 enemyToPlayerVector;
	float dotProduct;
	float absoluteMagnitude;
	float angleBetween;
	float aimRange;
	float angleInterval;
	float currentAngle;
	Collider2D[] enemyColliders;
	
	public float MeleeRange = 90f;
	public float MeleeReach = 2f;
	public float AttackCooldown = 0.5f;

	// Debug
	bool drawMeleeGizmos = false;

	// Damage Calculation
	private ChargeType Dimension;
	private ChargeType EnemyType;

	public AudioManager AM;

	private void Start()
	{
		Rigid = GetComponent<Rigidbody2D>();
		Data = GetComponent<PlayerData>();
		CursorTransform = Cursor.GetComponent<Transform>();
	}
	
	private void Update()
	{

	}

	private void FixedUpdate()
	{
		// Movement		
		if (canMove)
		{
			Vector2 targetSpeed = new Vector2(Movement.x * Data.MoveSpeed, Movement.y * Data.MoveSpeed);
			Vector2 speedDif = targetSpeed - Rigid.velocity;
			Vector2 actualSpeed = speedDif * new Vector2(12, 12); // Change the vector values to change acceleration
			Rigid.AddForce(actualSpeed);
		}
		else
		{
			Rigid.velocity = Vector2.zero;
		}


	}


	public void OnLook(InputAction.CallbackContext context)
	{
		if(canLook)
		{
			AimCoord = MainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
			AimVector = AimCoord - (Vector2)transform.position;
			AimAngle = Mathf.Atan2(AimVector.y, AimVector.x) * Mathf.Rad2Deg;

			// Weapon Rotation
			// rotation = Quaternion.AngleAxis(AimAngle, Vector3.forward);
			// WeaponObject.transform.rotation = rotation;

			// Cursor
			if(CursorLimit)
			{
				if (AimVector.x < -cursorLimitX || AimVector.x > cursorLimitX)
				{
					if (AimVector.y < -cursorLimitY || AimVector.y > cursorLimitY)
					{
						CursorTransform.localPosition = new Vector2(CursorTransform.localPosition.x, CursorTransform.localPosition.y);
					}
					else
					{
						CursorTransform.localPosition = new Vector2(CursorTransform.localPosition.x, AimVector.y);
					}
				}
				else
				{
					if (AimVector.y < -cursorLimitY || AimVector.y > cursorLimitY)
					{
						CursorTransform.localPosition = new Vector2(AimVector.x, CursorTransform.localPosition.y);
					}
					else
					{
						CursorTransform.localPosition = new Vector2(AimVector.x, AimVector.y);
					}
				}	
			}
			else
			{
				CursorTransform.localPosition = AimVector;
			}
		}
		
		
	}


	public void OnMove(InputAction.CallbackContext context)
	{
		Movement = context.ReadValue<Vector2>();
	}


	public void OnFire(InputAction.CallbackContext context)
	{
		if(canAttack && context.started && canMove)
		{
			// Debug
			Debug.Log("BFA: Melee");
			drawMeleeGizmos = true;

			WeaponVector = transform.position;

			// We get all the enemyies we have hit
			enemyColliders = Physics2D.OverlapCircleAll(WeaponVector, MeleeReach, EnemyLayer);

			foreach (Collider2D enemy in enemyColliders)
			{
				Debug.Log("BFA: Enemy " + enemy.name);

				enemyToPlayerVector = enemy.transform.position - WeaponVector; // Enemy - Player

				dotProduct = Vector2.Dot(AimVector, enemyToPlayerVector);
				absoluteMagnitude = (Mathf.Sqrt(Mathf.Pow(AimVector.x, 2) + Mathf.Pow(AimVector.y, 2)) * Mathf.Sqrt(Mathf.Pow(enemyToPlayerVector.x, 2) + Mathf.Pow(enemyToPlayerVector.y, 2)));
				angleBetween = Mathf.Acos(dotProduct / absoluteMagnitude) * Mathf.Rad2Deg;

				// The reason I'm diving by 2 is because I'm thinking of MeleeRange as an FOV so if FOV is 30 they enemy can only be 15 degrees from the aim center.
				if (angleBetween < MeleeRange / 2)
				{
					// Do damage
					Damage(enemy);
				}
				Debug.Log("BFA: Angle between enemy and player aim is " + angleBetween);
			}

			Data.Anim.SetTrigger("Attack");			
			StartCoroutine(Cooldown());
		}
		else if (!canAttack && context.started && !canMove && Data.isDead)

		{

			Time.timeScale = 0;

			//Scene scene = SceneManager.GetActiveScene(); 

			SceneManager.LoadScene("Main");
			AM.Stop("HuntingYourMom");
			AM.Play("Theme_Test");
            AM.updateMixerVolume();

        }
	}

	public void OnSecondary(InputAction.CallbackContext context)
	{
		
		if (canAttack && context.started && CanWeSecondary() && canMove)
		{
			// Debug
			Debug.Log("BFA: Melee");
			drawMeleeGizmos = true;

			WeaponVector = transform.position;

			// We get all the enemyies we have hit
			enemyColliders = Physics2D.OverlapCircleAll(WeaponVector, MeleeReach, EnemyLayer);

			foreach (Collider2D enemy in enemyColliders)
			{
				Debug.Log("BFA: Enemy " + enemy.name);

				enemyToPlayerVector = enemy.transform.position - WeaponVector; // Enemy - Player

				dotProduct = Vector2.Dot(AimVector, enemyToPlayerVector);
				absoluteMagnitude = (Mathf.Sqrt(Mathf.Pow(AimVector.x, 2) + Mathf.Pow(AimVector.y, 2)) * Mathf.Sqrt(Mathf.Pow(enemyToPlayerVector.x, 2) + Mathf.Pow(enemyToPlayerVector.y, 2)));
				angleBetween = Mathf.Acos(dotProduct / absoluteMagnitude) * Mathf.Rad2Deg;

				// The reason I'm diving by 2 is because I'm thinking of MeleeRange as an FOV so if FOV is 30 they enemy can only be 15 degrees from the aim center.
				if (angleBetween < MeleeRange / 2)
				{
					// Do damage
					enemy.GetComponent<NPCStateMachine>().Hurt(Data.ChargedAttack);
				}
				Debug.Log("BFA: Angle between enemy and player aim is " + angleBetween);
			}

			Data.Anim.SetTrigger("Attack");
			Data.RemoveCharge(Data.SelectedCharge);
			StartCoroutine(Cooldown());
		}
	}
	
	private void Damage(Collider2D enemy)
	{
		Debug.LogWarning("Damage");
		NPCStateMachine E = enemy.GetComponent<NPCStateMachine>();
		// Dimension =  Data.WeatherT.GetCurrentWeather();
		Dimension = ChargeType.FIRE;
		if (Dimension == ChargeType.FIRE)
		{
			if(enemy.tag == "Fire")
			{
				E.Hurt(Data.WeakAttack);
			}
			else if (enemy.tag == "Ice")
			{
				E.Hurt(Data.CorrectAttack);
			}
			else if (enemy.tag == "Electric")
			{
				E.Hurt(Data.NormalAttack);
			}
			else
			{
				Debug.LogError("DamageCalculation: Something went very wrong");
			}
		}
		else if (Dimension == ChargeType.ICE)
		{
			if (enemy.tag == "Fire")
			{
				E.Hurt(Data.CorrectAttack);
			}
			else if (enemy.tag == "Ice")
			{
				E.Hurt(Data.WeakAttack);
			}
			else if (enemy.tag == "Electric")
			{
				E.Hurt(Data.NormalAttack);
			}
			else
			{
				Debug.LogError("DamageCalculation: Something went very wrong");
			}
		}
		else if (Dimension == ChargeType.ELECTRIC)
		{
			if (enemy.tag == "Fire")
			{
				E.Hurt(Data.NormalAttack);
			}
			else if (enemy.tag == "Ice")
			{
				E.Hurt(Data.CorrectAttack);
			}
			else if (enemy.tag == "Electric")
			{
				E.Hurt(Data.WeakAttack);
			}
			else
			{
				Debug.LogError("DamageCalculation: Something went very wrong");
			}
		}
		else
		{
			Debug.LogError("DamageCalculation: Something went very wrong");
		}
	}
	
	private bool CanWeSecondary()
	{
		if(Data.SelectedCharge == ChargeType.FIRE)
		{
			Debug.Log("CanWeSecondary: " + Data.FireCharge);
			return Data.FireCharge == 0 ? false : true;
		}
		else if (Data.SelectedCharge == ChargeType.ICE)
		{
			Debug.Log("CanWeSecondary: " + Data.IceCharge);
			return Data.IceCharge == 0 ? false : true;
		}
		else if (Data.SelectedCharge == ChargeType.ELECTRIC)
		{
			Debug.Log("CanWeSecondary: " + Data.ElectricCharge);
			return Data.ElectricCharge == 0 ? false : true;
		}
		else
		{
			Debug.LogError("CanWeSecondary");
			return false;
		}
	}
	
	public void OnSpace(InputAction.CallbackContext context)
	{
		if(context.started && canMove)
		{
			Data.JumpDimension();
		}
	}
	
	public void OnWheel(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			if (context.ReadValue<Vector2>().y == 120f)
			{
				Data.CycleChargeType(1);
				Debug.Log("SelectedCharge: " + Data.SelectedCharge);
			}
			else if (context.ReadValue<Vector2>().y == -120f)
			{
				Data.CycleChargeType(-1);
				Debug.Log("SelectedCharge: " + Data.SelectedCharge);
			}
		}
	}

	public void OnControlsChanged()
	{
		// Nothing, just for the New Input System
	}

	private void OnDrawGizmos()
	{
		if (drawMeleeGizmos)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(transform.position, AimVector); // Draws line from weapon to mouse cursor
			Gizmos.DrawWireSphere(transform.position, MeleeReach); // Draws the reach of the melee weapon
		}
	}

	private IEnumerator Cooldown()
	{
		canAttack = false;
		yield return new WaitForSeconds(AttackCooldown);
		canAttack = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public int Health = 20;
	public int WeakAttack = 4;
	public int NormalAttack = 8;
	public int CorrectAttack = 10;
	
	public int ChargedAttack = 20;
	
	public float MoveSpeed = 4.5f;
	
	public const int MAX_CHARGES = 3;
	public ChargeType SelectedCharge = ChargeType.FIRE;
	
	public int FireCharge = 0;
	public int IceCharge = 0;
	public int ElectricCharge = 0;
	

	private SpriteRenderer Sprite;
	private Rigidbody2D Rigid;
	private Animator Anim;

	private PlayerInputHandler InputH;
	[SerializeField] private GameObject WeaponObject;
	
	// [SerializeField] private GameObject GameManager;
	// private WeatherTracker WeatherT;


	private SpriteRenderer WeaponSprite;
	
	private ChargeType Dimension; 

	private void Start()
	{
		Sprite = GetComponent<SpriteRenderer>();
		Rigid = GetComponent<Rigidbody2D>();
		// Anim = GetComponent<Animator>();
		InputH = GetComponent<PlayerInputHandler>();
		WeaponSprite = WeaponObject.GetComponent<SpriteRenderer>();
		
		// WeatherT = GameManager.GetComponent<WeatherTracker>();
	}

	private void FixedUpdate()
	{
		SpriteFlipCheck();
		// MovementAnimationCheck();
	}

	private void SpriteFlipCheck()
	{
		if (InputH.AimVector.x < 0)
		{
			Sprite.flipX = false;
			WeaponSprite.flipX = false;
			// WeaponSprite.flipY = true;
		}
		else if (InputH.AimVector.x > 0)
		{
			Sprite.flipX = true;
			WeaponSprite.flipX = true;
			// WeaponSprite.flipY = false;

		}
	}


	// private void MovementAnimationCheck()
	// {
	//     if (InputH.Movement == Vector2.zero)
	//     {
	//         Anim.SetBool("IsMoving", false);
	//     }
	//     else if ((InputH.Movement.y != 0 && Rigid.velocity.y == 0) && (Mathf.Abs(Rigid.velocity.x) != 0f))
	//     {
	//         Anim.SetBool("IsMoving", true);
	//     }
	//     else if ((InputH.Movement.y != 0 && Rigid.velocity.y == 0) && (Mathf.Abs(Rigid.velocity.x) == 0f))
	//     {
	//         Anim.SetBool("IsMoving", false);
	//     }
	//     else if ((InputH.Movement.x != 0 && Rigid.velocity.x == 0) && (Mathf.Abs(Rigid.velocity.y) != 0f))
	//     {
	//         Anim.SetBool("IsMoving", true);
	//     }
	//     else if ((InputH.Movement.x != 0 && Rigid.velocity.x == 0) && (Mathf.Abs(Rigid.velocity.y) == 0f))
	//     {
	//         Anim.SetBool("IsMoving", false);
	//     }
	//     else
	//     {
	//         Anim.SetBool("IsMoving", true);
	//     }
	// }

	public void Hurt(ChargeType chargeType, int weakDamage, int normalDamage, int strongDamage)
	{
		Debug.LogError("Damage");
		// Dimension =  Data.WeatherT.GetCurrentWeather();
		Dimension = ChargeType.FIRE;
		if (Dimension == ChargeType.FIRE)
		{
			if (chargeType == ChargeType.FIRE)
			{
				TakeDamage(strongDamage);
			}
			else if (chargeType == ChargeType.ICE)
			{
                TakeDamage(weakDamage);
			}
			else if (chargeType == ChargeType.ELECTRIC)
			{
                TakeDamage(normalDamage);
			}
			else
			{
				Debug.LogError("DamageCalculation: Something went very wrong");
			}
		}
		else if (Dimension == ChargeType.ICE)
		{
			if (chargeType == ChargeType.FIRE)
			{
                TakeDamage(weakDamage);
			}
			else if (chargeType == ChargeType.ICE)
			{
                TakeDamage(strongDamage);
			}
			else if (chargeType == ChargeType.ELECTRIC)
			{
                TakeDamage(normalDamage);
			}
			else
			{
				Debug.LogError("DamageCalculation: Something went very wrong");
			}
		}
		else if (Dimension == ChargeType.ELECTRIC)
		{
			if (chargeType == ChargeType.FIRE)
			{
                TakeDamage(normalDamage);
			}
			else if (chargeType == ChargeType.ICE)
			{
                TakeDamage(weakDamage);
			}
			else if (chargeType == ChargeType.ELECTRIC)
			{
                TakeDamage(strongDamage);
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
	
	private void TakeDamage(int damage)
	{
		Health -= damage;
		CheckDeath();
	}
	
	private void CheckDeath()
	{
		if(Health <= 0)
		{
			Debug.LogWarning("Player is Dead");

			InputH.canMove = false;
			InputH.canLook = false;
			InputH.canAttack = false;
			InputH.CursorLimit = false;
		}
	}
	
	public void AddCharge(ChargeType type)
	{
		if(type == ChargeType.FIRE)
		{
			if(FireCharge != MAX_CHARGES)
			{
				FireCharge++;
			}
		}
		else if (type == ChargeType.ICE)
		{
			if (IceCharge != MAX_CHARGES)
			{
				IceCharge++;
			}
		}
		else if (type == ChargeType.ELECTRIC)
		{
			if (ElectricCharge != MAX_CHARGES)
			{
				ElectricCharge++;
			}
		}
	}
	
	public void RemoveCharge(ChargeType type)
	{
		if (type == ChargeType.FIRE)
		{
			if (FireCharge > 0)
			{
				FireCharge--;
			}
		}
		else if (type == ChargeType.ICE)
		{
			if (IceCharge > 0)
			{
				IceCharge--;
			}
		}
		else if (type == ChargeType.ELECTRIC)
		{
			if (ElectricCharge > 0)
			{
				ElectricCharge--;
			}
		}
	}
	
	// 1 means up -1 means down on the wheel
	public void CycleChargeType(int value)
	{
		if(SelectedCharge == ChargeType.FIRE)
		{
			if(value == 1)
			{
				SelectedCharge = ChargeType.ELECTRIC;
			}
			else if(value == -1)
			{
				SelectedCharge = ChargeType.ICE;
			}
		}
		else if (SelectedCharge == ChargeType.ICE)
		{
			if (value == 1)
			{
				SelectedCharge = ChargeType.FIRE;
			}
			else if (value == -1)
			{
				SelectedCharge = ChargeType.ELECTRIC;
			}
		}
		else if (SelectedCharge == ChargeType.ELECTRIC)
		{
			if (value == 1)
			{
				SelectedCharge = ChargeType.ICE;
			}
			else if (value == -1)
			{
				SelectedCharge = ChargeType.FIRE;
			}
		}
	}
	
	public void JumpDimension()
	{

		// && WeatherT.GetCurrentWeather != ChargeType.FIRE
		if (SelectedCharge == ChargeType.FIRE && FireCharge == MAX_CHARGES)
		{
			Debug.LogWarning("Jumped to FIRE");
			// WeatherT.UpdateWeather(ChargeType.FIRE);
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.FIRE);
			}
		}
		else if (SelectedCharge == ChargeType.ICE && IceCharge == MAX_CHARGES)
		{
			Debug.LogWarning("Jumped to ICE");
			// WeatherT.UpdateWeather(ChargeType.ICE);
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.ICE);
			}
		}
		else if (SelectedCharge == ChargeType.ELECTRIC && ElectricCharge == MAX_CHARGES)
		{
			Debug.LogWarning("Jumped to ELECTRIC");
			// WeatherT.UpdateWeather(ChargeType.ELECTRIC);
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.ELECTRIC);
			}
		}
		else
		{
			Debug.Log("JumpDimension not available");
		}
	}
	
	

}
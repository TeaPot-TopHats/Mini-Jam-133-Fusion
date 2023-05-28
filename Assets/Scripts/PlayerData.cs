using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	public int Health = 10;
	public int Attack = 5;
	public int Defense = 5;
	
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


	private SpriteRenderer WeaponSprite;

	private void Start()
	{
		Sprite = GetComponent<SpriteRenderer>();
		Rigid = GetComponent<Rigidbody2D>();
		// Anim = GetComponent<Animator>();
		InputH = GetComponent<PlayerInputHandler>();
		WeaponSprite = WeaponObject.GetComponent<SpriteRenderer>();
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

	public void TakeDamage()
	{
		// here
		CheckDeath();
	}
	
	private void CheckDeath()
	{
		
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
		if (SelectedCharge == ChargeType.FIRE && FireCharge == MAX_CHARGES)
		{
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.FIRE);
			}
		}
		else if (SelectedCharge == ChargeType.ICE && IceCharge == MAX_CHARGES)
		{
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.ICE);
			}
		}
		else if (SelectedCharge == ChargeType.ELECTRIC && ElectricCharge == MAX_CHARGES)
		{
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.ELECTRIC);
			}
		}
		else
		{
			Debug.LogError("JumpDimension");
		}
	}
	
	

}
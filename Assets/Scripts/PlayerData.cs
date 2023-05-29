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
	
	public int MAX_CHARGES = 3;
	public ChargeType SelectedCharge = ChargeType.FIRE;
	
	public int FireCharge = 0;
	public int IceCharge = 0;
	public int ElectricCharge = 0;
	

	private SpriteRenderer Sprite;
	private Rigidbody2D Rigid;
	public Animator Anim;

	private PlayerInputHandler InputH;
	
	[SerializeField] private GameObject GameManager;
	private WeatherTracker WeatherT;

	
	private ChargeType Dimension;

	// Score
	public int Score = 0;

	// other
	GameObject map1;
	GameObject map2;
	GameObject map3;

	private void Start()
	{
		Sprite = GetComponent<SpriteRenderer>();
		Rigid = GetComponent<Rigidbody2D>();
		Anim = GetComponent<Animator>();
		InputH = GetComponent<PlayerInputHandler>();

		WeatherT = GameManager.GetComponent<WeatherTracker>();

		map1 = GameObject.FindGameObjectWithTag("Map");
		map2 = GameObject.FindGameObjectWithTag("Map2");
		map3 = GameObject.FindGameObjectWithTag("Map3");

		map1.GetComponent<ChangeDimension>().DimensionShift(ChargeType.FIRE);
		map2.GetComponent<ChangeDimension>().DimensionShift(ChargeType.FIRE);
		map3.GetComponent<ChangeDimension>().DimensionShift(ChargeType.FIRE);
	}

	private void Update()
	{
		if(SelectedCharge == ChargeType.FIRE)
		{
			Sprite.color = new Color(255, 0, 0, 255);
		}
		else if (SelectedCharge == ChargeType.ICE)
		{
			Sprite.color = new Color(0, 163, 255, 255);
		}
		else if (SelectedCharge == ChargeType.ELECTRIC)
		{
			Sprite.color = new Color(255, 211, 0, 255);
		}
	}
	
	private void FixedUpdate()
	{
		SpriteFlipCheck();
		MovementAnimationCheck();
	}
	

	private void SpriteFlipCheck()
	{
		if (InputH.AimVector.x < 0)
		{
			Sprite.flipX = true;
		}
		else if (InputH.AimVector.x > 0)
		{
			Sprite.flipX = false;

		}
	}


	private void MovementAnimationCheck()
	{
		if (InputH.Movement == Vector2.zero)
		{
			Anim.SetBool("IsMoving", false);
		}
		else if ((InputH.Movement.y != 0 && Rigid.velocity.y == 0) && (Mathf.Abs(Rigid.velocity.x) != 0f))
		{
			Anim.SetBool("IsMoving", true);
		}
		else if ((InputH.Movement.y != 0 && Rigid.velocity.y == 0) && (Mathf.Abs(Rigid.velocity.x) == 0f))
		{
			Anim.SetBool("IsMoving", false);
		}
		else if ((InputH.Movement.x != 0 && Rigid.velocity.x == 0) && (Mathf.Abs(Rigid.velocity.y) != 0f))
		{
			Anim.SetBool("IsMoving", true);
		}
		else if ((InputH.Movement.x != 0 && Rigid.velocity.x == 0) && (Mathf.Abs(Rigid.velocity.y) == 0f))
		{
			Anim.SetBool("IsMoving", false);
		}
		else
		{
			Anim.SetBool("IsMoving", true);
		}
	}

	public void Hurt(ChargeType chargeType, int weakDamage, int normalDamage, int strongDamage)
	{
		Debug.LogWarning("Damage");
		Dimension =  WeatherT.GetCurrentWeather();
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
		Health = 0;
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
			
			Anim.SetTrigger("Death");
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
		

		if (SelectedCharge == ChargeType.FIRE && FireCharge == MAX_CHARGES && WeatherT.GetCurrentWeather() != ChargeType.FIRE)
		{
			Debug.LogWarning("Jumped to FIRE");
			// WeatherT.UpdateWeather(ChargeType.FIRE);
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.FIRE);
			}
			WeatherT.UpdateWeather(ChargeType.FIRE);
			map1.GetComponent<ChangeDimension>().DimensionShift(ChargeType.FIRE);
			map2.GetComponent<ChangeDimension>().DimensionShift(ChargeType.FIRE);
			map3.GetComponent<ChangeDimension>().DimensionShift(ChargeType.FIRE);
			Anim.SetTrigger("Jump");
		}
		else if (SelectedCharge == ChargeType.ICE && IceCharge == MAX_CHARGES && WeatherT.GetCurrentWeather() != ChargeType.ICE)
		{
			Debug.LogWarning("Jumped to ICE");
			// WeatherT.UpdateWeather(ChargeType.ICE);
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.ICE);
			}
			WeatherT.UpdateWeather(ChargeType.ICE);
			map1.GetComponent<ChangeDimension>().DimensionShift(ChargeType.ICE);
			map2.GetComponent<ChangeDimension>().DimensionShift(ChargeType.ICE);
			map3.GetComponent<ChangeDimension>().DimensionShift(ChargeType.ICE);
			Anim.SetTrigger("Jump");
		}
		else if (SelectedCharge == ChargeType.ELECTRIC && ElectricCharge == MAX_CHARGES && WeatherT.GetCurrentWeather() != ChargeType.ELECTRIC)
		{
			Debug.LogWarning("Jumped to ELECTRIC");
			// WeatherT.UpdateWeather(ChargeType.ELECTRIC);
			for (int i = 0; i < MAX_CHARGES; i++)
			{
				RemoveCharge(ChargeType.ELECTRIC);
			}
			WeatherT.UpdateWeather(ChargeType.ELECTRIC);
			map1.GetComponent<ChangeDimension>().DimensionShift(ChargeType.ELECTRIC);
			map2.GetComponent<ChangeDimension>().DimensionShift(ChargeType.ELECTRIC);
			map3.GetComponent<ChangeDimension>().DimensionShift(ChargeType.ELECTRIC);
			Anim.SetTrigger("Jump");
		}
		else
		{
			Debug.Log("JumpDimension not available");
		}
	}
	
	
	public void AddScore(int value)
	{
		Score += value;
	}
	
	public int GetScore()
	{
		return Score;
	}
}
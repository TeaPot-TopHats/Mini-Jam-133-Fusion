using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBehavior : MonoBehaviour
{
	[SerializeField] public ChargeType chargeType;
	[SerializeField] private GameObject PlayerObject;
	private PlayerData Player;
	
	public int timeToDespawn = 4;
	
	private void Start() {
		PlayerObject = GameObject.FindGameObjectWithTag("Player");
		Player = PlayerObject.GetComponent<PlayerData>();
		StartCoroutine(Cooldown());
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.name == "Player")
		{
			Player = collision.GetComponent<PlayerData>();
			if(chargeType == ChargeType.FIRE && Player.FireCharge != Player.MAX_CHARGES)
			{
				Player.AddCharge(ChargeType.FIRE);
				Destroy(gameObject);
			}
			else if (chargeType == ChargeType.ICE && Player.IceCharge != Player.MAX_CHARGES)
			{
				Player.AddCharge(ChargeType.ICE);
				Destroy(gameObject);
			}
			else if (chargeType == ChargeType.ELECTRIC && Player.ElectricCharge != Player.MAX_CHARGES)
			{
				Player.AddCharge(ChargeType.ELECTRIC);
				Destroy(gameObject);
			}
			else
			{
				Debug.LogError("ChargeBehavior: Something went very wrong");
			}
		}
	}

	private IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(timeToDespawn);
		Destroy(gameObject);
	}
}

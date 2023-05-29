using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStats : MonoBehaviour
{
	public NPCStartingStat StartingStat;

	public int health;
	
	public int weakAttack;
	public int normalAttack;
	public int strongAttack;
	
	public int speed;
	public int defense;
	public float atkCoolDown;
	public float iFrames;
	public float deathTime;
	public float range;


	private void Awake()
	{
		health = StartingStat.initHealth;
		
		weakAttack = StartingStat.initWeakAttack;
		normalAttack = StartingStat.initNormalAttack;
		strongAttack = StartingStat.initStrongAttack;
		
		speed = StartingStat.initSpeed;
		defense = StartingStat.initDefense;
		atkCoolDown = StartingStat.initAtkCoolDown;
		iFrames = StartingStat.initIFrames;
		deathTime = StartingStat.initDeathTime;
		range = StartingStat.initRange;

	}

	public void ResetAtkCoolDown()
	{
		atkCoolDown = StartingStat.initAtkCoolDown;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NPCInitStats", menuName = "ScriptableObjects/NPCInitStats")]
public class NPCStartingStat : ScriptableObject
{
	public int initHealth;
	
	public int initWeakAttack;
	public int initNormalAttack;
	public int initStrongAttack;
	
	public int initSpeed;
	public int initDefense;
	public float initAtkCoolDown;
	public float initIFrames;
	public float initDeathTime;
	public float initRange;

}

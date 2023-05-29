using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//handles the states of the npc
public class NPCStateMachine : StateMachine
{
	//list of states
	public NPCIdleState IdleState { get; private set; }
	public NPCChasingState ChasingState { get; private set; }
	public NPCDeadState DeadState { get; private set; }
	public NPCAttackingState AttackingState { get; private set; }
	public NPCInjuredState InjuredState { get; private set; }

	public NPCMovementHandler MovementHandler;
	public NPCStats Stats;
	public NPCPathFinding Pathfinder;
	[SerializeField] Rigidbody2D RB;

	[SerializeField] Transform RangeTransformer;

	public NPCAnimationHandler AnimationHandler { get; private set; }
	[SerializeField] Animator Animator;
	public SpriteRenderer Renderer;

	ChargeType Type;

	private void Awake()
	{
		switch (this.gameObject.tag)
		{
			case "Fire":
				Type = ChargeType.FIRE;
				Renderer.color = new Color(255, 0, 0, 255);
				break;
			case "Ice":
				Type = ChargeType.ICE;
				Renderer.color = new Color(0, 163, 255, 255);
				break;
			case "Electric":
				Type = ChargeType.ELECTRIC;
				Renderer.color = new Color(255, 211, 0, 255);
				break;
			default:
				break;
		}

		MovementHandler = new NPCMovementHandler(RB);
		Pathfinder.SetMovementHandler(MovementHandler);
		AnimationHandler = new NPCAnimationHandler(Animator);

		IdleState = new NPCIdleState(this);
		ChasingState = new NPCChasingState(this);
		DeadState = new NPCDeadState(this);
		AttackingState = new NPCAttackingState(this);//Needs work
		InjuredState = new NPCInjuredState(this);

	}

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	void Start()
	{
		ChangeState(IdleState);
	}


	public void Hurt(int atk)
	{
		if (atk - Stats.defense > 0)
			Stats.health -= (atk - Stats.defense);

		if (Stats.health <= 0)
			ChangeState(DeadState);
		else
			ChangeState(InjuredState);
	}

	public bool PlayerInRange()
	{
		if(Pathfinder.distanceFromPlayer < Stats.range)
			return true;
		return false;
	}

	public void AtkCoolDownIteration()
	{
		if (Stats.atkCoolDown < Stats.StartingStat.initAtkCoolDown)
			Stats.atkCoolDown += Time.deltaTime;
	}

	public void AttackPlayer()
	{
		GameObject.FindWithTag("Player").GetComponent<PlayerData>().Hurt(Type, Stats.weakAttack, Stats.normalAttack, Stats.strongAttack);
	}

	public void EndAttack()
	{
		RevertState();
		AnimationHandler.ChangeAnimationState("SlimeIdle");
	}

	public void Die()
	{
		// Destroy(this.gameObject);
		this.gameObject.SetActive(false);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(RangeTransformer.position, Stats.range);
	}
}

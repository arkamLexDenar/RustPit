using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
	public float	timeBetweenAttacks = 0.5f;		// The time in seconds between each attack.
	public int		attackDamage = 10;				// The amount of health taken away per attack.

	Animator		anim;							// Reference to the animator component.
	GameObject		player;							// Reference to the player GameObject.
	PlayerHealth	playerHealth;					// Reference to the player's health.
//	EnemyHealth		enemyHealth;					// Reference to this enemy's health.
	bool			playerInRange;					// Whether player is within the trigger collider and can be attacked.
	float			timer;							// Timer for counting up to the next attack.

	void Awake()
	{
		// Setting up the references.
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.playerHealth = this.player.GetComponent<PlayerHealth>();
//		this.enemyHealth = this.GetComponent<EnemyHealth>();
		this.anim = this.GetComponent<Animator>();
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		// If the entering collider is the player...
		if (other.gameObject == player)
		{
			// ... the player is in range.
			this.playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		// If the exiting collider is the player...
		if (other.gameObject == player)
		{
			// ... the player is no longer in range.
			this.playerInRange = false;
		}
	}

	void Update()
	{
		// Add the time since Update was last called to the timer.
		this.timer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (this.timer >= this.timeBetweenAttacks && this.playerInRange) // && this.enemyHealth.currentHealth > 0)
		{
			// ... attack.
			this.Attack();
		}
		
		// If the player has zero or less health...
//		if (playerHealth.currentHealth <= 0)
//		{
			// ... tell the animator the player is dead.
//			this.anim.SetTrigger("PlayerDead");
//		}
	}

	void Attack()
	{
		// Reset the timer.
		this.timer = 0f;

		// If the player has health to lose...
		if (this.playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			this.playerHealth.TakeDamage(attackDamage);
		}
	}
}
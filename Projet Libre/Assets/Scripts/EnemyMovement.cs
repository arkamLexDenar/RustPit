using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
	Transform		player;				// Reference to the player's position.
//	PlayerHealth	playerHealth;		// Reference to the player's health.
//	EnemyHealth		enemyHealth;		// Reference to this enemy's health.
	NavMeshAgent	nav;				// Reference to the nav mesh agent.

	void Awake()
	{
		// Set up the references.
		this.player = GameObject.FindGameObjectWithTag("Player").transform;
//		this.playerHealth = this.player.GetComponent<PlayerHealth>();
//		this.enemyHealth = this.GetComponent<EnemyHealth>();
		this.nav = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update()
	{
		// If the enemy and the player have health left...
//		if (this.enemyHealth.currentHealth > 0 && this.playerHealth.currentHealth > 0)
//		{
			// ... set the destination of the nav mesh agent to the player.
			this.nav.SetDestination (this.player.position);
//		}
		// Otherwise...
//		else
//		{
			// ... disable the nav mesh agent.
//			this.nav.enabled = false;
//		}
	}
}

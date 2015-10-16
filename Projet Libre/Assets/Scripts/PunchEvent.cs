using UnityEngine;
using System.Collections;

public class PunchEvent : MonoBehaviour
{
	public int		damagePerPunch = 100;				// The damage inflicted by each punch.
	public float	timeBetweenPunches = 0.75f;			// The time between each punch.
	public float	range = 1f;							// The distance the punch can reach.

	Animator		anim;
	float			timer;								// A timer to determine when to punch.
	Ray				shootRay;							// A ray from the punch end forwards.
	RaycastHit		shootHit;							// A raycast hit to get information about what was hit.
	int				shootableMask;						// A layer mask so the raycast only hits things on the shootable layer.

	void Awake()
	{
		// Create a layer mask for the Shootable layer.
		this.shootableMask = LayerMask.GetMask("Shootable");

		this.anim = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		// Add the time since Update was last called to the timer.
		this.timer += Time.deltaTime;
		
		// If the Punch button is being press and it's time to punch...
		if (Input.GetMouseButtonDown(0) && this.timer >= this.timeBetweenPunches)
		{
			// ... punch the guy.
			this.Punch();
		}
	}

	void Punch()
	{
		// Reset the timer.
		this.timer = 0f;

		this.anim.SetTrigger("punch");	

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		this.shootRay.origin = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
		this.shootRay.direction = this.transform.forward;

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if (Physics.Raycast(this.shootRay, out this.shootHit, this.range, this.shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = this.shootHit.collider.GetComponent<EnemyHealth>();
			
			// If the EnemyHealth component exist...
			if (enemyHealth != null)
			{
				// ... the enemy should take damage.
				enemyHealth.TakeDamage(this.damagePerPunch, this.shootHit.point);
			}
		}
	}
}

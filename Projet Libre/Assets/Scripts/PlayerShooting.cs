using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
	public int		damagePerShot = 20;					// The damage inflicted by each bullet.
	public float	timeBetweenBullets = 0.15f;			// The time between each shot.
	public float	range = 100f;						// The distance the gun can fire.
	
	float			timer;								// A timer to determine when to fire.
	Ray				shootRay;							// A ray from the gun end forwards.
	RaycastHit		shootHit;							// A raycast hit to get information about what was hit.
	int				shootableMask;						// A layer mask so the raycast only hits things on the shootable layer.
	ParticleSystem	gunParticles;						// Reference to the particle system.
	LineRenderer	gunLine;							// Reference to the line renderer.
	AudioSource		gunAudio;							// Reference to the audio source.
	Light			gunLight;							// Reference to the light component.
	float			effectsDisplayTime = 0.2f;			// The proportion of the timeBetweenBullets that the effects will display for.

	void Awake()
	{
		// Create a layer mask for the Shootable layer.
		this.shootableMask = LayerMask.GetMask("Shootable");

		// Set up the references.
		this.gunParticles = this.GetComponent<ParticleSystem>();
		this.gunLine = this.GetComponent<LineRenderer>();
		this.gunAudio = this.GetComponent<AudioSource>();
		this.gunLight = this.GetComponent<Light>();
	}

	void Update()
	{
		// Add the time since Update was last called to the timer.
		this.timer += Time.deltaTime;

		// If the Fire1 button is being press and it's time to fire...
		if (Input.GetMouseButtonDown(1) && this.timer >= this.timeBetweenBullets)
		{
			// ... shoot the gun.
			this.Shoot();
		}
		
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
		if (this.timer >= this.timeBetweenBullets * this.effectsDisplayTime)
		{
			// ... disable the effects.
			this.DisableEffects();
		}
	}
	
	public void DisableEffects()
	{
		// Disable the line renderer and the light.
		this.gunLine.enabled = false;
		this.gunLight.enabled = false;
	}

	void Shoot()
	{
		// Reset the timer.
		this.timer = 0f;
		
		// Play the gun shot audioclip.
		this.gunAudio.Play();

		// Enable the light.
		this.gunLight.enabled = true;

		// Stop the particles from playing if they were, then start the particles.
		this.gunParticles.Stop();
		this.gunParticles.Play();

		// Enable the line renderer and set it's first position to be the end of the gun.
		this.gunLine.enabled = true;
		this.gunLine.SetPosition(0, this.transform.position);

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		this.shootRay.origin = this.transform.position;
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
				enemyHealth.TakeDamage(this.damagePerShot, this.shootHit.point);
			}

			// Set the second position of the line renderer to the point the raycast hit.
			this.gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			this.gunLine.SetPosition (1, this.shootRay.origin + this.shootRay.direction * this.range);
		}
	}
}
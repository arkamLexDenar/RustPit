using UnityEngine;
using System.Collections;

public class PunchEvent : MonoBehaviour
{
	private Animator	anim;

	// Use this for initialization
	void Start()
	{
		this.anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
			this.anim.SetTrigger("punch");	
	}
}

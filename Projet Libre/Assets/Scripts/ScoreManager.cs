using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	public static int	score;		// The player's score.

	Text				text;		// Reference to the Text component.

	void Awake()
	{
		// Set up the reference.
		this.text = this.GetComponent<Text>();
		
		// Reset the score.
		score = 0;
	}

	void Update()
	{
		// Set the displayed text to be the word "Score" followed by the score value.
		this.text.text = "" + score;
	}
}
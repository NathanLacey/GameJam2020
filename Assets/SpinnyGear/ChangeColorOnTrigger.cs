using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnTrigger : MonoBehaviour
{
	[SerializeField] string TriggerTagName;

	SpriteRenderer Sprite;
	Color OriginalColor;
	[SerializeField] Color TargetColor;

	private void Start()
	{
		Sprite = GetComponent<SpriteRenderer>();
		OriginalColor = Sprite.color;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == TriggerTagName)
		{
			Sprite.color = TargetColor;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == TriggerTagName)
		{
			Sprite.color = OriginalColor;
		}
	}
}

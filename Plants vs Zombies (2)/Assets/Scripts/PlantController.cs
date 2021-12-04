using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
	public SlotsManagerCollider objectContainer;
	public int Health;
	private Sprite spriteOrig;
	public Sprite spritelight;
	public Sprite spriteOriginal;
	public float damageDelay = 0.17f;
	public GameManager gameManager;

	private void Start()
	{
		gameManager = GameManager.instance;
	}
	private void Update()
	{

	}
	public bool ReceiveDamage(int Damage, bool isEating)
	{
		if (Health - Damage <= 0)
		{
			isEating = false;
			transform.parent.GetComponent<SlotsManagerCollider>().isFull = false;
			Destroy(this.gameObject);
			return isEating;
		}
		else
		{
			this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
			StartCoroutine(DamageColor(this.gameObject.GetComponent<SpriteRenderer>()));
			Health -= Damage;
			return isEating;
		}
	}
	IEnumerator DamageColor(SpriteRenderer spriteRenderer)
	{
		Sprite spriteOrig = spriteRenderer.sprite;
		spriteRenderer.sprite = spritelight;
		yield return new WaitForSeconds(damageDelay);
		spriteRenderer.sprite = spriteOrig;
	}
}
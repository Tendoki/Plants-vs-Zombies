using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ZombieController : MonoBehaviour
{
    public int Health;
    public int DamageValue;
    public float attackCooldown;
    public Vector3 movementSpeed;
    private bool isStopped;
    private GameManager gameManager;
    public Sprite spritelight;
    public Sprite spriteOriginal;
    public float damageDelay = 0.16f;
    public List<AudioClip> damageAudio;
    public List<AudioClip> chompAudio;
    public List<AudioClip> groanAudio;
    public float time;
    public float minTime;
    public float maxTime;
    public bool isEating = false;
    private void Start()
    {
        gameManager = GameManager.instance;
        time = Random.Range(minTime, maxTime);
        StartCoroutine(GroanRandom());
    }
    void Update()
    {
        if (gameManager.isLose || gameManager.isWin)
        {
            StopCoroutine(GroanRandom());
        }
        if (!gameManager.isLose && !gameManager.isWin)
		{
            if (!isStopped)
            {
                transform.Translate(movementSpeed * Time.deltaTime * -1);
            }
        }
    }

    public IEnumerator GroanRandom()
    {
        this.GetComponent<AudioSource>().PlayOneShot(groanAudio[Random.Range(0, groanAudio.Count)]);
        yield return new WaitForSeconds(time);
        time = Random.Range(minTime, maxTime);
        if (gameManager.isLose || gameManager.isWin)
        {
            StopCoroutine(GroanRandom());
        }
        else
        {
            StartCoroutine(GroanRandom());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
			if (!isEating)
			{
                isEating = true;
                StartCoroutine(Attack(collision));
                isStopped = true;
            }
        }
    }
    IEnumerator Attack(Collider2D collision)
    {
        if (collision == null)
        {
            isStopped = false;
        }
        else
        {
            isEating = collision.gameObject.GetComponent<PlantController>().ReceiveDamage(DamageValue, isEating);
            this.GetComponent<AudioSource>().PlayOneShot(chompAudio[Random.Range(0, chompAudio.Count)]);
            yield return new WaitForSeconds(attackCooldown);
            StartCoroutine(Attack(collision));
        }
    }

    public void ReceiveDamage(int Damage)
    {
        this.GetComponent<AudioSource>().PlayOneShot(damageAudio[Random.Range(0, damageAudio.Count)]);
        if (Health - Damage <= 0)
        {
            transform.parent.GetComponent<SpawnPoint>().zombies.Remove(this.gameObject);
			if (gameManager.zombiesLevel1 > 1)
			{
                gameManager.zombiesLevel1--;
            }
            else
			{
                gameManager.isWin = true;
			}
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteOriginal;
            StartCoroutine(DamageColor(this.gameObject.GetComponent<SpriteRenderer>()));
            foreach (Transform item in this.transform.GetComponentInChildren<Transform>())
			{
                StartCoroutine(DamageColor(item.gameObject.GetComponent<SpriteRenderer>()));      
            }
            Health -= Damage;
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

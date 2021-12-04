using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 movementSpeed;
    public int DamageValue;
    public GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void Update()
    {
		if (!gameManager.isLose && !gameManager.isWin)
		{
            transform.Translate(movementSpeed * Time.deltaTime);
        }
        else
		{
            Destroy(this.gameObject);
		}
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<ZombieController>().ReceiveDamage(DamageValue);
            Destroy(this.gameObject);
        }
    }
}

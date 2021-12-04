using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PeashooterCard : CardManager, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

	public void OnDrag(PointerEventData eventData)
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (isCoolingDown)
			{
				return;
			}
			//Взять gameObject
			if (isHoldingPlant)
			{
				objectDragInstance.GetComponent<SpriteRenderer>().sprite = plantSprite;
				objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
		//plant.GetComponent<SpriteRenderer>().sprite = plantSprite;
		//if (prevName != colliderName || prevName == null)
		//{
		//	isOverCollider = false;
		//	if (prevName != null)
		//	{
		//		prevName.plant = null;
		//	}
		//	prevName = colliderName;
		//}

		//if (!isOverCollider)
		//{
		//	plant.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//}

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (isCoolingDown)
			{
				return;
			}

			if (gameManager.SunAmount >= plantCardParameters.cost)
			{
				isHoldingPlant = true;
				Vector3 pos = new Vector3(0, 0, -1);
				objectDragInstance = Instantiate(plant_Drag, pos, Quaternion.identity);
				objectDragInstance.GetComponent<SpriteRenderer>().sprite = plantSprite;
				objectDragInstance.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				objectDragInstance.GetComponent<PeashooterDragging>().card = this;

				gameManager.draggingObject = objectDragInstance;
			}
		}
		//plant = Instantiate(plantPrefab, pos, Quaternion.identity);
		//plant.GetComponent<SpriteRenderer>().sprite = plantSprite;
		//plant.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			if (isCoolingDown)
			{
				return;
			}
			if (isHoldingPlant)
			{
				isHoldingPlant = false;
				gameManager.PlaceObjectShooter();
				if (gameManager.currentSlot != null)
				{
					gameManager.DeductSun(plantCardParameters.cost);
					this.GetComponent<AudioSource>().PlayOneShot(draggingAudio[Random.Range(0, draggingAudio.Count)]);
					StartCoroutine(CardCooldown(plantCardParameters.cooldown));
				}
				if (plantCardParameters.isSunflower)
				{
					SunSpawnerSunflower sunSpawner = plant.GetComponent<SunSpawnerSunflower>();
					sunSpawner.isSunflower = true;
					sunSpawner.time = plantCardParameters.sunSpawnerTemplate.time;
					sunSpawner.sun = plantCardParameters.sunSpawnerTemplate.sun;
				}
				gameManager.draggingObject = null;
				Destroy(objectDragInstance);
			}
		}
		//if (colliderName != null && !colliderName.isOccupied)
		//{
		//	colliderName.isOccupied = true;
		//	plant.tag = "Untagged";
		//	plant.transform.SetParent(colliderName.transform);
		//	plant.transform.position = new Vector3(0, 0, -1);
		//	plant.transform.localPosition = new Vector3(0, 0, -1);
		//}
		//else
		//{
		//	Destroy(plant);
		//}
	}
}

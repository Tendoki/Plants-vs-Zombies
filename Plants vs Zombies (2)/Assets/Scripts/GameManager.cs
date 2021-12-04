using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	[Header("Drag Parameters")]
	public GameObject draggingObject;
	public GameObject currentSlot;
    public static GameManager instance;
	[Header("Sun Parameters")]
	public TMP_Text sunDisp;
	public int startingSunAmnt;
	public int SunAmount = 0;
	public bool isLose = false;
	public bool isWin = false;
	public int zombiesLevel1;
	private void Start()
	{
		AddSun(startingSunAmnt);
		instance = this;
	}

	private void Update()
	{

	}

	public void AddSun(int amnt)
	{
		SunAmount += amnt;
		sunDisp.text = "" + SunAmount;
	}
	public void DeductSun(int amnt)
	{
		SunAmount -= amnt;
		sunDisp.text = "" + SunAmount;
	}

	private void Awake()
	{
		instance = this;
	}

	public void PlaceObjectShooter()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<PeashooterDragging>().card.plant, currentSlot.transform);
			objectGame.GetComponent<PlantAttackController>().zombies = currentSlot.GetComponent<SlotsManagerCollider>().spawnPoint.zombies;
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}
	public void PlaceObjectSunflower()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<SunflowerDragging>().card.plant, currentSlot.transform);
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}
	public void PlaceObjectWallNut()
	{
		if (draggingObject != null && currentSlot != null)
		{
			GameObject objectGame = Instantiate(draggingObject.GetComponent<WallNutDragging>().card.plant, currentSlot.transform);
			currentSlot.GetComponent<SlotsManagerCollider>().isFull = true;
		}
	}
}

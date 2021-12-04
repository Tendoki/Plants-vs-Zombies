using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlantCardManager : MonoBehaviour
{
    [Header("Cards Parameters")]
    public int amtOfCards;
    //int currAmrCard = 0;
    //public PlantCardScriptableObject[] plantCardSO;
    public GameObject[] cardPrefab;
    public Transform cardHolderTransform;

    [Header("Plant Parameters")]
    public GameObject[] plantCards;
    public float cooldown;
    public int cost;
    //public Texture plantIcon;

	private void Start()
	{
        amtOfCards = cardPrefab.Length;
        plantCards = new GameObject[amtOfCards];
		for (int i = 0; i < amtOfCards; i++)
		{
            AddPlantCard(i);
		}
    }
    public void AddPlantCard(int index)
	{
        GameObject card = Instantiate(cardPrefab[index], cardHolderTransform);
        CardManager cardManager = card.GetComponent<CardManager>();
        cardManager.plantCardParameters = cardPrefab[index].GetComponent<PlantCardParameters>();
        cardManager.plantSprite = cardPrefab[index].GetComponent<PlantCardParameters>().plantSprite;
        //cardManager.UI = GameObject.FindGameObjectWithTag("Canvas");

        plantCards[index] = card;

        //plantIcon = plantCardSO[index].plantIcon;
        //cost = plantCardSO[index].cost;
        //cooldown = plantCardSO[index].cooldown;

        //card.GetComponentInChildren<RawImage>().texture = plantIcon;
        //card.GetComponentInChildren<TMP_Text>().text = "" + cost;
    }
}

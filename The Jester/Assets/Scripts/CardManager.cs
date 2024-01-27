using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CardManager : MonoBehaviour
{
    public static List<Card> cards = new List<Card>();

    [SerializeField]
    public GameObject PhysicalCardPrefab;
    public GameObject SatireCardPrefab;
    public GameObject WordplayCardPrefab;

    private GameObject _cardPrefab;
    public static CardManager instance;

    private void Start()
    {   
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void AddCard(Card card){
        cards.Add(card);

        switch (card.cardType)
        {
            case Card.CardType.Physical:
                _cardPrefab = PhysicalCardPrefab;
                break;
            case Card.CardType.Satire:
                _cardPrefab = SatireCardPrefab;
                break;
            case Card.CardType.Wordplay:
                _cardPrefab = WordplayCardPrefab;
                break;
        }

        ResizeHand();
    }

    private void ResizeHand(){
        for (int i = 0; i < cards.Count; i++)
        {
            float distance_x = 1.1f; //distance per card
            float distance_z = 0.05f; 
            cards[i].transform.position = new Vector3(
                distance_x * i - cards.Count * distance_x / 2, 
                -1.78f, 
                5f - distance_z * i); 

            cards[i].transform.rotation = Quaternion.Euler(0, 180, 90);
        }
        
    }

    public void RemoveCard(Card card){
        cards.Remove(card);
        ResizeHand();
    }

    void Awake()
    {
        AddCard(Instantiate(PhysicalCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(SatireCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(WordplayCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(PhysicalCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(SatireCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(WordplayCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(PhysicalCardPrefab).GetComponent<Card>());
        AddCard(Instantiate(SatireCardPrefab).GetComponent<Card>());
    }    

}

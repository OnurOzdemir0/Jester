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

        ResizeHand();
    }

    private void ResizeHand(){
        for (int i = 0; i < cards.Count; i++)
        {
            float distance_x = 1.1f; //distance per card
            float distance_z = 0.05f; 
            cards[i].transform.localPosition = new Vector3(
                (distance_x * i - cards.Count * distance_x / 2)*-1, 
                -2.2f, 
                (5f - distance_z * i)*-1); 

            cards[i].transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        
    }

    public void RemoveCard(Card card){
        
        ResizeHand();
    }

    void Awake()
    {
        GameObject physicalCardPrefab = Instantiate(PhysicalCardPrefab, transform.position, Quaternion.identity,transform);

        AddCard(physicalCardPrefab.GetComponent<PhysicalJokeCard>()); //.GetComponent<Card>());

        GameObject satire = Instantiate(SatireCardPrefab, transform.position, Quaternion.identity, transform);
        AddCard(satire.GetComponent<SatireCard>()); //.GetComponent<Card>());

        GameObject word = Instantiate(WordplayCardPrefab, transform.position, Quaternion.identity, transform);
        AddCard(word.GetComponent<WordPlayCard>()); //.GetComponent<Card>());

    }    

}

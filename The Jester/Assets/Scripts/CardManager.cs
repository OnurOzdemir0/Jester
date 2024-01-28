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
    List<GameObject> cardPrefabs = new List<GameObject>();

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
        cards.Remove(card);
        ResizeHand();
    }

    void Awake()
    {
        int randomIndex1 = Random.Range(0, cardPrefabs.Count);
        int randomIndex2 = Random.Range(0, cardPrefabs.Count);
        int randomIndex3 = Random.Range(0, cardPrefabs.Count);
        int randomIndex4 = Random.Range(0, cardPrefabs.Count);
        //
        GameObject card1 = Instantiate(cardPrefabs[randomIndex1], transform.position, Quaternion.identity,transform);

        AddCard(card1.GetComponent<Card>()); //.GetComponent<Card>());

        GameObject card2 = Instantiate(cardPrefabs[randomIndex2], transform.position, Quaternion.identity, transform);

        AddCard(card2.GetComponent<Card>()); //.GetComponent<Card>());
        GameObject card3 = Instantiate(cardPrefabs[randomIndex3], transform.position, Quaternion.identity, transform);

        AddCard(card3.GetComponent<Card>()); //.GetComponent<Card>());
        GameObject card4 = Instantiate(cardPrefabs[randomIndex4], transform.position, Quaternion.identity, transform);

        AddCard(card4.GetComponent<Card>()); //.GetComponent<Card>());

    }    

}

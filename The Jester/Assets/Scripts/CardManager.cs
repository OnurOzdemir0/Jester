using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CardManager : MonoBehaviour
{
    private List<Card> cards = new List<Card>();



    [SerializeField]
    List<GameObject> cardPrefabs = new List<GameObject>();

    private GameObject _cardPrefab;
    public static CardManager instance;

    private void Awake()
    {
        if(instance != null && instance!= this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    { 

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
    public void FullSwapCards()
    {
        for(int i = 0; i< cards.Count;i++)
        {
            Destroy(cards[i].gameObject);
        }
        cards.Clear();
        DrawCard(4);

    }
    public void RemoveCard(Card card){
        cards.Remove(card);
        ResizeHand();
    }

    private void DrawCard(int count)
    {
        for(int i = 0;i < count; i++) {
            int randomIndex = Random.Range(0, cardPrefabs.Count);
            GameObject card = Instantiate(cardPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
            AddCard(card.GetComponent<Card>());
        }
    }
  

}

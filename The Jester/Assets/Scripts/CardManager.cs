using DG.Tweening;
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
    List<GameObject> atackCardPrefabs = new List<GameObject>();
    [SerializeField]
    List<GameObject> magicCardPrefabs = new List<GameObject>();
    [SerializeField]
    List<Transform> placeHolders = new List<Transform>();

    [SerializeField]
    int desiredCardCount = 4;

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

    public void SetupDeck()
    {
        DrawCard(desiredCardCount);
        Debug.Log("CARD MANAGER DEBUG :: SETUPPING DECK");
    }

    private void AddCard(Card card){
        cards.Add(card);
        Debug.Log("CARD MANAGER DEBUG :: ADDING CARD : " + card.name);
        ResizeHand();
           
    }

    public void FillHand()
    {
        if(desiredCardCount>cards.Count )
        {
            DrawCard(desiredCardCount - cards.Count);
            Debug.Log("CARD MANAGER DEBUG :: FILLING HAND number of : " + (desiredCardCount - cards.Count));
        }
    }

    private void ResizeHand(){
        Debug.Log("CARD MANAGER DEBUG :: RESIZING HAND ");
        int startIndex = 0;
        if(cards.Count == 4)
        {
            startIndex = 6;

        }
        if (cards.Count == 3)
        {
            startIndex = 3;

        }
        if (cards.Count ==2)
        {
            startIndex = 1;

        }
        if (cards.Count == 1)
        {
            startIndex = 0;

        }

        for (int i = 0; i < cards.Count; i++)
        {



            cards[i].Move(placeHolders[startIndex+i]);
            


            Debug.Log("CARD MANAGER DEBUG :: RESIZED ");


        }

        
        

        
    }
    public void FullSwapCards()
    {
        Debug.Log("CARD MANAGER DEBUG :: FULLSWAPPING HAND ");
        for (int i = 0; i< cards.Count;i++)
        {
            Destroy(cards[i].gameObject);
        }
        cards.Clear();
        DrawCard(4);


    }
    public void RemoveCard(Card card){
        Debug.Log("CARD MANAGER DEBUG :: REMOVING CARD " + card.name);
        cards.Remove(card);
        ResizeHand();
    }

    private void DrawCard(int count)
    {
        Debug.Log("CARD MANAGER DEBUG :: DRAWING CARD number of " + count );
        for (int i = 0;i < count; i++) {

            int selection = DecideWhereToDrawFrom();

            switch (selection)
            {
                case 0:
                    int s = Random.Range(0, 2);
                    if(s == 0)
                    {
                        int randomIndex = Random.Range(0, atackCardPrefabs.Count);
                        GameObject card = Instantiate(atackCardPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
                        Debug.Log("CARD MANAGER DEBUG :: DRAWEd CARD " + card.name);
                        AddCard(card.GetComponent<Card>());
                    }
                    else
                    {

                        int randomIndex = Random.Range(0, magicCardPrefabs.Count);
                        GameObject card = Instantiate(magicCardPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
                        Debug.Log("CARD MANAGER DEBUG :: DRAWEd CARD " + card.name);
                        AddCard(card.GetComponent<Card>());
                    }
                    
                  
                break;

                case 1:
                    {
                        int randomIndex = Random.Range(0, magicCardPrefabs.Count);
                        GameObject card = Instantiate(magicCardPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
                        Debug.Log("CARD MANAGER DEBUG :: DRAWEd CARD " + card.name);
                        AddCard(card.GetComponent<Card>());

                    }

                break;

                case 2:
                    {
                        int randomIndex = Random.Range(0, atackCardPrefabs.Count);
                        GameObject card = Instantiate(atackCardPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
                        Debug.Log("CARD MANAGER DEBUG :: DRAWEd CARD " + card.name);
                        AddCard(card.GetComponent<Card>());
                    }
            

              break;

            }
          


        }
    }

    private int DecideWhereToDrawFrom()
    {
        int magicCardCount = 0;
        int atackCardCount = 0;
        foreach(Card card in cards)
        {
            if (card.isMagicCard)
            {
                magicCardCount++;
            }
            else
            {
                atackCardCount++;
            }
        }
        if(atackCardCount - magicCardCount < 3 && magicCardCount < 2) 
        {
            return 0; // can draw both
        }
        else if(atackCardCount ==3)
        {
            return 1;// can draw magic card
        }
        else
        {
            return 2; // can draw atackCard
        }


    }
  

}

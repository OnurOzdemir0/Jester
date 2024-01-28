using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSwapCard : Card
{
    private void Awake()
    {
        this.isMagicCard = true;
    }
    public override void playCard()
    {


        if (!this.isPlayed && GameLogic.instance.getCanPlayMagicCard())
        {
            Debug.Log("FullSwap  Card Played!");
            CardManager.instance.FullSwapCards();
            GameLogic.instance.PlayMagicCard();
            isPlayed = true;
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
            CardManager.instance.RemoveCard(this);
            Destroy(this.gameObject, 0.5f);
        }
    }
}

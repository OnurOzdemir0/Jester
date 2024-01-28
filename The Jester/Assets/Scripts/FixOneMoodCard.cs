using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixOneMoodCard : Card
{
    private void Awake()
    {
        this.isMagicCard = true;
    }
    public override void playCard()
    {
        Debug.Log("FixOneMoodCard  Card Played!");
        this.isMagicCard = true;

        if(!this.isPlayed && GameLogic._logicInstance.getCanPlayMagicCard())
        {
            isPlayed = true;
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
            CardManager.instance.RemoveCard(this);
            // getSomeHow the mood to fix from UI then 
            //GameLogic._logicInstance.fixTheMoodCard(type);
            //

            GameLogic._logicInstance.PlayMagicCard();

            Destroy(this.gameObject, 0.5f);
        }
    }
}
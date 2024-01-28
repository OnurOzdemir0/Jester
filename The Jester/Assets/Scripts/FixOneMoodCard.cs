using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixOneMoodCard : Card
{
 
    public override void playCard()
    {
       
        this.isMagicCard = true;

        if(!this.isPlayed && GameLogic.instance.getCanPlayMagicCard())
        {
            Debug.Log("FixOneMoodCard  Card Played!");
            isPlayed = true;
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
            CardManager.instance.RemoveCard(this);
            // getSomeHow the mood to fix from UI then 
            //GameLogic._logicInstance.fixTheMoodCard(type);
            //

            GameLogic.instance.PlayMagicCard();

            Destroy(this.gameObject, 0.5f);
        }
    }
}
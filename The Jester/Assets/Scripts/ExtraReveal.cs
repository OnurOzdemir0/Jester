using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExtraReveal : Card
{
    

    public override void playCard()
    {
        this.isMagicCard = true;
       

        if(!this.isPlayed && GameLogic.instance.getCanPlayMagicCard())
        {

         


            if (GameLogic.instance.RevealMoodRandom())
            {
                Debug.Log("ExtraReveal  Card Played!");
                isPlayed = true;
               
                CardManager.instance.RemoveCard(this);

                GameLogic.instance.PlayMagicCard();

                Destroy(this.gameObject, 0.5f);
            }
            else
            {
              
            }

        }
    }
}

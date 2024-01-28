using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExtraReveal : Card
{
    

    public override void playCard()
    {
        this.isMagicCard = true;
        //Debug.Log("ExtraReveal  Card Played!");

        if(!this.isPlayed && GameLogic._logicInstance.getCanPlayMagicCard())
        {


           

            if (GameLogic._logicInstance.RevealMoodRandom())
            {
                isPlayed = true;
               
                CardManager.instance.RemoveCard(this);

                GameLogic._logicInstance.PlayMagicCard();

                Destroy(this.gameObject, 0.5f);
            }
            else
            {
              
            }

        }
    }
}

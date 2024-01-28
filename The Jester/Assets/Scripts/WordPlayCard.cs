using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WordPlayCard : Card
{

    public override void playCard()
    {
        
        
        if(!this.isPlayed)
        {
            if (GameLogic.instance.handleAttack(GameLogic.MoodType.WORD))
            {
                Debug.Log("WordPlay Card Played!");
                CardManager.instance.RemoveCard(this);
                isPlayed = true;
                Destroy(this.gameObject, 0.5f);
            }
            else
            {
                // handle problem
            }
          
        }
    }
}

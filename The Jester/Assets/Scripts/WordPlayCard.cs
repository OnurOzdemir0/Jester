using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WordPlayCard : Card
{

    public override void playCard()
    {
        Debug.Log("WordPlay Card Played!");
        
        if(!this.isPlayed)
        {
            if (GameLogic._logicInstance.handleAttack(GameLogic.MoodType.WORD))
            {
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

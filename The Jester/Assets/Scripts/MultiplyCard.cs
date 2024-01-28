using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiplyCard : Card
{   
    
    public override void playCard()
    {
        this.isMagicCard = true;
        Debug.Log("Multiply  Card Played!");

        if(!this.isPlayed && GameLogic._logicInstance.getCanPlayMagicCard())
        {

            GameLogic._logicInstance.UpdateMoodBonus();
            
                isPlayed = true;
                // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
                CardManager.instance.RemoveCard(this);
                GameLogic._logicInstance.PlayMagicCard();
                Destroy(this.gameObject, 0.5f);
            
          
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCard : Card
{
   
    public override void playCard()
    {
    
     
        if(!this.isPlayed && GameLogic.instance.getCanPlayMagicCard())
        {
            Debug.Log("Barrier  Card Played!");
            GameLogic.instance.ActivateBarrier();
            GameLogic.instance.PlayMagicCard();
            isPlayed = true;
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
            CardManager.instance.RemoveCard(this);
            Destroy(this.gameObject, 0.5f);
        }
    }
}

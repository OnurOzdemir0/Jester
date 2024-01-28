using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCard : Card
{
    public override void playCard()
    {
        Debug.Log("Barrier  Card Played!");

        if(!this.isPlayed && GameLogic._logicInstance.canPlayMagicCard)
        {
            isPlayed = true;
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
            CardManager.instance.RemoveCard(this);

            

            GameLogic._logicInstance.canPlayMagicCard = false;

            Destroy(this.gameObject, 0.5f);
        }
    }
}

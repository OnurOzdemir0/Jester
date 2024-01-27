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
            isPlayed = true;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z+5), 0.5f);
            CardManager.instance.RemoveCard(this);
            Destroy(this.gameObject, 0.5f);
        }
    }
}

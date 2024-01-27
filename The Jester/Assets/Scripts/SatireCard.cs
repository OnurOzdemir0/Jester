using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class SatireCard : Card
{

    public override void playCard()
    {
        Debug.Log("Satire  Card Played!");

        if(!this.isPlayed)
        {
            isPlayed = true;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z+5), 0.5f);
            CardManager.instance.RemoveCard(this);
        }
    }
    
}

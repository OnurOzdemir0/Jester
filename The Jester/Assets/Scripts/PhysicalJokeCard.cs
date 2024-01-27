using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PhysicalJokeCard : Card
{

    public override void playCard()
    {
        Debug.Log("Physical Joke Card Played!");

        if(!this.isPlayed)
        {
            isPlayed = true;
            transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z+5), 0.5f);
            CardManager.instance.RemoveCard(this);
            Destroy(this.gameObject, 0.5f);
        }
    }
}

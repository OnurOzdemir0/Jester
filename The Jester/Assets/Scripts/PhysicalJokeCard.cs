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
            
            // transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z-5), 0.5f);
           

            if (GameLogic._logicInstance.handleAttack(GameLogic.MoodType.PHY))
            {
                CardManager.instance.RemoveCard(this);
                isPlayed = true;
                Destroy(this.gameObject, 0.5f);
            }
            else
            {
                // handle problem
            }

            // GameLogic._logicInstance.handleAttack(0); //0 for index of the joke list index in GameLogic

            
        }
    }
}

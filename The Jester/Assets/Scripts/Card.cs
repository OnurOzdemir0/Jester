using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Card : MonoBehaviour
{
    private bool isHighlighted = false;
    protected bool isPlayed = false;
    private Vector3 initialPos;
    private Vector3 initialLocalRot;

    public enum CardType
    {
        Physical, Satire, Wordplay
    }
    public CardType cardType;

    private Material material;

    
    private enum FaceDir
    {
        FrontFace, BackFace
    }
    private FaceDir faceDir;

    private void Start()
    {
        initialPos = transform.position;
        initialLocalRot = transform.localRotation.eulerAngles;
        material = GetComponent<Material>();
    }

   
    public  void highlightCard(bool isTrue)
    {
        if(isTrue)
        {
            if(!isHighlighted)
            {
                isHighlighted = true;
                transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z-1f);
                transform.DOMove(new Vector3(transform.position.x, initialPos.y + 1, transform.position.z), 0.5f);
                


            }
        }
        else
        {
            if(isHighlighted)
            {   
                isHighlighted = false;
                transform.position = new Vector3(transform.position.x, transform.position.y,initialPos.z);
                transform.DOMove(new Vector3(transform.position.x, initialPos.y, transform.position.z), 0.5f).onComplete = resetCardRotation;
            }
           


            
        }
        // card highlight is same in every card
    }
    private void resetCardRotation()
    {
        switch (faceDir)
        {
            
            case FaceDir.BackFace:
                faceDir = FaceDir.FrontFace;
                transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x, initialLocalRot.y, transform.localRotation.eulerAngles.z), 0.5f); break;
                // reverse
        }
    }
    public void reverseCard()
    {
        switch (faceDir)
        {
            case FaceDir.FrontFace:
                faceDir = FaceDir.BackFace;
                transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x, 0, transform.localRotation.eulerAngles.z), 0.5f); break;
                // reverse
               

            case FaceDir.BackFace:
                faceDir = FaceDir.FrontFace;
                transform.DOLocalRotate(new Vector3(transform.localRotation.eulerAngles.x, initialLocalRot.y, transform.localRotation.eulerAngles.z), 0.5f); break;
                // reverse
        }
    }

    public abstract void playCard();
   
}

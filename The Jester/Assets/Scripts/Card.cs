using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    private bool isHighlighted = false;
    private bool isPLayed = false;
    private Vector3 initialPos;
    private Vector3 initialLocalRot;
    
    
    private enum FaceDir
    {
        FrontFace, BackFace
    }
    private FaceDir faceDir;

    private void Start()
    {
        initialPos = transform.position;
        initialLocalRot = transform.localRotation.eulerAngles;
    }

   
    public  void highlightCard(bool isTrue)
    {
        if(isTrue)
        {
            if(!isHighlighted)
            {
                isHighlighted = true;
                transform.position = new Vector3 (transform.position.x, transform.position.y, 4.95f);
                transform.DOMove(new Vector3(transform.position.x, initialPos.y + 1, transform.position.z), 0.5f);


            }
        }
        else
        {
            if(isHighlighted)
            {
                isHighlighted = false;
                transform.position = new Vector3(transform.position.x, transform.position.y,initialPos.z);
                transform.DOMove(new Vector3(transform.position.x, initialPos.y, transform.position.z), 0.5f);
            }
           


            
        }
        // card highlight is same in every card
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

    public abstract void playCard(); // every card implement this differently so handle this on inherited object
   
}

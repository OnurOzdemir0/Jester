using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Card : MonoBehaviour, IInteractable
{
    private bool isHighlighted = false;
    protected bool isPlayed = false;
   
    private Vector3 initialScale;
    [SerializeField]
    public bool isMagicCard = false;
  
    private Material material;

    
    private enum FaceDir
    {
        FrontFace, BackFace
    }
    private FaceDir faceDir;

    bool isAnimating = false;
    protected void Awake()
    {
        GetComponent<Collider>().enabled = false;
        Debug.Log(GetComponent<Collider>());
        initialScale = transform.localScale;
        material = GetComponent<Renderer>().material;
      
    }
    protected void OnDestroy()
    {
        // tween it 
    }


    

    private  void highlightCard(bool isTrue)
    {
        if(isTrue)
        {
            
            if(!isHighlighted)
            {
                isHighlighted = true;
                material.SetFloat("_emission_strength", .8f);
                transform.localPosition += transform.forward;
                transform.DOScale(initialScale * 1.2f, 0.1f);
                Debug.Log("CARD::DEBUG :: SCALE MOVED TO : " + initialScale * 1.2f);

               /*
                transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, initialPos.z+1f);
                Debug.Log(transform.position);
                isAnimating = true;
                transform.DOLocalMove(new Vector3(transform.localPosition.x, initialPos.y + 1, transform.localPosition.z), 0.5f);
                */


            }
            
        }
        else
        {
            if(isHighlighted)
            {   
                isHighlighted = false;
                material.SetFloat("_emission_strength",0.0f);
                transform.localPosition -= transform.forward;
                transform.DOScale(initialScale, 0.1f).onComplete =()=> { if(this)   resetCardRotation(); };
                Debug.Log("CARD::DEBUG :: SCALE MOVED TO : " + initialScale );
                /*
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,initialPos.z);
                transform.DOLocalMove(new Vector3(transform.localPosition.x, initialPos.y, transform.localPosition.z), 0.5f).onComplete = resetCardRotation;
                */

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
                transform.DOLocalRotate(new Vector3(0, -180, 90), 0.5f); break;
                // reverse
        }
    }
    public void Move(Transform t)
    {   
        GetComponent<Collider>().enabled = false;
        transform.localRotation = t.localRotation;
        transform.DOLocalMove(t.localPosition, 0.1f).onComplete = ()=> { GetComponent<Collider>().enabled = true; };
    }
    public void reverseCard()
    {
        if (!isAnimating)
        {

       
            switch (faceDir)
            {
                case FaceDir.FrontFace:
                    faceDir = FaceDir.BackFace;
                    isAnimating  = true;
                    transform.DOLocalRotate( new Vector3(0,180,90) , 0.5f).onComplete = ()=> { isAnimating = false; }; break;
                    // reverse
               

                case FaceDir.BackFace:
                    faceDir = FaceDir.FrontFace;
                    isAnimating = true;
                    transform.DOLocalRotate(new Vector3(0, -180, 90), 0.5f).onComplete = () => { isAnimating = false; }; break;
                    // reverse
            }
        }
    }
    public abstract void playCard();
    public void OnSelection()
    {
        highlightCard(true);
    }
    public void OnPressLeftClick()
    {
        playCard();
    }
    public void OnPressRightClick()
    {
        reverseCard();
    }

    public void OnDeSelection()
    {
        highlightCard(false);
    }


}

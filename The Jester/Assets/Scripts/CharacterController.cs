using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Camera playerCam;

    private Card currentCard = null;
 



    // shoot ray to detect card

    // reverse card

    // play card

    private void FixedUpdate()
    {
        shootRayIntoMouseDirection(); // always shoot 
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(currentCard != null)
            {
                currentCard.playCard();
            }


        }
        if(Input.GetMouseButtonDown(1))
        {
            if(currentCard != null)
            {
                currentCard.reverseCard();
            }
        }

    }
    private void shootRayIntoMouseDirection()
    {
        Vector3  mousedir =  new Vector3 (Input.mousePosition.x, Input.mousePosition.y, transform.position.z + playerCam.nearClipPlane) ;
        mousedir = playerCam.ScreenToWorldPoint(mousedir);
     

        Ray ray = new Ray(playerCam.transform.position, mousedir);
        //Debug.Log("origin :" + ray.origin + " dir : " + mousedir);
        Debug.DrawRay(ray.origin,ray.direction*10000,Color.red,1);

        if(Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity)){
            // Debug.Log("Card found");
            if (hit.collider.gameObject.TryGetComponent<Card>(out Card card))
            {
                if(currentCard == null)
                {
                    currentCard = card;
                    currentCard.highlightCard(true);

                }else if (currentCard != card)
                {
                    currentCard.highlightCard(false);
                    currentCard = card;
                    currentCard.highlightCard(true);

                }

            }
            else
            {
                currentCard.highlightCard(false);
                currentCard = null;
                
            }

        }
        else
        {   if(currentCard !=null) {
                currentCard.highlightCard(false);
                currentCard = null;
            
            }
            
        }
    }

    



}

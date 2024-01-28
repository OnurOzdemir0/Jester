using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClockVisuals : MonoBehaviour
{
    [SerializeField] Material clockMaterial;

    [SerializeField] Color emissionColor = Color.black;

    [SerializeField,Range(0.05f,float.MaxValue)] float duration = 1.0f;

    private bool isHighlighted = false;
    private bool isHighlightingNow = false;
    private bool isUnHighlightingNow  =false;
    public void  HighlightClock()
    {
        
        if(!isHighlighted)
        {
            isHighlighted = true;

            
            if(!isHighlightingNow)
                StartCoroutine(smoothIncrease());
                    
                  


        }

    }
    public void UnHighlight()
    {
        if(isHighlighted)
        {
            isHighlighted =false;   

            if(!isUnHighlightingNow)
            {
                StartCoroutine(smoothDecrease());
            }


        }

    }
    private IEnumerator smoothIncrease()
    {

        isHighlightingNow = true;
        float startValue = 0.0f;
            float elapsedTime = 0f;
            
        while (elapsedTime < duration && isHighlighted)
        {
              
            float newValue = Mathf.Lerp(startValue, 100, elapsedTime / duration);

            Color newColor = Color.HSVToRGB(0, 0, newValue);
            clockMaterial.SetColor("_EmissiveColor", newColor);

              
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isHighlightingNow = false;
            
        // Coroutine finished
        Debug.Log("Emission Value animation completed!");
     }


    private IEnumerator smoothDecrease()
    {

        isUnHighlightingNow = true;
        float startValue = 100.0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            float newValue = Mathf.Lerp(startValue, 0, elapsedTime / duration);

            Color newColor = Color.HSVToRGB(0, 0, newValue);
            clockMaterial.SetColor("_EmissiveColor", newColor);


            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isUnHighlightingNow = false;

        // Coroutine finished
        Debug.Log("Emission Value animation completed!");
    }



   

    
    
    
}


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ClockVisuals : MonoBehaviour,IInteractable
{
    [SerializeField]
    Material clockMaterial;
    [SerializeField]
    Light light;


    [SerializeField,Range(0.05f,float.MaxValue)] float duration = 1.0f;

    private bool isHighlighted = false;
    private bool isHighlightingNow = false;
    private bool isUnHighlightingNow  =false;

    private void Start()
    {
        clockMaterial = GetComponent<Renderer>().material;
        clockMaterial.SetColor("_EmissionColor", Color.black);
    }

    public void  HighlightClock()
    {


    

        if (!isHighlighted)
        {
            isHighlighted = true;
            light.intensity = 150;
            
            if(!isHighlightingNow)
                StartCoroutine(smoothIncrease());
                    
                  


        }

    }
    public void UnHighlight()
    {
        if(isHighlighted)
        {
            light.intensity = 30;
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
              
            Color newValue = Color.Lerp(clockMaterial.color, Color.white, startValue);

           
            clockMaterial.SetColor("_EmissionColor", newValue);
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isHighlightingNow = false;
            
      
     }


    private IEnumerator smoothDecrease()
    {

        isUnHighlightingNow = true;
        float startValue = 100.0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            Color newValue = Color.Lerp(clockMaterial.color, Color.black, startValue);
            clockMaterial.SetColor("_EmissionColor", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isUnHighlightingNow = false;

    }



    public void OnSelection()
    {
        HighlightClock();
    }
    public void OnPressLeftClick()
    {
        GameLogic.instance.NextTour();
    }
    public void OnPressRightClick()
    {

    }

    public void OnDeSelection()
    {
        UnHighlight();
    }




}


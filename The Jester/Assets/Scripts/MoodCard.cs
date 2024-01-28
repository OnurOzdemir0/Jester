using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoodCard : MonoBehaviour,IInteractable
{
    [SerializeField] private ParticleSystem candleFireParticle;
    [SerializeField] private Light candleLight;

    bool isRevealed = false;
   

    int mood;
   

    [SerializeField] private GameLogic.MoodType type;
    Color candleColor;
    private Vector3 initScale;
    bool isAnimating = false;
    private void Awake()
    {   
        initScale = transform.localScale;

    }
    private void Start()
    {   switch(type)
            {
            case GameLogic.MoodType.PHY:
                isRevealed = GameLogic.instance.m_phyMood.isRevealed;
                if(isRevealed) mood = GameLogic.instance.m_phyMood.mood;
                break;
            case GameLogic.MoodType.SAT:
                isRevealed = GameLogic.instance.m_satMood.isRevealed;
                if (isRevealed) mood = GameLogic.instance.m_satMood.mood;
                break;
            case GameLogic.MoodType.WORD:
                isRevealed = GameLogic.instance.m_wrdMood.isRevealed;
                if (isRevealed) mood = GameLogic.instance.m_wrdMood.mood;
                break;
            }
        if(isRevealed)
        {
            switch (mood)
            {
                case -1:
                    candleColor = Color.red;
                    break;


                case 0:
                    candleColor = Color.gray;
                    break;

                case 1:
                    candleColor = Color.green;
                    break;
            }
            candleLight.color = candleColor;
            candleLight.intensity = 2;
            candleFireParticle.Play();
        }
        else
        {
            candleFireParticle.Stop();
        }
            
    }
    public void OnSelection()
    {   if(!isAnimating && !isRevealed)
        {
            isAnimating = true;
            transform.DOScale(initScale * 1.2f, 0.2f);
        }
  
    }
    public void Reset()
    {
        candleFireParticle.Stop();
        isRevealed = false;
        candleLight.intensity = 0;
    }
    public void ForceReveal(GameLogic.MoodType type)
    {
        switch (type)
        {
            case GameLogic.MoodType.PHY:
                mood = GameLogic.instance.m_phyMood.mood;
                break;
            case GameLogic.MoodType.SAT:
                mood = GameLogic.instance.m_satMood.mood;
                break;
            case GameLogic.MoodType.WORD:
                mood = GameLogic.instance.m_wrdMood.mood;
                break;
        }

        switch (mood)
        {
            case -1:
                candleColor = Color.red;
                break;


            case 0:
                candleColor = Color.gray;
                break;

            case 1:
                candleColor = Color.green;
                break;
        }
        candleLight.color = candleColor;
        candleLight.intensity = 2;
        candleFireParticle.Play();
        isRevealed = true;
    }
    public void OnPressLeftClick()
    {
        if (!GameLogic.instance.canReveal) return;

        GameLogic.instance.canReveal = false;
        switch (type)
        {
            case GameLogic.MoodType. PHY:
                mood = GameLogic.instance.m_phyMood.mood;
                
                break; 
            case GameLogic.MoodType.SAT:
                mood = GameLogic.instance.m_satMood.mood;
                break; 
            case GameLogic.MoodType.WORD:
                mood = GameLogic.instance.m_wrdMood.mood;
                break;
        }

        switch (mood)
        {
            case -1:
                candleColor = Color.red;
                break;

                
            case 0:
                candleColor = Color.gray;
                break;
                
            case 1:
                candleColor = Color.green;
                break;
        }
        candleLight.color = candleColor;
        candleLight.intensity = 2;
        candleFireParticle.Play();
        isRevealed = true;

    }
    public void OnPressRightClick()
    {

    }

    public void OnDeSelection()
    {
        if (isAnimating)
        {
            isAnimating = false;
            transform.DOScale(initScale, 0.2f);
        }
    }
}

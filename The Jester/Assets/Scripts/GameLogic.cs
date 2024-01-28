using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    private static GameLogic _logic;

    public static GameLogic _logicInstance
    {
        get
        {
            if (_logic == null)
            {
                _logic = FindObjectOfType<GameLogic>();
                if (_logic == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameLogic";
                    _logic = obj.AddComponent<GameLogic>();
                }
            }
            return _logic;
        }
    }

    void Awake()
    {
        if (_logic == null)
        {
            _logic = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    int playerHealth = 3; 
    int tourScore = 0;
    int generalScore = 0;
    int toureCount = 0;
    int moodBonus = 1;
    bool canPlayMagicCard = true;


    //AI

    public struct KingMood
    {
        public int mood;
        public int fixedTourCount;
        public bool isRevealed;
        // maybe add type ? 

        public void setMood(int mood)
        {
            if (fixedTourCount == 0)
            {
                this.mood = mood;
            }
           
           
        }
 
    }

    public void PlayMagicCard()
    {

    }
    public enum MoodType
    {
        PHY,SAT,WORD
    }

    public KingMood phyMood = new KingMood { mood = 0, fixedTourCount = 2 , isRevealed = false};
    public KingMood satMood = new KingMood { mood = 0, fixedTourCount = 2 , isRevealed = false };
    public KingMood wrdMood = new KingMood { mood = 0, fixedTourCount = 2 , isRevealed = false };

    private void UpdateMood(){
        int selection = Random.Range(1, 4);

        switch (selection)
        {
            case 1: phyMood.setMood(1);
                    satMood.setMood(Random.Range(-1, 1));
                    wrdMood.setMood(Random.Range(-1, 1));
                    break;
            case 2: satMood.setMood(1);
                phyMood.setMood(Random.Range(-1, 1));
                    wrdMood.setMood(Random.Range(-1, 1)); 
                    break;
            case 3: wrdMood.setMood(1);
                    phyMood.setMood(Random.Range(-1, 1));
                    satMood.setMood(Random.Range(-1, 1)); 
                    break;
        }

    }
    public bool RevealMood(MoodType type)
    {
       switch (type)
        {
            case MoodType.PHY:
                if (!phyMood.isRevealed)
                {
                    phyMood.isRevealed = true;
                    return true;
                }
                break;
            case MoodType.SAT:
                if (!satMood.isRevealed)
                {
                    satMood.isRevealed = true;
                    return true;
                }
                break;
            case MoodType.WORD:
                if (!wrdMood.isRevealed)
                {

                    wrdMood.isRevealed = true;
                    return true;
                }
                break;
        }

        return false;

    }
    public bool isMoodRevealed(MoodType type)
    {
        switch (type) { 
         case MoodType.PHY:

            return phyMood.isRevealed;


        case MoodType.SAT:

            return satMood.isRevealed;

        case MoodType.WORD:

            return wrdMood.isRevealed = true;


        }
        Debug.Log("mood null given inside isMoodRevealed");
        return false;

    }
    public void RevealMoodRandom()
    {
        int selection = Random.Range(0, 3);
        
        List<int> unRevealeds = new List<int>();

        switch (selection)
        {
            case 0:
                if (!phyMood.isRevealed)
                {
                    unRevealeds.Add(0);
                }
               
                break;
            case 1:
                if (!satMood.isRevealed)
                {
                    unRevealeds.Add(1);
                }
                break;
            case 2:
                if (!wrdMood.isRevealed)
                {
                    unRevealeds.Add(2);
                }
                break;

        }


        int randomIndex = Random.Range(0, unRevealeds.Count);






        switch (unRevealeds[randomIndex])
        {
            case 0:
                if (!phyMood.isRevealed)
                {
                    unRevealeds.Add(0);
                }

                break;
            case 1:
                if (!satMood.isRevealed)
                {
                    unRevealeds.Add(1);
                }
                break;
            case 2:
                if (!wrdMood.isRevealed)
                {
                    unRevealeds.Add(2);
                }
                break;

        }


    }


    void Start()
    {
        setupGame();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void setupGame(){
        
        UpdateMood();                       //only for the first setup, update mood immediately

    }

   
    public void handleAttack(MoodType type)
    {
        switch (type)
        {
            case MoodType.PHY:
                int phy_result = phyMood.mood * moodBonus;
                if(phy_result < 0)
                {
                    DamageToPlayer(phy_result);
                }
                else
                {
                    GainPoint(phy_result);
                }
                break;
            case MoodType.SAT:
                int sat_result = satMood.mood * moodBonus;
                if (sat_result < 0)
                {
                    DamageToPlayer(sat_result);
                }
                else
                {
                    GainPoint(sat_result);
                }

                break;
            case MoodType.WORD:
                int word_result = wrdMood.mood * moodBonus;
                if (word_result < 0)
                {
                    DamageToPlayer(word_result);
                }
                else
                {
                    GainPoint(word_result);
                }

                break;
        }
    }


    private void GainPoint(int point)
    {

        tourScore += point;
        if(tourScore>=3)
        {
            WinGame();

        }
        generalScore += point;
        if(generalScore >= 5)
        {
            WinGame();
        }
    }
    private void DamageToPlayer(int damage)
    {
        // damage is negative
        if(Mathf.Abs(damage) >= playerHealth)
        {
            playerHealth = 0;
            LooseGame();
        }
        else
        {
            playerHealth += damage;
        }
    }
    private void NextTour(){
        toureCount++;
        if(toureCount > 10)
        {
            LooseGame();
        }
        tourScore = 0;
        moodBonus = 1;
        canPlayMagicCard = true;
        UpdateMood();
    }

    private void LooseGame()
    {

    }
    private void WinGame()
    {
        
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    public static GameLogic instance { get; private set; }

    [SerializeField] private Transform playerTransform_;

    public Transform playerTransform { get { return playerTransform_; } }
    int playerHealth = 3;
    int tourScore = 0;
    int generalScore = 0;
    int tourCount = 0;
    int moodBonus = 1;
    bool canPlayMagicCard = true;
    bool isBarrierActive = false;
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
                fixedTourCount = 2;
                this.mood = mood;
            }


        }

    }

    public enum MoodType
    {
        PHY, SAT, WORD
    }

    private KingMood phyMood = new KingMood { mood = 0, fixedTourCount = 0, isRevealed = false };
    private KingMood satMood = new KingMood { mood = 0, fixedTourCount = 0, isRevealed = false };
    private KingMood wrdMood = new KingMood { mood = 0, fixedTourCount = 0, isRevealed = false };

    public KingMood m_phyMood  {get { return phyMood; } }
    public KingMood m_satMood { get { return satMood; } }
    public KingMood m_wrdMood { get { return wrdMood; } }

    [SerializeField]
    private Transform clockPivot;

    [SerializeField]
    private Collider clockCollider;

    void Awake()
    {
        if (instance!=  null && instance != this)   
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    void Start()
    {
        setupGame();


    }

    //AI
    void Update()
    {
       
    }

    private void setupGame()
    {

        playerHealth = 3;
        tourScore = 0;
        generalScore = 0;
        tourCount = 0;
        moodBonus = 1;
        canPlayMagicCard = true;
        isBarrierActive = false;
        InitClock();
        UpdateMood();
        CardManager.instance.SetupDeck();

    }
    private void InitClock()
    {
        clockPivot.localEulerAngles = new Vector3(0f,0f,60f);

    }

    private void HandleClock()
    { 
        if(clockPivot.localEulerAngles.z + 30f > 360)
        {
            // clockPivot.localEulerAngles = Vector3.zero;
            clockCollider.enabled = false;
            clockPivot.DOLocalRotate(Vector3.zero, 0.1f).onComplete = () => { clockCollider.enabled = true; };
        }
        else
        {
            Vector3 nextRotation = new Vector3(0f, 0f, clockPivot.localEulerAngles.z + 30f);
            clockCollider.enabled = false;
            clockPivot.DOLocalRotate(nextRotation, 0.1f).onComplete = () => { clockCollider.enabled = true; };
        }
        
    }
   
    public void NextTour()
    {
        tourCount++;
        HandleClock();
        if (tourCount > 12)
        {
            LooseGame();
        }
        tourScore = 0;
        moodBonus = 1;
        canPlayMagicCard = true;
        UpdateMood();
        CardManager.instance.FillHand();
    }

    private void LooseGame()
    {
        Debug.Log("Lost the game");
    }
    private void WinGame()
    {
        Debug.Log("Won  the game");
    }

    private void UpdateMood(){
        int selection = Random.Range(0, 3);

        switch (selection)
        {
            case 0: phyMood.setMood(1);
                    satMood.setMood(Random.Range(-1, 1));
                    wrdMood.setMood(Random.Range(-1, 1));
                    
                    break;
            case 1: satMood.setMood(1);
                    phyMood.setMood(Random.Range(-1, 1));
                    wrdMood.setMood(Random.Range(-1, 1)); 
                    break;
            case 2: wrdMood.setMood(1);
                    phyMood.setMood(Random.Range(-1, 1));
                    satMood.setMood(Random.Range(-1, 1)); 
                    break;
        }
        Debug.Log("phymood : " + phyMood.mood);
        Debug.Log("satMood : " + satMood.mood);
        Debug.Log("wrdMood : " + wrdMood.mood);
    }


    public bool handleAttack(MoodType type)
    {


        switch (type)
        {
            case MoodType.PHY:
                int phy_result = phyMood.mood * moodBonus;
                if (phy_result < 0)
                {
                    DamageToPlayer(phy_result);
                }
                else
                {
                    GainPoint(phy_result);
                }
                return true;

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
                return true;

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
                return true;


        }
        return false;
    }

    private void GainPoint(int point)
    {

        tourScore += point;
        Debug.Log("tour point gained: " + point + "\n" + "total tourScore :  " + tourScore);
        if (tourScore >= 3)
        {
            WinGame();

        }
        generalScore += point;
        Debug.Log("generalScore point gained: " + point + "\n" + "generalScore tourScore :  " + generalScore);
        if (generalScore >= 5)
        {
            WinGame();
        }
    }
    private void DamageToPlayer(int damage)
    {
        if (isBarrierActive)
        {
            Debug.Log("Barrier was activated so damage emitted");
            isBarrierActive = false;
            return;
        }
        // damage is negative
        if (Mathf.Abs(damage) >= playerHealth)
        {
            Debug.Log("damage dealt by king and you die !");
            playerHealth = 0;
            LooseGame();
        }
        else
        {
            Debug.Log("damage dealt by king");
            playerHealth += damage;
        }
    }


    public bool getCanPlayMagicCard() { return canPlayMagicCard; }
    public void PlayMagicCard()
    {
        canPlayMagicCard = false;
        Debug.Log("cannot play magic card more this round");
        
    }
    public void UpdateMoodBonus()
    {
        moodBonus = 2;
        Debug.Log("mood bonus added: " + moodBonus);
    }


    public bool RevealMood(MoodType type)
    {
       switch (type)
        {
            case MoodType.PHY:
                if (!phyMood.isRevealed)
                {
                    phyMood.isRevealed = true;
                    Debug.Log("phyMood revealed");
                    return true;
                }
                break;
            case MoodType.SAT:
                if (!satMood.isRevealed)
                {
                    satMood.isRevealed = true;
                    Debug.Log("satMood revealed");
                    return true;
                }
                break;
            case MoodType.WORD:
                if (!wrdMood.isRevealed)
                {

                    wrdMood.isRevealed = true;
                    Debug.Log("wrdMood revealed");
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
    public bool RevealMoodRandom()
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
        if (unRevealeds.Count == 0) return false;

        int randomIndex = Random.Range(0, unRevealeds.Count);






        switch (unRevealeds[randomIndex])
        {
            case 0:
                if (!phyMood.isRevealed)
                {
                    unRevealeds.Add(0);
                    Debug.Log("phyMood Randomly  revealed ");
                    return true;
                }

                break;
            case 1:
                if (!satMood.isRevealed)
                {
                    unRevealeds.Add(1);
                    Debug.Log("satMood Randomly  revealed ");
                    return true;
                }
                break;
            case 2:
                if (!wrdMood.isRevealed)
                {
                    unRevealeds.Add(2);
                    Debug.Log("wrdMood Randomly  revealed ");
                    return true; 
                }
                break;

        }
        return false;


    }



    // Update is called once per frame
    
    
   
    public void ActivateBarrier()
    {
        isBarrierActive = true;
        Debug.Log(" barrier activated");
    }
    
    
    public bool fixTheMood(MoodType type)
    {

        switch (type) { 
        case MoodType.PHY:
                phyMood.fixedTourCount++;
                Debug.Log(" phy Mood fixed ");
                return true;

        case MoodType.SAT:
                satMood.fixedTourCount++;
                Debug.Log(" satMood Mood fixed ");
                return true;

        case MoodType.WORD:
            wrdMood.fixedTourCount++;
                Debug.Log(" wrdMood Mood fixed ");
                return true;
        
        }

        return false;
    }


   
}

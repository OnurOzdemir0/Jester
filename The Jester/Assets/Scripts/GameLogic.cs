using System.Collections;
using System.Collections.Generic;
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
    int currentScore = 0; //ranging from -3 to 3
    int generalScore = 0;

    int roundCount = 0;

//AI
    List<int> mood = new List<int> {0, 0, 0}; // [0] PHY, [1] SAT, [2] WORD   each ranges from -1 to 1

    private void UpdateMood(){

        for(int i = 0; i < 3; i++){
            int rand = Random.Range(-1,1);
            
            while(mood.Contains(rand) && rand == 1){
                rand = Random.Range(-1,1);
            }

            mood[i] = rand;

            Debug.Log(i + ": " + mood[i]);
        }
        Debug.Log("--------------");
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
        currentScore = 0;
        roundCount = 0;
        mood = new List<int> {0, 0, 0}; 
        UpdateMood();                       //only for the first setup, update mood immediately

    }

    public void handleAttack(int attackType)
    {
        currentScore += mood[attackType];
        if(currentScore == 3)
            winRound();

        if(currentScore == -3)
            loseRound();
        

        if(roundCount%2 == 0)
            UpdateMood();
    }

    private void winRound(){
        generalScore++;
        roundCount = 0;
        currentScore = 0;
        UpdateMood();
    }

    private void loseRound(){
        generalScore--;
        roundCount = 0;
        currentScore = 0;
        UpdateMood();
    }

}

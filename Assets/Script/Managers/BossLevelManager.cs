using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ResultUI
{
    public GameObject VictoryUI;
    public GameObject DefeatUI;
}

[System.Serializable]
public class ProblemUI
{
    public Text FirstDigitText;
    public Text SecondDigitText;
    public Image OperationImage;
}

[System.Serializable]
public class GameUI
{
    public Text PlayerLivesText;
    public Text BossLivesText;
    public Slider Timer;
}

[System.Serializable]
public class ChoicesUI
{
    public Text Choice1;
    public Text Choice2;
    public Text Choice3;
}

[System.Serializable]
public class CharacterAnimations
{
    public Animator PlayerAnim;
    public Animator BossAnim;
}

public class BossLevelManager : MonoBehaviour
{
    private enum GameState { NewRound, Ongoing, Finished}

    public static BossLevelManager Instance { get; private set; }

    public OpAssets OperationAssets;
    public ResultUI ResultAssets;
    public ProblemUI ProblemAssets;
    public GameUI GameAssets;
    public ChoicesUI ChoicesAssets;
    public CharacterAnimations AnimationAssets;

    public bool RunTimer = false;
    public int PlayerLives = 3;
    public int BossLives = 10;
    public float RoundTimer = 10f;
    public string NextLevel = null;

    private GameState State = GameState.NewRound;
    private float RunningTime = 0f;
    private Operations Operation = Operations.Addition;
    private int CorrectAnswer = 0;
    private int CorrectChoice = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameAssets.PlayerLivesText.text = PlayerLives.ToString("0");
        GameAssets.BossLivesText.text = BossLives.ToString("0");

        RunningTime = RoundTimer;

        GameAssets.Timer.maxValue = RoundTimer;
        GameAssets.Timer.minValue = 0;
    }

    private void Update()
    {
        if(State == GameState.NewRound)
        {
            //Reset Values
            CorrectAnswer = 0;
            CorrectChoice = 0;

            //Reset timer
            RunningTime = RoundTimer;
            GameAssets.Timer.value = RunningTime;
            RandomizeProblem();


            State = GameState.Ongoing;
        }

        else if(State == GameState.Ongoing)
        {
            if (RunTimer)
            {
                RunningTime -= Time.deltaTime;
                GameAssets.Timer.value = RunningTime;

                //Check if timer hits 0 or lower
                if (RunningTime < 0)
                {
                    PlayerHit();
                }

            }
        }

        else if(State == GameState.Finished)
        {
            RunTimer = false;
            AnimationAssets.PlayerAnim.enabled = false;
            AnimationAssets.BossAnim.enabled = false;
        }

        
    }

    private void RandomizeProblem()
    {
        GenerateOperation();
        GenerateDigits();
    }

    private void GenerateOperation()
    {
        int Chance = Random.Range(1, 10);

        //30% chance, addition
        if(Chance <= 3)
        {
            Operation = Operations.Addition;
            ProblemAssets.OperationImage.sprite = OperationAssets.Addicus;
        }

        //30% chance, subtraction
        else if (Chance > 3 && Chance <= 6)
        {
            Operation = Operations.Subtraction;
            ProblemAssets.OperationImage.sprite = OperationAssets.Minium;
        }

        //20% chance, multiplication
        else if(Chance > 6 && Chance <= 8)
        {
            Operation = Operations.Multiplication;
            ProblemAssets.OperationImage.sprite = OperationAssets.Multifly;
        }

        //20% chance, division
        else if(Chance > 8)
        {
            Operation = Operations.Division;
            ProblemAssets.OperationImage.sprite = OperationAssets.TheVoid;
        }
    }

    private void GenerateDigits()
    {
        int firstDigit = 0;
        int secondDigit = 0;

        int Chance = Random.Range(1, 10);
        int OddEvenOffset = 0;

        //60% chance, Generate odd numbers
        if (Chance <= 4)
        {
            OddEvenOffset = 1;
        }
        //40% chance, Generate even numbers
        else if (Chance > 4)
        {
            OddEvenOffset = 2;
        }

        //If boss has more than or equal to 80% health, randomize between -9 and 9 (if odd) or -8 and 8 (if even)
        if (BossLives >= 8)
        {
            firstDigit = (Random.Range(-4, 5) * 2) - OddEvenOffset;
            secondDigit = (Random.Range(-4, 5) * 2) - OddEvenOffset;
        }

        //If boss has less than 80% health, randomize between -99 and 99 (if odd) or -98 and 98 (if even)
        else
        {
            firstDigit = (Random.Range(-49, 50) * 2) - OddEvenOffset;
            secondDigit = (Random.Range(-49, 50) * 2) - OddEvenOffset;
        }

        ProblemAssets.FirstDigitText.text = firstDigit.ToString("0");
        ProblemAssets.SecondDigitText.text = secondDigit.ToString("0");

        GenerateAnswers(firstDigit, secondDigit);
    }

    private void GenerateAnswers(int firstDigit, int secondDigit)
    {
        int offset = firstDigit - secondDigit;
        int positiveOffset = firstDigit + offset;
        int negativeOffset = firstDigit - offset;

        int answer1 = 0;
        int answer2 = 0;

        switch (Operation)
        {
            case Operations.Addition:
                CorrectAnswer = firstDigit + secondDigit;
                answer1 = positiveOffset + secondDigit;
                answer2 = negativeOffset + secondDigit;
                break;

            case Operations.Subtraction:
                CorrectAnswer = firstDigit - secondDigit;
                answer1 = positiveOffset - secondDigit;
                answer2 = negativeOffset - secondDigit;
                break;

            case Operations.Multiplication:
                CorrectAnswer = firstDigit * secondDigit;
                answer1 = positiveOffset * secondDigit;
                answer2 = negativeOffset * secondDigit;
                break;

            case Operations.Division:
                CorrectAnswer = firstDigit / secondDigit;
                answer1 = positiveOffset / secondDigit;
                answer2 = negativeOffset / secondDigit;
                break;
        }

        //Randomize choices
        ChoicesAssets.Choice1.text = answer1.ToString("0");
        ChoicesAssets.Choice2.text = CorrectAnswer.ToString("0");
        ChoicesAssets.Choice3.text = answer2.ToString("0");

        CorrectChoice = 2;
    }

    private void CloseAllUI()
    {
        ProblemAssets.FirstDigitText.transform.parent.gameObject.SetActive(false);
        ProblemAssets.SecondDigitText.transform.parent.gameObject.SetActive(false);
        ProblemAssets.OperationImage.gameObject.SetActive(false);

        GameAssets.PlayerLivesText.transform.parent.gameObject.SetActive(false);
        GameAssets.BossLivesText.transform.parent.gameObject.SetActive(false);
        GameAssets.Timer.transform.parent.gameObject.SetActive(false);

        ChoicesAssets.Choice1.transform.parent.gameObject.SetActive(false);
        ChoicesAssets.Choice2.transform.parent.gameObject.SetActive(false);
        ChoicesAssets.Choice3.transform.parent.gameObject.SetActive(false);
    }

    public void PlayerHit()
    {
        PlayerLives -= 1;
        GameAssets.PlayerLivesText.text = PlayerLives.ToString("0");

        AnimationAssets.PlayerAnim.SetTrigger("Damaged");

        State = GameState.NewRound;

        //Player lose
        if (PlayerLives == 0)
        {
            State = GameState.Finished;
            //SoundManager.Instance.PlaySFX(SFXClip.Lose);
            CloseAllUI();
            Instantiate(ResultAssets.DefeatUI);
        }
    }

    public void BossHit()
    {
        BossLives -= 1;
        GameAssets.BossLivesText.text = BossLives.ToString("0");

        AnimationAssets.BossAnim.SetTrigger("Damaged");

        State = GameState.NewRound;

        //Player win
        if (BossLives == 0)
        {
            State = GameState.Finished;
            //SoundManager.Instance.PlaySFX(SFXClip.Win);
            CloseAllUI();
            Instantiate(ResultAssets.VictoryUI);
        }
    }

    public void CheckChoice(int choice)
    {
        //Correct answer is chosen, damage boss
        if(choice == CorrectChoice)
        {
            BossHit();
        }
        //Wrong answer is chosen, damage player
        else
        {
            PlayerHit();
        }
    }
}

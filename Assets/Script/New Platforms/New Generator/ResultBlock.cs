using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResultAssets
{
    public Sprite CorrectAnswer;
    public Sprite WrongAnswer;
}

public class ResultBlock : DigitBlock
{
    public ResultAssets ResultBlockAssets;

    public void SetResult(bool isAnswerCorrect)
    {
        if (isAnswerCorrect)
            sprite.sprite = ResultBlockAssets.CorrectAnswer;
        else
            sprite.sprite = ResultBlockAssets.WrongAnswer;
    }
}

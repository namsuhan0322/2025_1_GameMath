using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public Text resultText;

    private enum Grade { C, B, A, S }

    Grade DrawGrade(bool guaranteedAOrHigher = false)
    {
        if (guaranteedAOrHigher)
        {
            float rand = Random.value;
            return rand < 0.5f ? Grade.A : Grade.S; // 50% A, 50% S
        }

        float random = Random.value;

        if (random < 0.4f) return Grade.C;           // 40%
        else if (random < 0.7f) return Grade.B;      // 30%
        else if (random < 0.9f) return Grade.A;      // 20%
        else return Grade.S;                         // 10%
    }

    public void DrawOnce()
    {
        Grade grade = DrawGrade();
        resultText.text = $"1회 뽑기 결과: {grade} 등급";
    }

    public void DrawTen()
    {
        List<Grade> results = new List<Grade>();

        for (int i = 0; i < 9; i++)
        {
            results.Add(DrawGrade());
        }

        // 마지막 10번째는 A등급 이상 확정
        results.Add(DrawGrade(guaranteedAOrHigher: true));

        string resultStr = "10회 뽑기 결과:\n";
        for (int i = 0; i < results.Count; i++)
        {
            resultStr += $"{i + 1}회차: {results[i]} 등급\n";
        }

        resultText.text = resultStr;
    }
}

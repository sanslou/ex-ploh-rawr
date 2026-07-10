using System;
using UnityEngine;


/*
 * 
 * S => 100%
 * A+ => 97%
 * A => 95%
 * A- => 90%
 * B+ => 87%
 * B => 85%
 * B- => 80%
 * C+ => 77%
 * C => 75%
 * C- => 70%
 * D+ => 67%
 * D => 65%
 * D- => 60%
 * F => 59%
 * 
 * 
 */
public class QA
{
    public string QUESTION;
    public string ANSWER;
    public string savedAnswer;

    public virtual bool Answer(string answer) {
        this.savedAnswer = answer;
        return (this.ANSWER == answer);
    }

}

public class MultipleChoices : QA {
    public string[] choices;
    public MultipleChoices(string question, string[] choices, int correct)
    {
        base.QUESTION = question;
        this.choices = choices;
        base.ANSWER = choices[correct];
    }

    public bool AnswerChoice(int choice) {
        return Answer(this.choices[choice]);
    }

    public static string[] Shuffle(string[] here) {
        string[] res = new string[here.Length];

        for (int i = 0; i < res.Length; i++) {
            System.Random r = new System.Random();
            int ri = r.Next(0, here.Length);

            string e1 = here[i];
            string e2 = here[ri];

            here[i] = e2;
            here[ri] = e1;
        }

        return res;
    }
}

public class Trivia : QA {
    public bool isFact;

    public Trivia(string question, bool trueOrFalse) {
        this.QUESTION = question;
        this.isFact = trueOrFalse;
        base.ANSWER = isFact.ToString().ToLower();
    }

    public bool AnswerFact(bool answer) {
        return Answer(answer.ToString());
    }
    public bool AnswerFact(string answer)
    {
        return Answer(answer.ToLower());
    }
    
    public static bool ParseBool(string here) {
        here = here.ToLower();
        return bool.Parse(here);
    }
}

public class Identify : QA {
    bool isCaseSensitive;
    public Identify(string question, string answer, bool isCaseSensitive) {
        base.QUESTION = question;
        base.ANSWER = answer;
        this.isCaseSensitive = isCaseSensitive;
    }

    public bool AnswerIdentity(string answer) {
        if (isCaseSensitive) return Answer(answer);
                             return base.ANSWER.ToLower() == answer.ToLower();
    }


}

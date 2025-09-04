using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Interactions
{
    public class CheckAnswers
    {
        [SerializeField, Min(1)] private int nbAnswersRequired;
        private List<bool> answers = new List<bool>();


        public void CheckAnswer(bool answer) {

            answers.Add(answer);

            if (answers.Count >= nbAnswersRequired)
            {
                if (answers.All(userAnswer => userAnswer == true))
                {
                    GameLoopManager.Instance.LoadNextScene();
                }
                else 
                { 
                    GameLoopManager.Instance.LoadMenuScene();   
                }
            }
        }


        public void checkBarrel() { 
        

        }
    }
}

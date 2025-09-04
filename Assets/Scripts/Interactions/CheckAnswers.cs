using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts.Interactions
{
    public class CheckAnswers : MonoBehaviour
    {
        [SerializeField, Min(1)] private int nbAnswersRequired = 1;
        private List<bool> answers = new List<bool>();

        public void CheckAnswer(bool answer) 
        {
            if (answers.Count >= nbAnswersRequired)
            {
                return;
            }
            answers.Add(answer);
            
            if (answers.Count >= nbAnswersRequired)
            {
                if (answers.All(userAnswer => userAnswer))
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

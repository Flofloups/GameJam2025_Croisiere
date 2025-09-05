using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


namespace Assets.Scripts.Interactions
{
    [Serializable]
    public struct Feedback
    {
        public float timer;
        public UnityEvent FeedbackEvent;
    }
    
    public class CheckAnswers : MonoBehaviour
    {
        [SerializeField, Min(1)] private int nbAnswersRequired = 1;
        private List<bool> answers = new List<bool>();
        
        [SerializeField] ResultFeedback _resultFeedback;
        [SerializeField] NextSceneButton _nextSceneButton;
        [SerializeField] List<Feedback> _feedbacks = new List<Feedback>();

        private float _timer = -1f;
        private int _index = -1;

        public void CheckAnswer(bool answer) 
        {
            if (answers.Count >= nbAnswersRequired)
            {
                return;
            }
            answers.Add(answer);
            
            if (answers.Count >= nbAnswersRequired)
            {
                bool isSuccess = answers.All(userAnswer => userAnswer);
                _resultFeedback?.ShowResultFeedback(isSuccess);
                _nextSceneButton?.SetVictory(isSuccess);
                if (isSuccess && _feedbacks.Count > 0)
                {
                    _timer = 0f;
                    _index = 0;
                }
            }
        }

        public void Update()
        {
            if (_index < 0 || _timer < 0) return;
            
            _timer += Time.deltaTime;
            while (_index < _feedbacks.Count && _feedbacks[_index].timer <= _timer)
            {
                _feedbacks[_index].FeedbackEvent?.Invoke();
                _timer -= _feedbacks[_index].timer;
                _index++;
            }

            if (_index >= _feedbacks.Count)
            {
                _index = -1;
                _timer = -1f;
            }
        }
    }
}

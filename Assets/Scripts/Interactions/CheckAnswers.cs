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
        [SerializeField] List<Feedback> _victoryFeedbacks = new List<Feedback>();
        [SerializeField] List<Feedback> _defeatFeedbacks = new List<Feedback>();
        
        private bool _isVictory = false;
        
        private List<Feedback> _currentFeedbacks => _isVictory ? _victoryFeedbacks : _defeatFeedbacks;

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
                _isVictory = answers.All(userAnswer => userAnswer);
                _resultFeedback?.ShowResultFeedback(_isVictory);
                _nextSceneButton?.SetVictory(_isVictory);
                if (!_isVictory)
                {
                    _nextSceneButton?.Display();
                }
                if (_isVictory && _currentFeedbacks.Count > 0)
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
            if (_index < _currentFeedbacks.Count && _currentFeedbacks[_index].timer <= _timer)
            {
                _currentFeedbacks[_index].FeedbackEvent?.Invoke();
                _timer = 0;
                _index++;
            }

            if (_index >= _currentFeedbacks.Count)
            {
                _index = -1;
                _timer = -1f;
            }
        }
    }
}

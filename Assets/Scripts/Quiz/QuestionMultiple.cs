using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Answer
{
    [SerializeField] private string _info;
    public string info {get {return _info;} }

    [SerializeField] private bool _isCorrect;
    public bool isCorrect {get {return _isCorrect;}}
    
}
public class QuestionMultiple : ScriptableObject
{
    [SerializeField] private string _info = string.Empty;
    public string info {get {return _info;} }

    [SerializeField] Answer[] _answers = null;
    public Answer[] answers{get {return _answers;} }
}

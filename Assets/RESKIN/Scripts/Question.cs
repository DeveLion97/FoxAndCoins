using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class Question : ScriptableObject
{
    public string questionText;
    public string[] options;
    public int correctOptionIndex;
}

using UnityEngine;

public class TemplateLevel : MonoBehaviour
{
    [SerializeField] private Transform _nextLvlPoint;
    [SerializeField] private Transform _startLvlPoint;
    public Transform NextLvlPoint => _nextLvlPoint;

    public Transform StartLvlPoint => _startLvlPoint;
}

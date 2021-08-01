using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GenerationMap : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelTemplates;
    [SerializeField] private int _countRoom;
    [SerializeField] private GameObject _startTemplate, _endTemplate;
    private void Start()
    {
        Random random = new Random();
        Transform prevTemplate = Instantiate(_startTemplate, Vector3.zero, Quaternion.identity)
            .GetComponent<TemplateLevel>().NextLvlPoint;
       
        for (int i = 0; i < _countRoom; i++)
        {
            var levelTemplate = _levelTemplates[random.Next(0,_levelTemplates.Count)];
            var instantiate = Instantiate(levelTemplate,prevTemplate.position,Quaternion.identity).GetComponent<TemplateLevel>();

            while (instantiate.StartLvlPoint.rotation != prevTemplate.rotation)
                instantiate.transform.Rotate(Vector3.up * 1f);
            
            prevTemplate=instantiate.GetComponent<TemplateLevel>().NextLvlPoint;
        }

        Instantiate(_endTemplate, prevTemplate.position, Quaternion.identity);
    }
}

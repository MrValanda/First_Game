using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    [SerializeField] private Slider _sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        _sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        ChangeSensitivity( _sensitivitySlider.value);
    }

    public void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity",value);
        PlayerPrefs.Save();
    }
}

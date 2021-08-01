using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         PlayerPrefs.SetInt("LastLvl",SceneManager.GetActiveScene().buildIndex+1);
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
      }
   }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int activeScene;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        activeScene = 0;
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void LoadNextLevel()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene + 1);
               
    }
}

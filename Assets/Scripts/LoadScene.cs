using UnityEngine;

public class LoadScene : MonoBehaviour {

    public string sceneName = "-1";

    void Start()
    {
        if (sceneName == "-1")
        {
            sceneName = Application.loadedLevelName;
        }
    }
    public void Load()
    {
        Application.LoadLevel(sceneName);
    }
}

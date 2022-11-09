using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public void ReloatScene() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
}

using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SceneLoader._instance.LoadScene("Scene A");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SceneLoader._instance.LoadScene("Scene B");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            SceneLoader._instance.LoadScene("Scene C");
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            SceneLoader._instance.LoadScene("Scene D");
        }
    }
}

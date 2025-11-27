using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public APIUploader apiuploader;
    public PlayerHealth playerHealth;       
    public float restartDelay = 5f;            
    public Text warningText; 
    bool isApiCalled = false;

    

    Animator anim;                          
    float restartTimer;                    


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay && !isApiCalled)
            {
                Debug.Log("This is end");
                
                isApiCalled = true; // prevent multiple calls
                apiuploader.SendDataToSomnia();
                
            }
        }
    }
    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }
}
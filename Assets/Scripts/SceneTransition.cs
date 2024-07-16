using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition SceneInstance;
    Animator animator;

    private void Awake()
    {      
        if (SceneInstance == null)
        {
            SceneInstance = this;
            animator = GetComponent<Animator>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        NextLvl(1);
    }
    public void NextLvl(int scene)
    {
        StartCoroutine(Loadlvl(scene));
    }
    public IEnumerator Loadlvl(int scene)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene);
        animator.SetTrigger("Start");
    }
}

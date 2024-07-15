using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
        }
        else
        {
            Destroy(this.gameObject);
        }
        animator = GetComponent<Animator>();
    }
    public IEnumerator Loadlvl(int scene)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene);
        animator.SetTrigger("Start");
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition SceneInstance;
    private Animator _animator;

    private void Awake()
    {      
        if (SceneInstance == null)
        {
            SceneInstance = this;
            _animator = GetComponent<Animator>();
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
        _animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene);
        _animator.SetTrigger("Start");
    }
}
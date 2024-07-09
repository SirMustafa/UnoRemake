using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    public static SetCamera SetCamerainstance;
    private void Awake()
    {
        SetCamerainstance = this;
    }
    public void SetSizeBeginning()
    {
        this.GetComponent<Camera>().DOOrthoSize(5,1);
    }
}

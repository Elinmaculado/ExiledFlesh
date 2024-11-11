using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LifeScreen : MonoBehaviour
{
    [SerializeField] Image lifeImage;


    public void SetAlpha(float alpha){
        alpha = alpha < 0.0000001? 0 : Mathf.Clamp(alpha,0.5f,0.99f);
        Color color = lifeImage.color;
        color.a = alpha;
        lifeImage.color = color;
    }
}

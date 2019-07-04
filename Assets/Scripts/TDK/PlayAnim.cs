using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayAnim : MonoBehaviour
{
    public static PlayAnim instance;
    public static PlayAnim Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }
    public void PlayAnim1(List<List<GameObject>> allObj)
    {   
        float Time  = 0.2f;
        for (int i = 0; i < allObj.Count; i++)
        {
            for (int k = 0; k < allObj[i].Count; k++)
            {

                allObj[i][k].GetComponent<CanvasGroup>().alpha = 0;
                allObj[i][k].GetComponent<RectTransform>().localScale = Vector3.zero;
                allObj[i][k].GetComponent<RectTransform>().rotation = Quaternion.identity;
            }
        }
        for (int i = 0; i < allObj.Count; i++)
        {   
            for (int k = 0; k < allObj[i].Count; k++)
            {
                float tiem = Random.Range(0, 1f);
                allObj[i][k].GetComponent<CanvasGroup>().DOFade(1f, 0.1f).SetDelay(Time);
                allObj[i][k].GetComponent<RectTransform>().DOScale(1f, 0.2f).SetEase(Ease.OutBounce).SetDelay(Time);
                // DOTween.Sequence().Append(tweener1).Append(tweener2);
                Time += 0.02f;
            }
        }
    }
    //竖着出现
    public void PlayAnim2(List<List<GameObject>> allObj,int column)
    {
        float Time = 0.4f;
        for (int i = 0; i < allObj.Count; i++)
        {
            for (int k = 0; k < allObj[i].Count; k++)
            {
                 allObj[i][k].GetComponent<RectTransform>().localScale = Vector3.zero ;
                allObj[i][k].GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        for (int i = 0; i < column; i++)
        {
            for (int k = 0; k < allObj.Count; k++)
            {
                allObj[k][i].GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.InSine).SetDelay(Time);
                allObj[k][i].GetComponent<RectTransform>().DOScale(1f,0.5f).SetEase(Ease.InSine).SetDelay(Time);
            }
            Time += 0.1f;
        }
    }
    public void PlayAnim3(List<List<GameObject>> allObj)
    {
       
        float Time = 0.1f;
        for (int i = 0; i < allObj.Count; i++)
        {
            for (int k = 0; k < allObj[i].Count; k++)
            {

                allObj[i][k].GetComponent<RectTransform>().localScale = Vector3.zero;
            }
        }
        for (int i = 0; i < allObj.Count; i++)
        {
            for (int k = 0; k < allObj[i].Count; k++)
            {
                float tiem = Random.Range(0, 1f);
                Tweener tweener1 = allObj[i][k].GetComponent<RectTransform>().DOScale(0f, 1f).SetDelay(0.3f);
                Tweener tweener2 = allObj[i][k].GetComponent<RectTransform>().DOScale(1f, 0.8f).SetEase(Ease.InOutBack).SetDelay(0.5f + tiem);
                Time += 0.01f;
            }
        }
    }
    public void AnimType4(List<List<GameObject>> allObj)
    {
        float Time = 0.25f;
        List<Tweener> tweenrs = new List<Tweener>();
        for (int i = 0; i < allObj.Count; i++)
        {
            for (int k = 0; k < allObj[i].Count; k++)
            {
                Tweener tweenr = allObj[i][k].GetComponent<RectTransform>().DORotate(new Vector3(90, 0, 0), 0.8f, RotateMode.LocalAxisAdd).SetEase(Ease.InBack).SetDelay(Time);
                allObj[i][k].GetComponent<RectTransform>().DOScale(0f, 0.8f).SetDelay(Time);
                Time += 0.05f;
            }
        }
    }

}

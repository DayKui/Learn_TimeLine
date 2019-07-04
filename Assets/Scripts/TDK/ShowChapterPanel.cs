using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using Cinemachine.Timeline;
using DG.Tweening;
using System;
using System.IO;
using UnityEngine.UI;

public class ShowChapterPanel : MonoBehaviour
{
    public static ShowChapterPanel instance;
    public Button closeBtn;
    public Button openBtn;
    public Button ResetBtn;
    public GameObject chapterPrefab;
    public Transform parentTrans;
    public static bool isRuning = false;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       // AddPrefab();
        CtrlBtn();
        ShowProcess();
    }
    void CtrlBtn()
    {
        closeBtn.onClick.AddListener(ClickCloseBtn);
        ResetBtn.onClick.AddListener(ResetchapterPrefab);
        openBtn.onClick.AddListener(delegate () {
            transform.Find("MoveContent").GetComponent<RectTransform>().DOAnchorPosX(380, 0.1f).SetEase(Ease.Linear).OnComplete(delegate () {
                if (isRuning == true)
                {
                    Time.timeScale = 0;
                }
            });
            openBtn.gameObject.SetActive(false);
        });
    }
    //重置预制体 demo测试用
    void ResetchapterPrefab()
    {
            PlayerPrefs.SetInt("process", 1);
            PlayerPrefs.SetInt("looking", 1);
            ShowProcess();
            ClickInChapterBtn(parentTrans.GetChild(0).gameObject);

    }
    public  void ShowProcess()
    {
        for (int i = 0; i < parentTrans.childCount; i++)
        {
            Transform obj = parentTrans.GetChild(i);
            string name = obj.name;
            Transform processImg = obj.transform.Find("ProcessCon");
            GameObject showObj;
            int num = int.Parse(obj.name);
            bool apl = num > PlayerPrefs.GetInt("process") ? false : true;
            obj.GetComponentInChildren<Button>().interactable = apl;
            for (int j = 0; j < processImg.childCount; j++)
            {
                processImg.GetChild(j).gameObject.SetActive(false);
            }
            if (int.Parse(name) == PlayerPrefs.GetInt("looking"))
            {
                showObj = processImg.transform.Find("Doing").gameObject;
            }
            else
            {
                showObj = int.Parse(name) > PlayerPrefs.GetInt("process") ? processImg.transform.Find("NotAchieved").gameObject : processImg.transform.Find("Achieved").gameObject;
            }
            showObj.SetActive(true);
        }
    }
    void AddPrefab()
    {
        int num = 1;
        List<string> chapterName = TableConfig.instance.GetConfigOneAllName1();
        #region
        //        if (chapterPrefab == null)
        //        {

        //#if UNITY_EDITOR
        //            UnityEditor.EditorApplication.isPlaying = false;
        //#else
        //                            Application.Quit();
        //#endif

        //        }
        #endregion
        for (int i = 0; i < chapterName.Count; i++)
        {
            GameObject clonePre = Instantiate(chapterPrefab, Vector3.zero, Quaternion.identity);
            clonePre.transform.SetParent(parentTrans);
            clonePre.name = num.ToString();
            bool apl = num > PlayerPrefs.GetInt("process") ? false : true;
            clonePre.GetComponentInChildren<Button>().interactable = apl;
            clonePre.transform.Find("ConText").GetComponent<Text>().text = chapterName[i];
            clonePre.transform.Find("NumText").GetComponent<Text>().text = num.ToString();
            clonePre.transform.localScale = Vector3.one;
            num += 1;
        }
    }
    /// <summary>
    /// 点击每个章节进入该章节
    /// </summary>
    /// <param name="obj"></param>
    public void ClickInChapterBtn(GameObject obj)
    {
        int num = int.Parse(obj.name);
        if (num <= GameManager.instance.allPanelPrefab)
        {
                ClickCloseBtn();
                GameManager.instance.CtrlPrefabActive(false ,false );
                StartCoroutine(InvokeGMShowPrefabFun(num));
        }
    }
    IEnumerator InvokeGMShowPrefabFun(int num)
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.ShowPrefab(num);
    }
    void ClickCloseBtn()
    {
        if (isRuning)
        {
            Time.timeScale = 1;
        }
        transform.Find("MoveContent").GetComponent<RectTransform>().DOAnchorPosX(963, 0.1f).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            openBtn.gameObject.SetActive(true);
        });
    }
}


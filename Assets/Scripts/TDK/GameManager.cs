using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using Cinemachine.Timeline;
using DG.Tweening;
using System;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isDisSliderl = false;
    public static GameManager instance;
    public Transform logoPanel;
    public VideoPlayer logoVideoPlayer;
    public GameObject canvas;
    public RectTransform textContent;
    public GameObject showChapters;
    public GameObject scm1;
    public GameObject scm2;
    public GameObject everyChapterShow;
    public int allPanelPrefab =2;
    public CanvasGroup canvasGorup;
    public int sliderShowNum = 1;
    public bool isFirstPlay = true;
    public GameObject slidetCont;
    public bool isEnd=false;
    public int chapterNum =0;
    public bool isSet = false;
    private void Awake()
    {
        PlayerPrefs.SetInt("process", 1);
        PlayerPrefs.SetInt("looking", 1);
        instance =this;
        SetPlayerPref();
    }
    private void Start()
    {
        logoVideoPlayer.loopPointReached += VideoPlayerEnd;
    }
    void VideoPlayerEnd(VideoPlayer player)
    {
        logoPanel.gameObject.SetActive(true);
        Destroy(logoVideoPlayer.gameObject);
    }
    void SetPlayerPref()
    {
        if (PlayerPrefs.GetInt("process") == 0)
        {
            PlayerPrefs.SetInt("process", 1);
        }
        if (PlayerPrefs.GetInt("looking") == 0)
        {
            PlayerPrefs.SetInt("looking", 1);
        }
    }
    /// <summary>
    /// 控制director 的暂停和播放
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="isPause"></param>
    public void CtrlPlayableDirPause(PlayableDirector playable,bool isPause)
    {    
        if (playable!=null && isPause)
        {
            playable.Pause();
        }
        else if (playable!=null&& !isPause )
        {
            playable.Play();
        }
    }
    public void CtrlSlidetContShow(bool isShow)
    {
        slidetCont.gameObject.SetActive(isShow);
        slidetCont.transform.GetChild(0).GetComponent<Slider>().value=0;
        slidetCont.transform.GetChild(1).GetComponent<Slider>().value = 0;
        slidetCont.transform.GetChild(0).gameObject.SetActive(true);
        slidetCont.transform.GetChild(1).gameObject.SetActive(true);
    }
    /// <summary>
    /// 控制预制体的显示
    /// </summary>
    /// <param name="num"></param>
   public void ShowPrefab(int num)
    {
        if (num >allPanelPrefab) return;
        int process = PlayerPrefs.GetInt("process");
        int realProcess= num > process ? num : process;
        SetPlayerProcess(realProcess);
        SetPlayerLooking(num);
        string preNamescm="scm"+num.ToString();
        if (!isFirstPlay)
        {
            CtrlEveryChapterShow(num,preNamescm);
        }
        else
        {
            ShowChapterPanel.isRuning = true;
            CtrlShowPre(preNamescm);
        }
    }
    public void CtrlShowPre(string preNamescm)
    {
       // ShowChapterPanel.instance.ShowProcess();
        if (preNamescm == "scm1")
        {
            CtrlPrefabActive(true,false);
        }
        else if (preNamescm == "scm2")
        {
            CtrlPrefabActive(false, true);
        }
        isFirstPlay = false;
    }
    public void CtrlPrefabActive(bool s1,bool s2)
    {
        scm1.gameObject.SetActive(s1);
        scm2.gameObject.SetActive(s2);
    }
    public void CtrlEveryChapterShow(int num,string preNamescm=null)
    {
        string sNum = "";
        if (num==1)
        {
            sNum = "一";
        }
        if (num == 2)
        {
            sNum = "二";
        }
        if (num==3)
        {
            isEnd = true;
            everyChapterShow.transform .parent .GetComponent<NormalShow>().hasStart = false;
            sNum = "三";

        }
        string con = "第" + sNum + "幕";
        SerializaCon sc ;
        DOTween.KillAll();
        sc = TableConfig.instance.GetTableRowData<SerializaCon>("ConfigOne", num);
        everyChapterShow.transform.Find("DotImg").GetComponent<Image>().DOFade(0.4f, 1).SetEase(Ease.InOutBack).SetLoops(-1);
        everyChapterShow.transform.Find("NumText").GetComponent<Text>().text = "";
        everyChapterShow.transform.Find("Name1").GetComponent<Text>().text = "";
        everyChapterShow.transform.Find("Name2").GetComponent<Text>().text = "";
        everyChapterShow.transform.parent .GetComponent<CanvasGroup>().DOFade(1,1f).SetEase(Ease.Linear);
        everyChapterShow.transform.Find("NumText").GetComponent<Text>().text = con;
        everyChapterShow.transform.Find("Name1").GetComponent<Text>().DOText(sc.name1 + "",1f).SetEase(Ease.Linear).SetDelay(1f).OnComplete(delegate () {
            everyChapterShow.transform.Find("Name2").GetComponent<Text>().DOText(sc.name2 + "", 1.5f).SetEase(Ease.Linear);
            });
    }

    public void CtrlCancasIsShow(bool isShow,int alpha=-1)
    {
        if (alpha!=-1)
        {
            canvasGorup.alpha = alpha;
        }
        canvas.SetActive(isShow);
    }
    //显示奖励内容
    public void CtrlTextCon(List<string> con)
    {
        for (int i = 0; i < con.Count; i++)
        {
            if (string .IsNullOrEmpty(con[i]))
            {
                continue;
            }
            textContent.GetComponent<Text>().text+= con[i] + "\n";
        }
        textContent.gameObject.SetActive(true);
        textContent.DOAnchorPosY( 438f,2f).SetEase(Ease.Linear).OnComplete(delegate() {
            textContent.GetComponent<Text>().text = "";
            textContent.gameObject.SetActive(false);
        });
    }
    public int GetLookingProcess()
    {
        return PlayerPrefs.GetInt("looking");
    }
    public void CtrlShowChapters()
    {
        showChapters.SetActive(true);
    }
   /// <summary>
   /// 设置进度
   /// </summary>
    public void SetPlayerProcess(int process)
    {
        PlayerPrefs.SetInt("process", process);
    }
    /// <summary>
    /// 设置正在浏览的进度
    /// </summary>
    public void SetPlayerLooking(int looking )
    {
        PlayerPrefs.SetInt("looking", looking);
    }
}

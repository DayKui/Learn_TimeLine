using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using DG.Tweening;
using UnityEngine.Playables;
using Spine;
using Spine.Unity;
public class Sc001M002 : MonoBehaviour {

    public GameObject leftBtn;
    public GameObject rigBtn;
    private bool isClickLeftBtn = false;
    private bool isClickRigBtn = false;
    float clickLeftTime = -1;
    float clickRigTime = -1;
    bool hadStartSliderAddCor = false;
    bool canAddSlider = false;
    bool hadStartSliderLesser = false;
    float ctrlSliderDisProcess = 1;
    //  public GameObject sucessImage;
    bool isNeedCtrlSlider = true;
    public Slider leftSlider;
    public Slider rigthSlider;
    public Transform RotationCam;
    public GameObject role;
    public Tweener tween;
    public PlayableDirector director;
    public bool isStartJuQi = false;
    public bool isClickLeftSKBtn;
    public bool isClicikRigSKBtn;
    public GameObject touchContent;
    int clickSkNum = 0;
    public float upTime;
    public float nowTime;
    public PlayableDirector skDirector;
    public GameObject juqiPartical;
    public GameObject redPartical;
    public GameObject noRedPartical;
    public Text sliderPer;
    public bool isShowSlider = false;
    public PlayableDirector caoZupDir;
    public SkeletonAnimation sk;
    // Use this for initialization
    private void OnEnable()
    {
        ResetData();
        EasyButton.On_ButtonDown += On_BtnDown;
        EasyButton.On_ButtonUp += On_BtnUp;
        EasyButton.On_ButtonPress += On_BtnPress;
    }
    private void Start()
    { }
    void ResetData()
    {
        leftSlider.gameObject.SetActive(true);
        rigthSlider.gameObject.SetActive(true);
        rigthSlider.value = 0;
        leftSlider.value = 0;
        isClickLeftBtn = false;
        isClickRigBtn = false;
        clickLeftTime = -1;
        clickRigTime = -1;
        hadStartSliderAddCor = false;
        canAddSlider = false;
        hadStartSliderLesser = false;
        ctrlSliderDisProcess = 1;
        isNeedCtrlSlider = true;
    }
    private void OnDisable()
    {
        DisBtnEvent();
    }
    private void OnDestroy()
    {
        DisBtnEvent();
    }
    void DisBtnEvent()
    {
        EasyButton.On_ButtonDown -= On_BtnDown;
        EasyButton.On_ButtonUp -= On_BtnUp;
        EasyButton.On_ButtonPress -= On_BtnPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.sliderShowNum == 2)
        {
            hadStartSliderAddCor = false;
            if (Time.time - upTime > 1 && leftSlider.value > 0 && isClickLeftSKBtn == false && isClicikRigSKBtn == false)
            {
                juqiPartical.gameObject.SetActive(false);
                leftSlider.value -= 0.003f;
                rigthSlider.value -= 0.003f;
            }
        }
        if (isClickLeftSKBtn || isClicikRigSKBtn && Time.time - upTime < 1)
        {
            juqiPartical.gameObject.SetActive(true);
            leftSlider.value += 0.004f;
            rigthSlider.value += 0.004f;
        }
        if (isClickLeftSKBtn && !isClicikRigSKBtn && GameManager.instance.sliderShowNum == 2)
        {    
            CtrlRedPartical(true, false);
        }
        if (GameManager.instance.sliderShowNum == 2 && leftSlider.value >0.97f)
        {
            GameManager.instance.CtrlPlayableDirPause(skDirector, false);
           GameManager.instance.isDisSliderl = true;
           // GameManager.instance.sliderShowNum = 2;
        }
        if (isNeedCtrlSlider)
        {
            DisSlider();
        }
        if (isShowSlider)
        {
            CtrlSliertPer();
        }
    }
    public void CtrlSliertPer()
    {
        float value = leftSlider.value * 100;
         string strValue=string.Format("{0:N0}", value);
        sliderPer.text = strValue;
    }
    void DisSlider()
    {
        if (leftSlider.value > 0 && !canAddSlider && !hadStartSliderLesser&& GameManager.instance.sliderShowNum!=2)
        {
            hadStartSliderLesser = true;
            StopCoroutine(CtrlSlider());
            StartCoroutine(CtrlSlicerDisProcess());
        }
        if (leftSlider.value >= 0.99f || rigthSlider.value >= 0.99f)
        {
            tween.SetAutoKill();
            isNeedCtrlSlider = false;
            leftBtn.SetActive(false);
            rigBtn.SetActive(false);
            hadStartSliderAddCor = false;
            // sucessImage.SetActive(true);
            leftSlider.value = 0;
            rigthSlider.value =0;
            leftSlider.gameObject.SetActive(false);
            rigthSlider.gameObject.SetActive(false);
            touchContent.SetActive(false);
            CtrlRedPartical(false, true);
            Invoke("ShowNextPrefab",0f);
        }
    }
    public void ShowNextPrefab()
    {
        GameManager.instance.CtrlPlayableDirPause(director, false);
        //GameManager.instance.ShowPrefab(4);
        // sucessImage.SetActive(false);
    }
    void On_BtnDown(string name)
    {
        isShowSlider = true;
        if (name=="GoBtn")
        { 
            GameManager.instance.CtrlPlayableDirPause(caoZupDir,false);
        }
        if (name == "SKBtnLeft")
        {
            GameManager.instance.sliderShowNum = 2;
            clickSkNum += 1;
            isClickLeftSKBtn = true;
            CtrlShowSkMove("Sc001_M001_00sk");
        }
        if (name == "SKBtnRight")
        {
            CtrlShowSkMove("Sc001_M001_00sk");
            GameManager.instance.sliderShowNum = 2;
            clickSkNum += 1;
            isClicikRigSKBtn = true;
        }
        if (name == "LeftBtn")
        {
            isClickLeftBtn = true;
            clickLeftTime = Time.time;
        }
        else if (name == "RigBtn")
        {
            isClickRigBtn = true;
            clickRigTime = Time.time;
        }
    }
    void On_BtnUp(string name)
    {
        if (name == "SKBtnLeft")
        {
            CtrlShowSkMove("Sc001_M001_00sk2");
            upTime = Time.time;
            isClickLeftSKBtn = false;
            return;
        }
        if (name == "SKBtnRight")
        {
            CtrlShowSkMove("Sc001_M001_00sk2");
            upTime = Time.time;
            isClicikRigSKBtn = false;
            return;
        }
#if UNITY_EDITOR
        canAddSlider = false;
        hadStartSliderAddCor = false;
        StopCoroutine(CtrlSlider());
        if (!hadStartSliderLesser)
        {
            hadStartSliderLesser = true;
            StartCoroutine(CtrlSlicerDisProcess());
        }
#else
        if (name == "LeftBtn")
        {
            isClickLeftBtn = false;
            clickLeftTime = -1;
        }
        else if (name == "RigBtn")
        {
            isClickRigBtn = false;
            clickRigTime = -1;
        }
        CtrlAddSlider();
#endif
    }
    void On_BtnPress(string name)
    {
        if (name == "SKBtnRight"|| name== "SKBtnLeft")
        {
            upTime = Time.time;
            isClickRigBtn = false;
            CtrlShowSkMove("Sc001_M001_00sk");
            return;
        }
        if (name == "GoBtn")
        {
            return;
        }
#if UNITY_EDITOR
        canAddSlider = true;
        hadStartSliderLesser = false;
        StopCoroutine(CtrlSlicerDisProcess());
        if (!hadStartSliderAddCor)
        {
            hadStartSliderAddCor = true;
            StartCoroutine(CtrlSlider());
        }
# else
        CtrlAddSlider();
#endif
    }
    public void CtrlAddSlider()
    {
        float abs = Math.Abs(clickRigTime - clickLeftTime);
        canAddSlider = isClickLeftBtn && clickRigTime >= 0 && clickLeftTime >= 0 && isClickRigBtn && abs < 0.2f;
        if (!canAddSlider)
        {
            hadStartSliderAddCor = false;
            StopCoroutine(CtrlSlider());
        }
        else if (!hadStartSliderAddCor && canAddSlider)
        {
            hadStartSliderAddCor = true;
            hadStartSliderLesser = false;
            StopCoroutine(CtrlSlicerDisProcess());
            StartCoroutine(CtrlSlider());
        }
    }
    //血条减少
    IEnumerator CtrlSlicerDisProcess()
    {
        DOTween.KillAll();
        role.transform.DOScale(new Vector3(55, 55, 55),0.1f).SetEase(Ease.Linear).SetDelay(0f);
        role.transform.DOLocalMove(new Vector3( 6252,-990,-1),0.1f).SetEase(Ease.Linear);
        CtrlRedPartical(true,false);
        // tween = role.transform.DOScale(1f, 10f).SetEase(Ease.InBack);
        while (leftSlider.value > 0 && hadStartSliderLesser)
        {
            Time.timeScale = 1;
            yield return new WaitForSeconds(0.1f);
            ctrlSliderDisProcess += 0.1f;
            leftSlider.value -= 0.01f * ctrlSliderDisProcess;
            rigthSlider.value -= 0.01f * ctrlSliderDisProcess;
        }
    }
    public bool isPlaySk;
    public bool isplaySk2;
    public void CtrlShowSkMove(string name)
    {
        if (name == "Sc001_M001_00sk")
        {
            if (isPlaySk)
            {
                return;
            }
            isPlaySk = true;
            isplaySk2 = false;
        }
        else if (name == "Sc001_M001_00sk2")
        {
            if (isplaySk2)
            {
                return;
            }
            isplaySk2 = true;
            isPlaySk = false;
        }
        sk.state.SetAnimation(0, name, false);
    }
    /// <summary>
    /// 
    /// </summary>
    public void CtrlRedPartical(bool red,bool nored)
    {
        redPartical.SetActive(red);
        noRedPartical.SetActive(nored);
    }
    //血条增加协成
    IEnumerator CtrlSlider()
    {
        DOTween.KillAll();
         role.transform.DOScale (new Vector3(50,50,50),0.4f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        role.transform.DOLocalMove(new Vector3(6116, -851, -1), 0.4f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        // role.transform.DOLocalMoveY(-800,5f).SetEase(Ease.Linear);
        CtrlRedPartical(false,true);
        while (leftSlider.value<=1&& hadStartSliderAddCor)
        {
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(0.1f);
            leftSlider.value += 0.02f;
            rigthSlider.value += 0.02f;
        }
    }



}

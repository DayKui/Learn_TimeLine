using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Spine;
using Spine.Unity;

public class SM001 : MonoBehaviour {

    public PlayableDirector phoneDir;
    public PlayableDirector ShowGril;
    public SkeletonAnimation repulsePhoneSA;
    public SkeletonAnimation answerPhoneSA;
    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnEnable()
    {
        EasyButton.On_ButtonDown += On_BtnDown;
        EasyButton.On_ButtonUp += On_BtnUp;
        EasyButton.On_ButtonPress += On_BtnPress;
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
    void On_BtnDown(string name)
    {
        if (name=="ShowBtn")
        {
            GameManager.instance.CtrlPlayableDirPause(ShowGril, false);
            GameManager.instance.CtrlCancasIsShow(true);
        }
        if (name=="AnswerBtn")
        {
            GameManager.instance.CtrlPlayableDirPause(phoneDir, false);
            GameManager.instance.CtrlCancasIsShow(true);
            if (answerPhoneSA==null)
            {
                Debug.Log("answerPhoneSA is null");
            }
            Invoke("ShowAnswerPhoneSA",0.3f);
            //List<string> awards = TableConfig.instance.GetAwards(10001);
            //GameManager.instance.CtrlTextCon(awards);
        }
        else if (name== "RepulseBtn")
        {
            GameManager.instance.CtrlPlayableDirPause(phoneDir, false);
            phoneDir.gameObject.SetActive(false);
            phoneDir.gameObject.SetActive(true);
            repulsePhoneSA.state.SetAnimation(0, "Sc001_M001_0002", false);
            GameManager.instance.CtrlCancasIsShow(true);
        }
    }
    void ShowAnswerPhoneSA()
    {
        answerPhoneSA.state.SetAnimation(0, "Sc001_M001_0011", false);

    }
    void On_BtnUp(string name)
    {

    }
    void On_BtnPress(string name)
    {

    }
}

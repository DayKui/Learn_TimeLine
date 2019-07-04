using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using Cinemachine;
using Cinemachine.Timeline;

public class Sc001M001 : MonoBehaviour {

    public Button answerBtn;
    public Button repulseBtn;
    public PlayableDirector director;
	// Use this for initialization
	void Start ()
    {
        answerBtn.onClick.AddListener(OnClickAnswerBtn);
        repulseBtn.onClick.AddListener(delegate() {
            GameManager.instance.CtrlPlayableDirPause(director, false);
            director.transform.GetChild(0).gameObject.SetActive(false);
            director.gameObject.SetActive(false);
            director.gameObject.SetActive(true);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnClickAnswerBtn()
    {
        GameManager.instance.CtrlPlayableDirPause(director, false);
        director.transform.GetChild(0).gameObject.SetActive(false);
        GameManager.instance.CtrlCancasIsShow(true);
       List<string> awards= TableConfig.instance.GetAwards(10001);
        GameManager.instance.CtrlTextCon(awards);
    }
    private void OnDisable()
    {
      GameManager.instance.textContent.GetComponent<Text>().text = "";
    }
}

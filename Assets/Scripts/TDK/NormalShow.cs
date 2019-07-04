using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NormalShow : MonoBehaviour {
    public Button button;
    public RectTransform lunImg;
    public CanvasGroup cg;
    public bool hasStart=false;
	// Use this for initialization
	void Start() {
        button.onClick.AddListener(delegate ()
        {
            SoundManager.Instance.bgmAudio.volume = 0;
            if (GameManager.instance.isEnd)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                                        Application.Quit();
#endif
            }
            GameManager.instance.CtrlPrefabActive(false, false);
            transform.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(delegate ()
            {
                button.interactable = false;
                GameManager.instance.CtrlShowPre("scm2");
            });
        });
    }
    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update ()
    {
       
        if (cg.alpha>=0.96f&& !hasStart)
        {
            SoundManager.Instance.PlayAudio("BlackVideo",true,0,1);
            hasStart = true;
            button.interactable = true;
            lunImg.DOLocalRotate(new Vector3(0, 0, 180), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
	}
}

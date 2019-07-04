using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoPanel : MonoBehaviour {
    List<List<GameObject>> listObjs = new List<List<GameObject>>();
    List<List<Sprite>> spriteOneList = new List<List<Sprite>>();
    public Image move;
    public Button clickButton;
    int column = 12;
    int row = 7;
    public int clickInt=1;
    // Use this for initialization
    void Start()
    {
        clickButton.onClick.AddListener(ClickBtn);
        int count = this.transform.childCount;
        for (int i = 0; i < count / column; i++)
        {
            List<GameObject> list = new List<GameObject>();
            for (int k = 0; k < count / row; k++)
            {
                list.Add(transform.GetChild(k + i * 12).gameObject);
            }
            listObjs.Add(list);
        }
        GetResObj("LogoImage");
        SetSprite();
        PlayAnimation();
        SoundManager.Instance.PlayAudio("StartBgVideo", true, 0, 1);

    }
    public void ClickBtn()
    {
        if (clickInt>1)
        {
            return;
        }
        clickInt += 1;
        //GameManager.Instance.GetObjs("PanelTwo");
        this.gameObject.GetComponent<CanvasGroup>().DOFade(0,3f).SetEase(Ease.Linear).OnComplete(delegate ()
         {    
             this.gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
             ShowChapterMove();
         }
        );
        SoundManager.Instance.bgmAudio.volume = 0;
    }
    public void ShowChapterMove()
    {
        int pro = GameManager.instance.GetLookingProcess();
        if (pro<=GameManager.instance.allPanelPrefab)
        {
           // GameManager.instance.CtrlShowChapters();
            GameManager.instance.ShowPrefab(pro);
        }
    }
    public void SetSprite()
    {
        for (int i = 0; i < row; i++)
        {
            for (int k = 0; k < column; k++)
            {
                listObjs[row - 1 - i][k].GetComponent<Image>().sprite = spriteOneList[i][k];
            }
        }
    }
    public void GetResObj(string name)
    {
        Texture2D t2d = Resources.Load("Texture/"+"TDK/"+name, typeof(Texture2D)) as Texture2D;
        for (int i = 0; i < row; i++)
        {
            List<Sprite> iSpriteList = new List<Sprite>();
            for (int k = 0; k < column; k++)
            {
                iSpriteList.Add(Sprite.Create(t2d, new Rect(new Vector2(t2d.width / column * k, t2d.height / row * i), new Vector2(t2d.width / column, t2d.height / row)), new Vector2(0, 0)));
            }
            spriteOneList.Add(iSpriteList);
        }
    }
    private void PlayAnimation()
    {
        PlayAnim.Instance.PlayAnim3(listObjs);
        Invoke("ShowBtnAni",1.5f);
    }
    private void ShowBtnAni()
    {
          move.DOFade(1, 1.5f).SetEase(Ease.InCubic).SetDelay(0.4f).OnComplete(delegate() {
             clickButton.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.InBack).OnComplete(delegate() {
             });
          });
    }
}

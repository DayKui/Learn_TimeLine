using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableConfig : MonoBehaviour {

    public static TableConfig instance; 
    public  ClassData cd;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    #region
    //读取对应表的一行数据
    public T GetTableRowData<T>(string configName, int id) where T : class, new()
    {
        T data = new T();
        if (configName == "AwardContent")
        {
            AwardContent ac = new AwardContent();
            for (int i = 0; i < cd.awardContent.Count; i++)
            {
                if (cd.awardContent[i].id == id)
                {
                    ac.id = id;
                    ac.awardId = cd.awardContent[i].awardId;
                    ac.name1 = cd.awardContent[i].name1;
                }
            }
            data = ac as T;
        }
        if (configName == "ChapterAwardItem")
        {
            ChapterAward ac = new ChapterAward();
            for (int i = 0; i < cd.chapterAward.Count; i++)
            {
                if (cd.chapterAward[i].playId== id)
                {
                    ac.playId = id;
                    ac.playName = cd.chapterAward[i].playName;
                    ac.chapterId = cd.chapterAward[i].chapterId;
                    ac.chapter = cd.chapterAward[i].chapter;
                    ac.annotation = cd.chapterAward[i].annotation;
                    ac.awaidId = cd.chapterAward[i].awaidId;
                    ac.awardItem = cd.chapterAward[i].awardItem;
                }
            }
            data = ac as T;
        }
        if (configName == "ConfigOne")
        {
            SerializaCon  ac = new SerializaCon();
            for (int i = 0; i < cd.configOne.Count; i++)
            {
                if (cd.configOne[i].id == id)
                {
                    ac.id = id;
                    ac.chapter = cd.configOne[i].chapter;
                    ac.name1 = cd.configOne[i].name1;
                    ac.name2 = cd.configOne[i].name2;
                }
            }
            data = ac as T;
        }
        return data;
    }
    //获取所有章节
    public  List<string>  GetConfigOneAllName1()
    {
        List<string> name1Datas = new List<string>();
        for (int i = 0; i < cd.configOne.Count; i++)
        {
            if (string .IsNullOrEmpty(cd.configOne[i].name1))
            {
                Debug.LogError("table name1 is null ");
                continue;
            }
            else
            {
                string name1 = cd.configOne[i].name1;
                name1Datas.Add(name1);
            }
        }
        return name1Datas;
    }
    /// <summary>
    /// 获取章节某一幕的操作奖励
    /// </summary>
    /// <returns></returns>
    public List<string> GetAwards(int awardId)
    {
        List<string> awards = new List<string>();
        List<AwardContent> ac = cd.awardContent;
            for (int i = 0; i < ac.Count; i++)
            {
                if (ac[i].awardId == awardId)
                {
                string con = ac[i].name1;
                awards.Add(con);
                }
            }
        return awards;       
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SerializaCon 
{
    public int id;
    public string chapter;
    public string name1;
    public string name2;
    public void SetValue(int _id, string _chapter, string _name1,string _name2 )
    {
        id =_id;
        chapter = _chapter;
        name1 = _name1;
        name2 = _name2;
    }
    public void SetValue(SerializaCon sc)
    {
        id = sc.id;
        chapter = sc.chapter;
        name1 = sc.name1;
        name2 = sc.name2;
    }
}
[System.Serializable]
public class ChapterAward
{
    public int playId;
    public string playName;
    public string chapterId;
    public string chapter;
    public string annotation;//注释
    public int awaidId;
    public int awardItem;
    public void SetValue(int _playId, string _playName, string _chapterid, string _chapter,string _anno,int _awardId,int _awardItem)
    {
        playId = _playId;
        playName = _playName;
        chapterId = _chapterid;
        chapter = _chapter;
        annotation = _anno;
        awaidId = _awardId;
        awardItem = _awardItem;
    }
    public void SetValue(ChapterAward sc)
    {
        playId = sc.playId; ;
        playName = sc.playName;
        chapterId = sc.chapterId;
        chapter = sc.chapter;
        annotation = sc.annotation;
        awaidId = sc.awaidId;
        awardItem = sc.awardItem;
    }
}
[System.Serializable]
public class AwardContent
{
    public int id;
    public int awardId;
    public string name1;
    public void SetValue(int _id, int _awardId, string _name1)
    {
        id = _id;
        awardId = _awardId;
        name1 = _name1;
    }
    public void SetValue(AwardContent ac)
    {
        id = ac.id;
        awardId = ac.awardId;
        name1 = ac.name1;
    }
}
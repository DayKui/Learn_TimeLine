using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Excel;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System;


public class ExcelAccess
{
    public static string[] excelName = { "ConfigOne.xlsx" , "ChapterAwardItem.xlsx", "AwardContent.xlsx" };
    public static string[] sheetNames = { "sheet1", "sheet2", "sheet3" };
    public static T SelectMensTable<T>(int tableId) where T:IList,new()
    {
        DataRowCollection collect = ExcelAccess.ReadExcel(excelName[tableId - 1]);
        T menuArray = new T ();
        if (tableId==1)
        {
            for (int i = 1; i < collect.Count; i++)
            {
                if (collect[i][1].ToString() == "") continue;
                SerializaCon cd = new SerializaCon();
                cd.id = int.Parse(collect[i][0].ToString());
                cd.chapter = collect[i][1].ToString();
                cd.name1 = collect[i][2].ToString();
                cd.name2 = collect[i][3].ToString();
                menuArray.Add(cd);
            }
        }
        if (tableId ==2)
        {
            for (int i = 1; i < collect.Count; i++)
            {
                if (collect[i][1].ToString() == "") continue;
                ChapterAward cd = new ChapterAward();
                cd.playId = int.Parse(collect[i][0].ToString());
                cd.playName = collect[i][1].ToString();
                cd.chapterId = collect[i][2].ToString();
                cd.chapter = collect[i][3].ToString();
                cd.annotation = collect[i][4].ToString();
                cd.awaidId = int.Parse(collect[i][5].ToString());
                cd.awardItem=int.Parse( collect[i][6].ToString());
                menuArray.Add(cd);
            }
        }
        if (tableId == 3)
        {
            for (int i = 1; i < collect.Count; i++)
            {
                if (collect[i][1].ToString() == "") continue;
                AwardContent cd = new AwardContent();
                cd.id = int.Parse(collect[i][0].ToString());
                cd.awardId = int.Parse(collect[i][1].ToString());
                cd.name1 = collect[i][2].ToString();
                menuArray.Add(cd);
            }
        }
        return menuArray;
    }

    static DataRowCollection ReadExcel(string excelName)
    {
        FileStream stream = File.Open(FilePath(excelName), FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        return result.Tables["sheet1"].Rows;
    }

    public static string FilePath(string name)
    {
        Debug.Log("name ==" + name);
        return Application.dataPath + "/"+"Configs/" + name;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundles : Editor
{

    [MenuItem("AssetBundles/Create AssetBundles")]
    public static void ExcuteBuild()
    {
        ClassData deta = ScriptableObject.CreateInstance<ClassData>();
        deta.configOne = ExcelAccess.SelectMensTable<List<SerializaCon>>(1);
        deta.chapterAward = ExcelAccess.SelectMensTable<List<ChapterAward>>(2);
        deta.awardContent = ExcelAccess.SelectMensTable<List<AwardContent>>(3);
        AssetDatabase.CreateAsset(deta, HolderPath);
        AssetImporter importer = AssetImporter.GetAtPath(HolderPath);
        importer.assetBundleName = "alldata";
        BuildPipeline.BuildAssetBundles("Assets/Abs", 0, EditorUserBuildSettings.activeBuildTarget);
    }

    [MenuItem("AssetBundles/BuildAssetBundles")]
    public static void BuildAssetBundle()
    {
        string outputPath = "Assets/bundles";
        foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets))
        {
            Debug.Log(obj.name);
            //得到指定资源的路劲
            string path = AssetDatabase.GetAssetPath(obj);
            Debug.Log("path" + path);
            //得到指定资源的bundle名字
            Debug.Log(AssetImporter.GetAtPath(path).assetBundleName + "name");

            string abName = AssetImporter.GetAtPath(path).assetBundleName;
            //得到指定资源的依赖资源路径
            string[] depends = AssetDatabase.GetDependencies(path);
            //修改所有依赖的bundle名
            foreach (string dp in depends)
            {
                if (dp.EndsWith(".cs") || dp.EndsWith(".js"))
                {
                    continue;
                }
                AssetImporter ai = AssetImporter.GetAtPath(dp);
                ai.assetBundleName = abName;
            }
        }
        //生成bundle包的路径
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }
        //把已经赋值AssetBundleName的Object全部打包到指定目录中
        BuildPipeline.BuildAssetBundles(outputPath, 0, EditorUserBuildSettings.activeBuildTarget);
        AssetDatabase.Refresh();
        Debug.Log("Build AssetBundle Success");
    }
    public static string HolderPath
    {
        get
        {
            return "Assets/Datas.asset";
        }
    }

}

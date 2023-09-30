using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    private static string Name;
    private static int Kill;
    private static int Death;
    private static int Win;
    private static int Lose;
    private static int Task;
    private static int TaskPoint;
    private static GameObject DataInstance;
    private string filePath;
    private StatusData Data;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        CheckInstance();
        //Ç±Ç±Ç…ÉfÅ[É^ì«Ç›éÊÇËèàóù
        filePath = Application.persistentDataPath + "/" + ".savedata.json";
        Data = new StatusData();
        Load();
        if(NameSetting == null)
        {
            NameSetting = "Guest";
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    void CheckInstance()
    {
        if (DataInstance == null)
        {
            DataInstance = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        Data.NameData = Name;
        Data.KillData = Kill;
        Data.DeathData = Death;
        Data.WinData = Win;
        Data.LoseData = Lose;
        Data.TaskData = Task;
        Data.TaskPointData = TaskPoint;
        string json = JsonUtility.ToJson(Data);
        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json); streamWriter.Flush();
        streamWriter.Close();
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            Data = JsonUtility.FromJson<StatusData>(data);
            Name = Data.NameData;
            Kill = Data.KillData;
            Death = Data.DeathData;
            Win = Data.WinData;
            Lose = Data.LoseData;
            Task = Data.TaskData;
            TaskPoint = Data.TaskPointData;
        }
    }

    public string NameSetting
    {
        get { return Name; }
        set { Name = value; }
    }

    public int KillSetting
    {
        get { return Kill; }
        set { Kill = value; }
    }

    public int DeathSetting
    {
        get { return Death; }
        set { Death = value; }
    }

    public int WinSetting
    {
        get { return Win; }
        set { Win = value; }
    }

    public int LoseSetting
    {
        get { return Lose; }
        set { Lose = value; }
    }

    public int TaskSetting
    {
        get { return Task; }
        set { Task = value; }
    }

    public int TaskPointSetting
    {
        get { return TaskPoint; }
        set { TaskPoint = value; }
    }
}

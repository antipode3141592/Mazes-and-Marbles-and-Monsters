﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Data
{
    public class JSONSaver
    {
        private static readonly string _filename = "saveData1.sav"; //default file name
        //TODO implement a routine for incrementing save file names and choosing between save files
        public static string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + _filename; //slash, because it's from inside unity
        }

        public void Save(SaveData data)
        {
            data.hashValue = String.Empty;  //initialize empty
            string json = JsonUtility.ToJson(data);
            data.hashValue = GetSHA256(json);
            json = JsonUtility.ToJson(data);

            string saveFilename = GetSaveFilename();

            FileStream filestream = new FileStream(saveFilename, FileMode.Create);
            //using syntax automatically opens and closes the filestream cleanly
            using (StreamWriter writer = new StreamWriter(filestream))
            {
                writer.Write(json);
            }
        }

        private bool CheckData(string json)
        {
            SaveData tempSaveData = new SaveData();
            JsonUtility.FromJsonOverwrite(json, tempSaveData);
            string oldHash = tempSaveData.hashValue;    //store old hash
            tempSaveData.hashValue = String.Empty;

            string tempJson = JsonUtility.ToJson(tempSaveData);
            string newHash = GetSHA256(tempJson);

            return (oldHash == newHash);
        }

        public bool Load(SaveData data)
        {
            string loadFileName = GetSaveFilename();
            //make sure exists
            if (File.Exists(loadFileName))
            {
                using (StreamReader reader = new StreamReader(loadFileName))
                {
                    string json = reader.ReadToEnd();
                    //check for tampering
                    if (CheckData(json))
                    {
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.LogWarning("JSONSAVER Load: invalid hash, aborting file read...");
                    }

                    
                }
                return true;
            }
            return false;
        }
        //returns false if there is no save file present
        public static bool CheckSaveFile()
        {
            if (File.Exists(GetSaveFilename())) { return true; }
            else { return false; }
        }

        public void Delete()
        {
            File.Delete(GetSaveFilename());
        }

        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = String.Empty;
            foreach (byte b in hash)
            {
                hexString += b.ToString("x2");
            }
            return hexString;
        }

        private string GetSHA256(string text)
        {
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed mySHA256 = new SHA256Managed();
            byte[] hashValue = mySHA256.ComputeHash(textToBytes);
            return GetHexStringFromHash(hashValue);
        }
    }
}

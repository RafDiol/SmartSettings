using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace SmartSettings
{
    public class Settings
    {
        /// <summary>
        /// The filepath of the settings file
        /// </summary>
        public string filepath;
        const string sectionBreak = "\n!-----NEW SECTION-----!\n";

        Dictionary<string, string> stringDictionary = new Dictionary<string, string>();
        Dictionary<string, int> intDictionary = new Dictionary<string, int>();
        Dictionary<string, double> doubleDictionary = new Dictionary<string, double>();
        Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();
        Dictionary<string, List<string>> stringListDictionary = new Dictionary<string, List<string>>();
        Dictionary<string, List<int>> intListDictionary = new Dictionary<string, List<int>>();

        public Settings(string filepath)
        {
            this.filepath = filepath;
            
            if (File.Exists(this.filepath))
            {
                LoadDataFromFile(this.filepath);
            }
        }

        private void LoadDataFromFile(string file)
        {
            string allText = File.ReadAllText(file);
            string[] sections = allText.Split(sectionBreak);

            stringDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(sections[0]);
            intDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(sections[1]);
            doubleDictionary = JsonConvert.DeserializeObject<Dictionary<string, double>>(sections[2]);
            boolDictionary = JsonConvert.DeserializeObject<Dictionary<string, bool>>(sections[3]);
            stringListDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(sections[4]);
            intListDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(sections[5]);
        }

        /// <summary>
        /// Writes the changes to the file
        /// </summary>
        public void Save()
        {
            string sd = JsonConvert.SerializeObject(stringDictionary);
            string id = JsonConvert.SerializeObject(intDictionary);
            string dd = JsonConvert.SerializeObject(doubleDictionary);
            string bd = JsonConvert.SerializeObject(boolDictionary);
            string sld = JsonConvert.SerializeObject(stringListDictionary);
            string ild = JsonConvert.SerializeObject(intListDictionary);

            string[] allJsonStrings = new string[] { sd, id, dd, bd, sld, ild };

            using(StreamWriter sw = new StreamWriter(filepath))
            {
                foreach(string s in allJsonStrings)
                {
                    sw.Write(s);
                    sw.Write(sectionBreak);
                }
            }
        }

        public bool EncodeString(string key, string data)
        {
            return stringDictionary.TryAdd(key, data);
        }

        public string DecodeString(string key)
        {
            return stringDictionary[key];
        }

        public bool RemoveString(string key)
        {
            return stringDictionary.Remove(key);
        }

        public bool EncodeInt(string key, int data)
        {
            return intDictionary.TryAdd(key, data);
        }

        public int DecodeInt(string key)
        {
            return intDictionary[key];
        }

        public bool RemoveInt(string key)
        {
            return intDictionary.Remove(key);
        }

        public bool EncodeDouble(string key, double data)
        {
            return doubleDictionary.TryAdd(key, data);
        }

        public double DecodeDouble(string key)
        {
            return doubleDictionary[key];
        }

        public bool RemoveDouble(string key)
        {
            return doubleDictionary.Remove(key);
        }

        public bool EncodeBool(string key, bool data)
        {
            return boolDictionary.TryAdd(key, data);
        }

        public bool DecodeBool(string key)
        {
            return boolDictionary[key];
        }

        public bool RemoveBool(string key)
        {
            return boolDictionary .Remove(key);
        }

        public bool EncodeStringList(string key, List<string> data)
        {
            return stringListDictionary.TryAdd(key, data);
        }

        public List<string> DecodeStringList(string key)
        {
            return stringListDictionary[key];
        }

        public bool RemoveStringList(string key)
        {
            return stringListDictionary.Remove(key);
        }

        public bool EncodeIntList(string key, List<int> data)
        {
            return intListDictionary.TryAdd(key, data);
        }

        public List<int> DecodeIntList(string key)
        {
            return intListDictionary[key];
        }

        public bool RemoveIntList(string key)
        {
            return intListDictionary.Remove(key);
        }
    }
}

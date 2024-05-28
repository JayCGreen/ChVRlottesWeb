using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WordNode : MonoBehaviour
{

    [System.Serializable]
    public class Definition
    {
        public string definition;
        public string[] synonyms;
        public string[] antonyms;
    }

    [System.Serializable]
    public class License
    {
        public string name;
        public string url;
    }

    [System.Serializable]
    public class Meaning
    {
        public string partOfSpeech;
        public List<Definition> definitions;
        public List<string> synonyms;
        public List<string> antonyms;
    }

    [System.Serializable]
    public class Phonetic
    {
        public string audio;
        public string sourceUrl;
        public License license;
        public string text;
    }

    [System.Serializable]
    public class Wrap
    {
        public string word;
        public List<Phonetic> phonetics;
        public Meaning[] meanings;
        public License license;
        public List<string> sourceUrls;
    }

    [System.Serializable]
    public class Root{
        public Wrap[] wrap;
    }

    public string wordString;

    public List<string> defs;
    public List<string> syn;
    public List<string> ant;

    void Start()
    { 
        StartCoroutine(GetRequest("https://api.dictionaryapi.dev/api/v2/entries/en/" + wordString));  
    }

    IEnumerator GetRequest(string uri){
        //GO BACK AND CHECK THE XR REQ
        Root wordData = new Root();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)){
            yield return webRequest.SendWebRequest();
            
            if (!string.IsNullOrWhiteSpace(webRequest.error))
            {
                Debug.LogError($"Error {webRequest.responseCode} - {webRequest.error}");
                yield break;
            }

            //Grab all the word data
            wordData = JsonUtility.FromJson<Root>("{\"wrap\":" + webRequest.downloadHandler.text + "}");

            //Get definitions, syn, and ants from the api
            foreach (Meaning m in wordData.wrap[0].meanings){
                //Debug.Log(test.wrap[0].meanings[0].antonyms.Count);
                foreach(Definition def in m.definitions)
                {
                    defs.Add(def.definition);
                }
                foreach(string syno in m.synonyms){
                    syn.Add(syno);
                }
                foreach(string anto in m.antonyms){
                    ant.Add(anto);
                }
            }
        }
    }
}

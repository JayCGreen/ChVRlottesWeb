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

    public class Word{
        public string word;
        public List<string> definition = new List<string>();
        public List<Word> synonyms = new List<Word>();
        public List<Word> antonyms = new List<Word>();
        public bool test;

        public Word(string word, List<string> def, List<Word> synonyms, List<Word> antonyms){
            this.word = word;
            this.definition = def;
            this.synonyms = synonyms;
            this.antonyms = antonyms;
        }

        public Word(string word, bool mode){
            this.word = word;
            if(mode){
                define();
            }
        }

        public void define()
        {
            definition.Add("just a random definition");
            synonyms.Add(new Word("A synonym", false));
            antonyms.Add(new Word("An Antonym", false));
        }
    }

    public string wordString;

    public List<string> defs;
    public List<string> syn;
    public List<string> ant;

    public Word myWord;

    void Start()
    { 
        StartCoroutine(GetRequest("https://api.dictionaryapi.dev/api/v2/entries/en/" + wordString));
        
    }

    IEnumerator GetRequest(string uri){
        Root test = new Root();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)){
            yield return webRequest.SendWebRequest();
            
            if (!string.IsNullOrWhiteSpace(webRequest.error))
            {
                Debug.LogError($"Error {webRequest.responseCode} - {webRequest.error}");
                yield break;
            }

            //Debug.Log("{\"wrap\":" + webRequest.downloadHandler.text + "}");
            test = JsonUtility.FromJson<Root>("{\"wrap\":" + webRequest.downloadHandler.text + "}");

            foreach (Meaning m in test.wrap[0].meanings){
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

            myWord = new Word(test.wrap[0].word, true);
        }
    }
}

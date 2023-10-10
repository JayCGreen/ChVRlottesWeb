using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class Talker : MonoBehaviour
{
    AudioSource source;
    string script;
    WordNode currentWord;
    bool spoke = false;
    // Start is called before the first frame update
void Start()
    { 

        
        
    }

    void Update()
    {
        
        source = this.gameObject.GetComponent<AudioSource>();
        currentWord = this.gameObject.GetComponent<SpawnWeb>().source;
        //Debug.Log(currentWord.defs.Count);
        if (currentWord.defs.Count > 0 && !spoke)
        {
            script = currentWord.defs[0];
            spoke = true;
            StartCoroutine(GetRequest("http://api.voicerss.org/?key=c5153d4872c7406894a483528582682c&hl=en-us&c=WAV&src=" + script));
        }

    }

    IEnumerator GetRequest(string uri){
        using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.WAV)){
            yield return webRequest.SendWebRequest();
            
            if (!string.IsNullOrWhiteSpace(webRequest.error))
            {
                Debug.LogError($"Error {webRequest.responseCode} - {webRequest.error}");
                yield break;
            }
            AudioClip myClip = DownloadHandlerAudioClip.GetContent(webRequest);
            source.clip = myClip;
            source.Play();
        }
    }
}

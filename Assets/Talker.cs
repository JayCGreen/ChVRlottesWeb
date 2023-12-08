using System;
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
        string[] greetings = {"Hi", "Hello", "Hiya", "Howedy", "Greetings"};
        string[] intro = {"I'm", "My name is", "People call me"};
        string[] welcome= {"Welcome to my web", "This is my web", "Welcome to the parlor", "Make yourself of home"};
    
        script = greetings[UnityEngine.Random.Range(0,greetings.Length)] + intro[UnityEngine.Random.Range(0,intro.Length)] + "Charlotte.  ";
        script = script + welcome[UnityEngine.Random.Range(0, welcome.Length)] + ". There are some flies floating around with associated words. ";
        
        script = script + "Bring me the fly and give you its definition and will web up more flies that carry around words related to it.  ";
        script = script + "Fireflies with green glows are synonyms, red glow are antomnyms.  ";
        script = script + "To move, grab that pole on top of the Bee you're riding. With the pole grabbed, tilt both hands forward to move forward and tilt both back to move backwards.  ";
        script = script + "You can also tilt your hands in opposite directions to tell the bee to turn.  ";
        script = script + "You can say define followed by the word and I'll find the associated fly myself and create the web. ";
        script = script + "I've gone ahead and created a web for the word Happy for you to see how it works. ";
        script = script + "Happy defining!";
        spoke = true;
        StartCoroutine(GetRequest("http://api.voicerss.org/?key=c5153d4872c7406894a483528582682c&hl=en-us&c=WAV&src=" + script));
        
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

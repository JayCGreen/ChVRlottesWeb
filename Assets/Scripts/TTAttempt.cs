using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.Windows.Speech;

public class TTAttempt : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private DictationRecognizer m_DictationRecognizer;
    
    private Dictionary<string, Action> phrases = new Dictionary<string, Action>();
    String command = "";

    // Start is called before the first frame update
    void Start()
    {
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.InitialSilenceTimeoutSeconds = 120;
 

        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text);
            command = text;
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };


        m_DictationRecognizer.Start();
        /*
        phrases.Add("define", Testing);
        keywordRecognizer = new KeywordRecognizer(phrases.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;  
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(command.Length);
        if ((command.Length > 0) && (command.ToUpper() =="OMEGA"))
        {
            m_DictationRecognizer.Stop();
            command = "";
        }
        if (command.Length > 0 && command.ToUpper().StartsWith("DEFINE"))
        {
            
            String[] lexi = command.Split(' ');
            if (lexi.Length > 1)
            {
                Debug.Log(lexi[1]);
                this.gameObject.GetComponent<StrandOfFate>().history.AddFirst(lexi[1]);
            }
            command = "";
        }
        if (command.Length > 0 && command.ToUpper().StartsWith("SENTENCE"))
        {
            
            String[] lexi = command.Split(' ');
            if (lexi.Length > 1)
            {
                Debug.Log(lexi[1]);
                this.gameObject.GetComponent<StrandOfFate>().history.AddFirst(command);;

            }
            command = "";
        }
        if (command.Length > 0 && command.ToUpper().StartsWith("BACK"))
        {
            this.gameObject.GetComponent<StrandOfFate>().history.RemoveFirst();
            command = "";
        }

    }

}

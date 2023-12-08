using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnString : MonoBehaviour
{
    public int shift = -10;

    public GameObject nodeW;
    public WordNode source;

    public string testPhrase;
    
    public TMPro.TMP_Text mytext;

    // Start is called before the first frame update

    void Start()
    {
        if (testPhrase != null && testPhrase.Length > 0)
            StartCoroutine(makeString());

        
    }

    Vector3 spawnPoint(){
        /*
        float x = Random.Range(rX[0], rX[1]);
        float y = Random.Range(rY[0], rY[1]);
        float z = Random.Range(rZ[0], rZ[1]);
        */

        return new Vector3(0, 0, 0);
    }

    IEnumerator makeString(){
        String[] lexi = testPhrase.Split(' ');
        Debug.Log(testPhrase);
        Debug.Log(string.Join("", lexi));
        foreach (string a in  lexi){
            Debug.Log(a);
            GameObject midnode = Instantiate(nodeW, new Vector3(shift, 0 , 0), Quaternion.identity);
            shift = shift + 3;
            midnode.transform.parent = this.transform;
            midnode.name = a;
            source = midnode.GetComponent<WordNode>();
            source.wordString = a;
            mytext = midnode.GetComponentInChildren<TMPro.TMP_Text>();
            mytext.text = source.wordString;
            mytext.alignment = TMPro.TextAlignmentOptions.TopLeft;
            yield return new WaitForSeconds(1);
        }
        
    }



    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWeb : MonoBehaviour
{
    
    private float[] rX = {-10, 10};
    private float[] rY = {2, 8};
    private float[] rZ = {-10, 10};

    public GameObject nodeW;
    public WordNode source;

    public string testWord;
    
    public TMPro.TMP_Text mytext;
   

    // Start is called before the first frame update


    void Start()
    {
        if (testWord != null && testWord.Length > 0){
            StartCoroutine(makeWeb());
        }
        
    }

    Vector3 spawnPoint(){
        float x = Random.Range(rX[0], rX[1]);
        float y = Random.Range(rY[0], rY[1]);
        float z = Random.Range(rZ[0], rZ[1]);

        return new Vector3(x,y,z);
    }

    IEnumerator makeWeb(){
        GameObject midnode = Instantiate(nodeW);
        midnode.transform.parent = this.transform;
        midnode.name = testWord;
        source = midnode.GetComponent<WordNode>();
        source.wordString = testWord;
        mytext = midnode.GetComponentInChildren<TMPro.TMP_Text>();
        mytext.text = source.wordString;
        mytext.alignment = TMPro.TextAlignmentOptions.TopLeft;
        yield return new WaitForSeconds(1);

        foreach(string word in source.syn){
            nodeW.GetComponent<WordNode>().wordString =  word;
            GameObject synnode = Instantiate(nodeW, spawnPoint() + new Vector3(10, 0, 0), Quaternion.identity);
            synnode.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green); ;
            synnode.name = word;
            synnode.transform.SetParent(midnode.transform.parent);
            synnode.GetComponentInChildren<TMPro.TMP_Text>().text = synnode.GetComponent<WordNode>().wordString;
           
        }
        foreach(string word in source.ant){
            nodeW.GetComponent<WordNode>().wordString =word;
            GameObject antnode = Instantiate(nodeW, spawnPoint() + new Vector3(-10, 0, 0), Quaternion.identity);
            antnode.name = word;
            antnode.transform.SetParent(midnode.transform.parent);
            antnode.GetComponentInChildren<TMPro.TMP_Text>().text = antnode.GetComponent<WordNode>().wordString;
            antnode.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.magenta);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            

    }
}

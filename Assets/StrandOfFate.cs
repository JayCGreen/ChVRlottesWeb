using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandOfFate : MonoBehaviour
{
    public LinkedList<string> history = new LinkedList<string>();
    public GameObject webMaker;
    public GameObject lineMaker;
    public SpawnWeb w;
    public SpawnString line;
    private int historyLen;
    private GameObject currentNode;
    private bool first;

    // Start is called before the first frame update
    void Start()
    {
        historyLen = history.Count;
        history.AddFirst("old");
        first = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hitting " + other.gameObject);
        if (other.gameObject.tag == "Node")
        {
            history.AddFirst(other.gameObject.GetComponent<WordNode>().wordString);
            other.gameObject.transform.position = new Vector3(0, history.Count, 7);
            currentNode = other.gameObject;
            Debug.Log("Add Size of the array now is " + history.Count);
            
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            GameObject toBeDeleted = GameObject.Find(other.gameObject.GetComponent<WordNode>().wordString);
            Destroy(toBeDeleted);
            history.RemoveFirst();
            currentNode = w.transform.Find(w.testWord).transform.Find(history.First.Value).gameObject;
            Debug.Log("leavin");
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (historyLen != history.Count){

            historyLen = history.Count;
            if (w != null){
                //Transform currentNode = w.transform.Find(w.testWord).transform.Find(history.First.Value);
                Debug.Log("test word is " + w.testWord + " and first val is " + history.First.Value + " currentNode is " + currentNode);
                if (currentNode != null && history.Count > 1) { currentNode.transform.SetParent(null); }
                Destroy(w.gameObject);
           }
           if (line != null){
                //Transform currentNode = w.transform.Find(w.testWord).transform.Find(history.First.Value);
                Debug.Log("test word is " + w.testWord + " and first val is " + history.First.Value + " currentNode is " + currentNode);
                if (currentNode != null && history.Count > 1) { currentNode.transform.SetParent(null); }
                Destroy(line.gameObject);
           }
           if(history.First.Value.Split(' ').Length > 1){
                StartCoroutine(spawnString());
           }
           else{
                Debug.Log("Oh no no no");
                StartCoroutine(spawnCore());
           }
            
        }   
    }


    IEnumerator spawnCore(){
        
        GameObject web = Instantiate(webMaker);
        web.GetComponent<Talker>().intro = first;
        w = web.GetComponent<SpawnWeb>();
        w.testWord = history.First.Value;
        first = false;
        yield return new WaitForSeconds(2);
    }

    IEnumerator spawnString(){
        GameObject web = Instantiate(webMaker);
        line = web.GetComponent<SpawnString>();
        line.testPhrase = history.First.Value;
        yield return new WaitForSeconds(2);
    }


}

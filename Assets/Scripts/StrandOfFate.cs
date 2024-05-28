using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandOfFate : MonoBehaviour
{
    public LinkedList<string> history = new LinkedList<string>();
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
        //When a word node enters the area, set the node's word as the current word for the web
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
        //When a word node exits the area, bump it from the history stack. The new top of the stack becomes the source for the web
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
        //Update when there's a descrepensy between what the count "should" be and what it is
        if (historyLen != history.Count){
            historyLen = history.Count;
            if (w != null){
                //Transform currentNode = w.transform.Find(w.testWord).transform.Find(history.First.Value);
                Debug.Log("test word is " + w.testWord + " and first val is " + history.First.Value + " currentNode is " + currentNode);
                if (currentNode != null && history.Count > 1) { currentNode.transform.SetParent(null); }
                //Destroy(w.gameObject);
           }
           if (line != null){
                //Transform currentNode = w.transform.Find(w.testWord).transform.Find(history.First.Value);
                Debug.Log("test word is " + w.testWord + " and first val is " + history.First.Value + " currentNode is " + currentNode);
                //if (currentNode != null && history.Count > 1) { currentNode.transform.SetParent(null); }
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
        w.newWord(history.First.Value);
        yield return new WaitForSeconds(2);
    }
}

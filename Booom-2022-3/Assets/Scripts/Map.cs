using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject NE, SW;
    public GameObject Player;
    public GameObject[] Target;
    public GameObject targetSignPrefab;

    Vector2 mapRange;
    float mapUIW, mapUIH;
    Vector2 playerMapPos;
    Vector2[] targetMapPos;
    public Transform playerSign;
    GameObject[] targetSign; 
    
    // Start is called before the first frame update
    void Start()
    {
        mapRange.x = NE.transform.position.x - SW.transform.position.x;
        mapRange.y = NE.transform.position.z - SW.transform.position.z;

        mapUIW = transform.GetComponent<RectTransform>().rect.width;
        mapUIH = transform.GetComponent<RectTransform>().rect.height;


        DisplayTargetOnMap();
    }

    // Update is called once per frame
    void Update()
    {
        playerMapPos = new Vector2(Player.transform.position.x - SW.transform.position.x, Player.transform.position.z - SW.transform.position.z) / mapRange - Vector2.one * 0.5f;
        playerSign.GetComponent<RectTransform>().localPosition = playerMapPos * new Vector2(mapUIW, mapUIH);
    }

    void DisplayTargetOnMap()
    {
        targetMapPos = new Vector2[Target.Length];
        targetSign = new GameObject[Target.Length];
        for (int i = 0; i < Target.Length; i++)
        {
            targetSign[i] = Instantiate(targetSignPrefab, transform);
            targetMapPos[i] = new Vector2(Target[i].transform.position.x - SW.transform.position.x, Target[i].transform.position.z - SW.transform.position.z) / mapRange - Vector2.one * 0.5f;
            targetSign[i].GetComponent<RectTransform>().localPosition = targetMapPos[i] * new Vector2(mapUIW, mapUIH);
            targetSign[i].transform.GetChild(0).GetComponent<Text>().text = (i+1).ToString();
        }
    }
}

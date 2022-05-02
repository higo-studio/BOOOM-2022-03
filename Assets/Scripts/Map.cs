using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Map : MonoBehaviour
{

    public enum targetType { Q ,E , X };
    [System.Serializable]
    public class Target {
        public GameObject targetObj;
        public string targetText;
        public targetType targetType;
    }

    public GameObject NE, SW;
    public GameObject Player;

    public GameObject targetSignPrefab;

    Vector2 mapRange;
    float mapUIW, mapUIH;
    Vector2 playerMapPos;
    Vector2[] targetMapPos;
    public Transform playerSign;
    GameObject[] targetSign;

    public Sprite questionPic, exclamatoryPic, crossPic;

    [SerializeField]
    private List<Target> targets = new List<Target>();
    public Transform PointParent;

    // Start is called before the first frame update
    void Start()
    {
        mapRange.x = NE.transform.position.x - SW.transform.position.x;
        mapRange.y = NE.transform.position.z - SW.transform.position.z;

        mapUIW = transform.GetComponent<RectTransform>().rect.width;
        mapUIH = transform.GetComponent<RectTransform>().rect.height;

        // init targets
        var questions = PointParent
            .GetComponentsInChildren<TaskReleaser>(true)
            .Select(c => new Target()
            {
                targetObj = c.gameObject,
                targetText = c.transform.parent.name,
                targetType = targetType.Q
            });
        targets.AddRange(questions);

        var distinctions = PointParent
            .GetComponentsInChildren<TaskTarget>(true)
            .Select(c => new Target()
            {
                targetObj = c.gameObject,
                targetText = c.transform.parent.name,
                targetType = targetType.E
            });
        targets.AddRange(distinctions);
        DisplayTargetOnMap();
        UpdateVisible();
    }

    // Update is called once per frame
    void Update()
    {
        playerMapPos = new Vector2(Player.transform.position.x - SW.transform.position.x, Player.transform.position.z - SW.transform.position.z) / mapRange;
        playerSign.GetComponent<RectTransform>().localPosition = playerMapPos * new Vector2(mapUIW, mapUIH);

        UpdateVisible();
    }

    void DisplayTargetOnMap()
    {

        targetMapPos = new Vector2[targets.Count];
        targetSign = new GameObject[targets.Count];


        for (var i = 0; i < targets.Count; i++)
        {
            var target = targets[i];
            targetSign[i] = Instantiate(targetSignPrefab, transform);
            switch (target.targetType)
            {
                case targetType.Q:
                    targetSign[i].GetComponent<Image>().sprite = questionPic;
                    targetSign[i].GetComponent<Image>().color = new Color(0, 0.6f, 0);
                    break;
                case targetType.E:
                    targetSign[i].GetComponent<Image>().sprite = exclamatoryPic;
                    targetSign[i].GetComponent<Image>().color = new Color(1, 0.9f, 0.45f);
                    break;
                case targetType.X:
                    targetSign[i].GetComponent<Image>().sprite = crossPic;
                    targetSign[i].GetComponent<Image>().color = new Color(0.7f, 0.25f, 0.25f);
                    break;
                default:
                    break;
            }
            targetMapPos[i] = new Vector2(
                target.targetObj.transform.position.x - SW.transform.position.x,
                target.targetObj.transform.position.z - SW.transform.position.z) / mapRange - Vector2.one * 0.5f;
            targetSign[i].GetComponent<RectTransform>().localPosition = targetMapPos[i] * new Vector2(mapUIW, mapUIH);
            targetSign[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = target.targetText;
        }
    }

    void UpdateVisible()
    {
        for(var i = 0; i < targets.Count; i++)
        {
            var t = targets[i];
            var sign = targetSign[i];
            sign.SetActive(t.targetObj.transform.position.y < 400);
        }
    }
}

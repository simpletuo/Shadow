using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestsUI : MonoBehaviour
{
    public GameObject bottom;
    public GameObject left;
    public GameObject questChainPrefab;
    public GameObject questChainArrowPrefab;

    public GameObject panel;
    public TMP_Text title;
    public TMP_Text desc;
    public TMP_Text gold;
    public TMP_Text exp;

    void Awake()
    {
        bottom = transform.GetChild(0).gameObject;
        left = bottom.transform.GetChild(0).gameObject;
        panel = bottom.transform.GetChild(1).gameObject;

        TMP_Text[] textArr = panel.GetComponentsInChildren<TMP_Text>(true);
        title = textArr[1];
        desc = textArr[2];
        gold = textArr[3];
        exp = textArr[4];
    }

    void Update()
    {
        if (PartyController.scriptInstance == null || PartyController.quest == null)
            return;
        Quest quest = PartyController.quest;

        // set texts in quest window
        title.text = quest.title;
        desc.text = quest.desc;
        gold.text = quest.goldReward.ToString();
        exp.text = quest.expReward.ToString();
    }


    void OnEnable()
    {
        if (PartyController.scriptInstance == null || PartyController.quest == null)
            return;

        if (PartyController.questChain != null)
        {
            QuestChain qc = PartyController.questChain;
            left.SetActive(true);

            Vector3 coords = left.transform.GetChild(0).localPosition;
            for (int i = 0; i < qc.quests.Count; i++)
            {
                // no space for now, implement scroll (FUTURE)
                if (i > 3)
                    break; 

                Quest q = qc.quests[i];
                coords = new Vector3(coords.x, coords.y - 100, coords.z);
                GameObject qcTitle = Instantiate(questChainPrefab, coords, Quaternion.identity);
                qcTitle.transform.SetParent(left.transform, false);
                qcTitle.GetComponent<TMP_Text>().text = q.title;

                // no arrow if last quest in quest chain
                if (i == qc.quests.Count - 1)
                    break;
                coords = new Vector3(coords.x, coords.y - 100, coords.z);
                GameObject arrow = Instantiate(questChainArrowPrefab, coords, Quaternion.identity);
                arrow.transform.SetParent(left.transform, false);
            }
        }


    }

}
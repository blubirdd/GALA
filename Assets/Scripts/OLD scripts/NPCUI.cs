using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Image prefabUI;
    private Image uiUse;

    private Transform tr_head;
    private Vector3 offSet = new Vector3(0, 2f, 0);

    void Start()
    {
        uiUse = Instantiate(prefabUI, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        tr_head = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        uiUse.transform.position = Camera.main.WorldToScreenPoint(tr_head.position + offSet);

        
    }
}

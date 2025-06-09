using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHP : MonoBehaviour
{
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.pc.transform.position);
    }
}

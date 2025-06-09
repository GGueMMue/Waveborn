using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    RectTransform rect;
    public GameObject child;
    Item[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = child.GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

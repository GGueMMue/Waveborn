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
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next() 
    { 
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] rand = new int[3];
        while(true)
        {
            rand[0] = Random.Range(0, items.Length);
            rand[1] = Random.Range(0, items.Length);
            rand[2] = Random.Range(0, items.Length); 
            if (rand[0] != rand[1] && rand[2] != rand[1] && rand[2] != rand[0])
                break;
        }

        for(int i = 0; i < rand.Length; i++)
        {
            Item randItem = items[rand[i]];

            if(randItem.level == randItem.ItemData.damages.Length)
            {
                items[4].gameObject.SetActive(true);
            }
            else
                randItem.gameObject.SetActive(true);
        }
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

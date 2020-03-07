using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
public class Bag
{
    private List<int> contents;
    private List<int> removed;
    private int size;

    public Bag()
    {
        contents = new List<int>();
        removed = new List<int>();
    }

    public Bag(int Size)
    {
        contents = new List<int>();
        removed = new List<int>();

        for (int i = 0; i < Size; i++)
        {
            contents.Add(i);
        }
    }

    public int DrawFromBag()
    {
        //grab a random item from bag, if bag is empty, refill bag
        if (contents.Count <= 0)
        {
            //refill bag
            Debug.Log("refill bag!");
            contents = removed;
        }
        int randomIndex = (int)Random.Range(0, contents.Count - 1);

        int result = contents[randomIndex];
        removed.Add(result);
        contents.RemoveAt(randomIndex);
        return result;
    }
}

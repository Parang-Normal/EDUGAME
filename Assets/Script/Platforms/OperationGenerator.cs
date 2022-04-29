using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabAssts
{
    public GameObject NumberBlockPrefab;
    public GameObject OperationBlockPrefab;
    public Transform EquationTransform;
}

public class OperationGenerator : MonoBehaviour
{
    public PrefabAssts Dependencies;

    [Range(1, 4)]
    public int Operations;

    public GameObject Portal;
    public List<OperationBlock> BlockList;
    public List<GameObject> ChildList;

    public void initialize()
    {
        ClearChildren();

        //Spawn an equation alternating between number and operation
        for(int i = 0; i < (Operations * 2) + 1; i++)
        {
            if(i >= Dependencies.EquationTransform.childCount)
            {
                //Even, Number block
                if (i % 2 == 0)
                {
                    GameObject temp = Instantiate(Dependencies.NumberBlockPrefab, Dependencies.EquationTransform);
                    temp.transform.position = new Vector3(0 + (Dependencies.EquationTransform.childCount * 0.64f), 0, 1);
                }
                //Odd, Operation block
                else if (i % 2 == 1)
                {
                    GameObject temp = Instantiate(Dependencies.OperationBlockPrefab, Dependencies.EquationTransform);
                    temp.GetComponent<OperationBlock>().ParentGenerator = this;
                    temp.transform.position = new Vector3( 0 + (Dependencies.EquationTransform.childCount * 0.64f), 0, 1);
                }
            }
        }
    }

    private void Start()
    {
        for(int i = 0; i < Dependencies.EquationTransform.childCount; i++)
        {
            //Even, Number block
            if (i % 2 == 0)
            {
                ChildList.Add(Dependencies.EquationTransform.GetChild(i).gameObject);
            }
            //Odd, Operation block
            else if (i % 2 == 1)
            {
                ChildList.Add(Dependencies.EquationTransform.GetChild(i).gameObject);
                BlockList.Add(Dependencies.EquationTransform.GetChild(i).gameObject.GetComponent<OperationBlock>());
            }
        }
    }

    private void ClearChildren()
    {

        for(int i = 0; i < ChildList.Count; i++)
        {
            GameObject temp = ChildList[i].gameObject;
            DestroyImmediate(temp);
        }

        BlockList.Clear();
        ChildList.Clear();
    }

    public void CheckOperations()
    {
        int passed = 0;

        for(int i = 0; i < BlockList.Count; i++)
        {
            if (BlockList[i].IsOperationCorrect())
            {
                passed++;
            }
        }

        //If all operations are correct, activate end portal
        if(passed == Operations)
        {
            //Activate end portal
            Portal.SetActive(true);
            Debug.Log("Activate portal");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

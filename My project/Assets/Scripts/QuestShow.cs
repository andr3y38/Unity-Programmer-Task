using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestShow : MonoBehaviour
{
    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(QuestIn(1.5f));
    }
    
    private IEnumerator QuestIn(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        animator.Play("In"); 
        
        StartCoroutine(QuestOut(3f));
    }
    
    private IEnumerator QuestOut(float delay)
    {
        yield return new WaitForSeconds(delay);

        animator.Play("Out"); 
    }
}

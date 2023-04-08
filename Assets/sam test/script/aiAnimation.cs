using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAnimation(Vector3 modifiedAngle)
    {
        if (modifiedAngle.x < 0)
        {
            _animator.SetFloat("turn", 1);
        }
        else if (modifiedAngle.x > 0)
        {
            _animator.SetFloat("turn", -1);
        }
        else
        {
            _animator.SetFloat("turn", 0);
        }

        if (modifiedAngle.y < 0)
        {

            _animator.SetFloat("vertical", -1);
        }
        else if (modifiedAngle.y > 0)
        {
            _animator.SetFloat("vertical", 1);
        }
        else
        {
            _animator.SetFloat("vertical", 0);
        }

    }
}

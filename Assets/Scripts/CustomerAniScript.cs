using UnityEngine;

public class CustomerAniScript : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] AnimationClip[] animations;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //int randomIndex = Random.Range(0, animations.Length);
        //animator.Play(animations[randomIndex].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

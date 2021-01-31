using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GeneratorManager : MonoBehaviour
{
    [SerializeField]
    AnimationClip anim;

    Animator animator;

    bool beginGame = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (anim)
        {
            AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var a in aoc.animationClips)
            {
                if (a.name == "emptyClip")
                {
                    anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, anim));
                }
            }
            aoc.ApplyOverrides(anims);
            animator.runtimeAnimatorController = aoc;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (StageLink.instance.ready() && !beginGame) {
            beginGame = true;
            animator.SetTrigger("beginGame");
        }
    }
}

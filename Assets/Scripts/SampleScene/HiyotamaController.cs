using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiyotamaController : MonoBehaviour
{
	[SerializeField] Animator hiyotamaAnimator;
	private bool IsCry;

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			hiyotamaAnimator.SetBool("Cry", IsCry);
			IsCry = !IsCry;
		}
    }
}

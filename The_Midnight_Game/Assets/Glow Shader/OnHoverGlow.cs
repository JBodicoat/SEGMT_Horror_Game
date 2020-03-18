//Created by Dan 10/03/2020

using UnityEngine;

public class OnHoverGlow : MonoBehaviour
{
	public Material glowing;
	public Material nonGlowing;

	void OnMouseOver()
	{
		GetComponent<Renderer>().material = glowing;
	}

	void OnMouseExit()
	{
		GetComponent<Renderer>().material = nonGlowing;
	}
}

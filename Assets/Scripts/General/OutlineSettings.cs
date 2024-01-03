using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomizedPackages.Outline
{
	[CreateAssetMenu(fileName = "New Outline Settings", menuName = "Customized Packages/Outline/Outline Settings")]
	public class OutlineSettings : ScriptableObject
	{
		[SerializeField]
		public Outline.Mode OutlineMode;

		[SerializeField]
		public Color OutlineColor = Color.white;

		[SerializeField, Range(0f, 10f)]
		public float OutlineWidth = 2f;

        [Header("Optional")]
		[SerializeField, Tooltip("Precompute enabled: Per-vertex calculations are performed in the editor and serialized with the object. "
		+ "Precompute disabled: Per-vertex calculations are performed at runtime in Awake(). This may cause a pause for large meshes.")]
		public bool PrecomputeOutline;
	}

}
using UnityEngine;
using UnityEngine.UI;

namespace MyExtensions
{
	public static class ImageTransparencyExtension
	{
		public static void SetTransparency(this Image p_image, float p_transparency)
		{
			if (p_image != null)
			{
				Color __alpha = p_image.color;
				__alpha.a = p_transparency;
				p_image.color = __alpha;
			}
		}
	}

}

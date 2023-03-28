Shader "Custom/DisableZWrite"
{
	Properties{}

		SubShader{

			Tags {
				"RenderType" = "Opaque"
			}

			Pass {
				ZWrite Off
			}
	}
}

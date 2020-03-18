//Created by Dan 10/03/2020

Shader "Custom/ApplyTexture"
{
	Properties//Variables
	{
		_MainTex("Main Texture (RBG)", 2D) = "white" {}//Texture property.
		_Color("Color", Color) = (1,1,1,1)//Color property.
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert

			#pragma fragment frag

			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float4 _Color;
			sampler2D _MainTex;

			v2f vert(appdata IN)
			{
				v2f OUT;

				OUT.pos = UnityObjectToClipPos(IN.vertex);//Puts the object into the camera view.
				OUT.uv = IN.uv;

				return OUT;
			}


			fixed4 frag(v2f IN) : SV_Target
			{
				float4 texColor = tex2D(_MainTex, IN.uv);//Wraps texture around the uv's.
				return texColor * _Color;//Tints the texture.
			}

			ENDCG
		}
	}
}

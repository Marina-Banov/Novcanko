////////////////////////////////////
// 
//	Written by NinjaPretzel 
//	2017
//	ninjapretzel@gmail.com
//	For Unity Asset Store
//

Shader "Bar/Simple" {
	Properties {
		// Toggles the bar filling direction
		[Toggle(LEFT_TO_RIGHT)] _LEFT_TO_RIGHT("Left To Right", Float) = 1
		
		// Used to provide information about the physical size of the bar
		// Mostly to make sure it has the correct aspect ratio for the border.
		// the 'Fancy' version also uses this for non-screenspace textures.
		_SizeInfo ("Size information (_SizeInfo), xy = Width/Height", Vector) = (8,2, 0,0)
		
		// Amount of fill.
		// [0, _Fill] is considered 'Full'
		// [_Fill, 1] is considered 'Empty'
		_Fill ("Fill Amount (_Fill)", Range(0, 1)) = .5
		
		// Delta of _Fill
		// If this is zero, there is no third segment.
		// If this is > zero, the third segment is 'filling'
		//		Is tinted with _PosColor, and is the region [_Fill - _Delta, _Fill]
		// If this is < zero, the third segment is 'emptying'
		//		Is tinted with _NegColor, and is the region [_Fill, _Fill - _Delta]
		_Delta ("Delta Fill (_Delta)", Range(-1, 1)) = .2
		
		// Main texture, basically just overlaid across the bar. 
		// Vertical gradients work best
		_MainTex ("Main Texture (Bar Background)", 2D) = "white" {}
		
		// Color tint applied after everything else. 
		_Color ("Primary Color Tint", Color) = (1,1,1,1)
		
		// Color of border region.
		_BorderColor ("Border Color", Color) = (0,0,0,1)
		
		// Thickness of border region. Do you like it Thicc or Slim?
		_Border ("Border Thickness", Range(0, .03)) = .01
		
		// Color blended with _MainTex
		_BGColor ("Background Color (RGB * A)", Color) = (1,1,1,.75)
		// Color blended with the 'Filled' region
		_FillColor ("Filled Color Tint (RGB * A)", Color) = (0,.66,0,.5)
		// Color blended with the 'Empty' region
		_EmptyColor ("Empty Color Tint (RGB * A)", Color) = (.5,0,0,.5)
		// Color blended with the 'Filling' region
		_PosColor ("Positive Color Tint (RGB * A)", Color) = (0,.22,.66,.88)
		// Color blended with the 'Emptying' region
		_NegColor ("Negative Color Tint (RGB * A)", Color) = (.66,.22,0,1.)
		// Multiplier for the 4 above tint colors. Allows for HDR.
		_TintMultiplier ("Tint Multiplier", Range(0, 8)) = 1
		
		// Everything below is used to work with the UnityUI system.
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
		_ClipRect ("Clip Rect", vector) = (-32767, -32767, 32767, 32767)

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}

	SubShader {
		Tags { 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil {
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass {
			CGPROGRAM
				
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile __ UNITY_UI_ALPHACLIP
				#pragma multi_compile __ LEFT_TO_RIGHT
				
				struct appdata_t {
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					//float4 vertex   : SV_POSITION;
					fixed4 color    : COLOR;
					half2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					
				};
				
				float4 _SizeInfo;
				float _Fill;
				float _Delta;
				
				sampler2D _MainTex;
				fixed4 _Color;
				fixed4 _BorderColor;
				float _Border;
				
				float4 _BGColor;
				float4 _FillColor;
				float4 _EmptyColor;
				float4 _PosColor;
				float4 _NegColor;
				
				float _TintMultiplier;
				float _LayerBlend;
				
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;

#if UNITY_VERSION < 530
				bool _UseClipRect;
#endif
				v2f vert(appdata_t IN, out float4 outpos : SV_POSITION) {
					v2f OUT;
					OUT.worldPosition = IN.vertex;
					
					outpos = UnityObjectToClipPos(OUT.worldPosition);
					
					//OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
					OUT.texcoord = IN.texcoord;
					
					
					#ifdef UNITY_HALF_TEXEL_OFFSET
					OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
					#endif
					
					
					OUT.color = IN.color;
					return OUT;
				}


				fixed4 frag(v2f IN, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target {
					//float aspect = _ScreenParams.x / _ScreenParams.y;
					half4 inputColor = IN.color;
					half4 barColor = tex2D(_MainTex, IN.texcoord);
					float time = _Time.y;
					
					half4 blendColor;
					
					float fn = _Fill;
					float ft = _Fill - _Delta;
					float fmin = min(fn, ft);
					float fmax = max(fn, ft);
					float y = IN.texcoord.y;
					
					#ifdef LEFT_TO_RIGHT
					float p = IN.texcoord.x;
					#else
					float p = 1.0f - IN.texcoord.x;
					#endif
					
					// Near a border?
					float dborderlerp = 0; 
					if (_Border > 0) { 
						float dborderx = min(p, abs(1.0-p)) * _SizeInfo.x / 4.0;
						float dbordery = min(y, abs(1.0-y)) * _SizeInfo.y / 4.0;
						float dborder = min(dborderx, dbordery);
						if (dborder < .000001) { return _BorderColor; }
					
						dborderlerp = _Border / dborder; 
						if (dborderlerp > 1) { return _BorderColor; }
					}
					
					if (p < fmin) { blendColor = _FillColor; }
					else if (p < fmax) { blendColor = (_Delta > 0) ? _PosColor : _NegColor; }
					else { blendColor = _EmptyColor; }
					
					half4 color = barColor * inputColor;
					color.rgb *= _BGColor.rgb * _BGColor.a * 4.0;
					color.rgb *= blendColor.rgb * (blendColor.a * _TintMultiplier);
					color *= _Color;
					
					color = lerp(color, _BorderColor, dborderlerp);
				#if UNITY_VERSION < 530
					if (_UseClipRect) {
						color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
					}
				#else
					color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
				#endif
					
					#ifdef UNITY_UI_ALPHACLIP
					clip (color.a - 0.001);
					#endif

					return clamp(color, half4(0,0,0,0), half4(1,1,1,1));
				}
			ENDCG
		}
	}
}










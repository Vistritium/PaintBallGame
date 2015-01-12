Shader "Custom/CircleProgress" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_Angle ("Angle (Float)", Float) = 0
		_Color ("Tint", Color) = (1.0, 0.6, 0.6, 1.0)
		_RangePercentMax ("RangePercentMax", Float) = 100
		_RangePercentMin ("RangePercentMin", Float) = 60
    }

    SubShader {        
	Tags{"Queue" = "Transparent" }
	Pass {
			Blend SrcAlpha OneMinusSrcAlpha 
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            uniform sampler2D _MainTex;
			uniform float _Angle;
			uniform fixed4 _Color;
			uniform float _RangePercentMax;
			uniform float _RangePercentMin;

            float4 frag(v2f_img i) : COLOR 
			{		 
				float4 result = tex2D(_MainTex, i.uv);
				float angle = atan2(i.uv.x-0.5 , i.uv.y-0.5);
		
				if(angle > _Angle)
				{				
					result.a = 0;
				}

				half2 toCurrent = half2(i.uv.x - 0.5, i.uv.y - 0.5);
				half vecLength = length(toCurrent);
				if(vecLength > (_RangePercentMax / 200)){
					result.a = 0;
				}

				if(vecLength < _RangePercentMin / 200){
					result.a = 0;
				}



                return result*_Color;  
			}
            ENDCG
        }
    }
}
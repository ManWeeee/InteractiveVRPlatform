Shader "Custom/AlwaysVisible"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1) // Editable color
    }
    SubShader
    {
        Tags {"Queue"="Overlay" "RenderType"="Transparent"} // Renders on top
        Pass
        {
            ZTest Always  // Always visible
            ZWrite Off    // Doesn't affect depth buffer
            Blend SrcAlpha OneMinusSrcAlpha // Allows transparency
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };

            struct v2f {
                float4 pos : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color; // Use customizable color
            }
            ENDCG
        }
    }
}

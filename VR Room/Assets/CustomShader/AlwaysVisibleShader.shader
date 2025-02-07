Shader "Custom/AlwaysVisibleVR"
{
    Properties
    {
        _Color ("Main Color", Color) = (1, 0, 0, 1) // Default: Solid Red
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }  // Renders last

        Pass
        {
            ZTest Always      // Always render on top
            ZWrite Off        // Don't write to depth buffer
            Cull Off          // Render both sides
            Blend SrcAlpha OneMinusSrcAlpha  // Enable transparency

            CGPROGRAM
            #pragma multi_compile_instancing
            #pragma multi_compile _ UNITY_SINGLE_PASS_STEREO
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
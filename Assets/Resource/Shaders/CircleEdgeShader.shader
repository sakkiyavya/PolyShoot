Shader "Unlit/CircleEdgeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)

        _Offset("Offset", Vector) = (0, 0, 0, 0)

        _ColorPower("ColorPower", Int) = 5
        _Scale("Scale", Range(0.1, 0.6)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color1;
            float4 _Color2;
            float4 _Offset;

            float2 p1;
            float2 p2;
            float2 p3;

            int _ColorPower;
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Color1;

                float d = abs(length(i.uv.xy - _Offset) - _Scale);
                d = 1 - d;
                d = pow(d, _ColorPower);
                if(length(i.uv.xy - _Offset) - _Scale > 0)
                    d = pow(d, 3);
                col.w *= d;

                return col;
            }
            ENDCG
        }
    }
}

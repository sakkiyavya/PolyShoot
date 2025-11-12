Shader "Unlit/TriangleEdgeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)

        _Offset("Offset", Vector) = (0, 0, 0, 0)

        _ColorPower("ColorPower", Float) = 5
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
                float4 positionOS : TEXCOORD1;
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

            float _ColorPower;
            float _Scale;

            float HeronC(float a, float b, float c)
            {
                float p = (a + b + c) / 2;
                return sqrt(p * (p - a) * (p - b) * (p - c));
            }

            float DistanceToLine(float2 linePoint1, float2 linePoint2, float2 p)
            {
                float a = length(linePoint1 - linePoint2);
                float b = length(linePoint1 - p);
                float c = length(linePoint2 - p);
                float S = HeronC(a, b, c);
                if(dot(linePoint1 - linePoint2, p - linePoint2) < 0)
                    return length(p - linePoint2);
                if(dot(linePoint2 - linePoint1, p - linePoint1) < 0)
                    return length(p - linePoint1);

                return 2 * S / a;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.positionOS = v.vertex;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                p1 = float2(0, 1.0) * _Scale + _Offset.xy;
                p2 = float2( - sqrt(3) / 2, - 1.0 / 2) * _Scale + _Offset.xy;
                p3 = float2( sqrt(3) / 2, - 1.0 / 2) * _Scale + _Offset.xy;

                float h12 = DistanceToLine(p1, p2, i.uv.xy);
                float h13 = DistanceToLine(p1, p3, i.uv.xy);
                float h23 = DistanceToLine(p2, p3, i.uv.xy);

                float t12 = (1 - h12 / _Scale);
                t12 = saturate(t12);
                t12 = pow(t12, _ColorPower);

                float t13 = (1 - h13 / _Scale);
                t13 = saturate(t13);
                t13 = pow(t13, _ColorPower);

                float t23 = (1 - h23 / _Scale);
                t23 = saturate(t23);
                t23 = pow(t23, _ColorPower);

                float h = min(h12,min(h13, h23));

                fixed4 col = _Color1;
                col.w = 0;
                col.w += t12 / 3;
                col.w += t13 / 3;
                col.w += t23 / 3;

                return col;
            }
            ENDCG
        }
    }
}

Shader "Unlit/SquareEdgeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)

        _Offset("Offset", Vector) = (0, 0, 0, 0)
        _XYScale("XYScale", Vector) = (1, 1, 0, 0)

        _ColorPower("ColorPower", Int) = 5
        _Scale("Scale", Range(0.1, 2.0)) = 0.5

        
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off

        Pass
        {
            HLSLPROGRAM
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
                float4 p : TEXCOORD1;
                float4 scale : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            half4 _Color1;
            half4 _Color2;
            float4 _Offset;
            float4 _XYScale;

            float2 p1;
            float2 p2;
            float2 p3;

            int _ColorPower;
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
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);


                float4 p = float4 (0, 0, 0, 0);
                p.xy = o.uv.xy - _Offset;
                o.p = _XYScale * p;
                float4 scale = float4(1, 1, 0, 0);
                o.scale = _XYScale * scale;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {


                float2 v1 = float2(0.5, 0.5) * _Scale * i.scale.xy;
                float2 v2 = float2(-0.5, 0.5) * _Scale * i.scale.xy;
                float2 v3 = float2(-0.5, -0.5) * _Scale * i.scale.xy;
                float2 v4 = float2(0.5, -0.5) * _Scale * i.scale.xy;


                float d1 = 1 - DistanceToLine(v1, v2, i.p.xy) / _Scale;
                float d2 = 1 - DistanceToLine(v2, v3, i.p.xy) / _Scale;
                float d3 = 1 - DistanceToLine(v3, v4, i.p.xy) / _Scale;
                float d4 = 1 - DistanceToLine(v4, v1, i.p.xy) / _Scale;

                d1 = saturate(d1);
                d2 = saturate(d2);
                d3 = saturate(d3);
                d4 = saturate(d4);

                d1 = pow(d1, _ColorPower);
                d2 = pow(d2, _ColorPower);
                d3 = pow(d3, _ColorPower);
                d4 = pow(d4, _ColorPower);

                half4 col = _Color1;
                col.w = 0;
                col.w += d1 / 4;
                col.w += d2 / 4;
                col.w += d3 / 4;
                col.w += d4 / 4;

                return col;
            }
            ENDHLSL
        }
    }
}

Shader "Unlit/ CrosshairCircleEdgeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color1", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)

        _Offset("Offset", Vector) = (0, 0, 0, 0)

        _Dir("Dir", Vector) = (0, 0, 0, 0)
        _Angle("Angle", Float) = 10
        _CrosshairRadius("_CrosshairRadius", Float) = 0.02
        _TransformScale("TransformScale", Float) = 1

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
            float4 _Dir;

            float2 p1;
            float2 p2;
            float2 p3;

            float _Scale;
            float _Angle;
            float _CrosshairRadius;
            float _TransformScale;

            int _ColorPower;

            float angleMinus(float2 p1, float2 p2)
            {
                float dp = dot(p1, p2);
                float len = length(p1) * length(p2);

                if (len < 1e-6) return 0;

                float cosTheta = clamp(dp / len, -1.0, 1.0);

                return degrees(acos(cosTheta));
            }

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
                d = pow(d, _ColorPower * _TransformScale);
                if(length(i.uv.xy - _Offset) - _Scale > 0)
                    d = pow(d, 3);
                col.w *= d;

                if(length(i.uv.xy - _Offset - normalize(_Dir) * _Scale) < _CrosshairRadius / _TransformScale)
                    col.w = length(i.uv.xy - _Offset - normalize(_Dir) * _Scale) / _CrosshairRadius / _TransformScale;
                    // col.w = 1;

                if(length(i.uv.xy - _Offset) > 0)
                {
                    float am = angleMinus(i.uv.xy - _Offset, _Dir);
                    if(abs(am - _Angle / 2)  < 0.1 || abs(am + _Angle / 2) < 0.1)
                    {
                        if(length(i.uv.xy - _Offset) - _Scale < 0)
                            col.w = length(i.uv.xy - _Offset) * length(i.uv.xy - _Offset) * length(i.uv.xy - _Offset);
                    }
                    col.w *= min(1 - min((am - _Angle / 2) / 5, 1), 1.5);
                }

                return col;
            }
            ENDCG
        }
    }
}

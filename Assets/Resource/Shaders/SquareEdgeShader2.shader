Shader "Unlit/SquareEdgeShader2"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _EdgePower ("Edge Power", Range(1,10)) = 4
        _Scale ("Square Scale", Range(0.1, 2.0)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 local : TEXCOORD0;   // 模型空间坐标
            };

            float4 _Color;
            float _EdgePower;
            float _Scale;

            // 顶点着色器：输出模型空间坐标
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.local = v.vertex.xyz;   // 保留模型空间坐标
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 使用模型空间坐标计算方形
                float2 p = i.local.xy;  // 模型空间平面

                // 方形区域（[-0.5, 0.5] * scale）
                float halfSize = 0.5 * _Scale;

                // 计算点到四条边的距离（方形的 SDF）
                float2 d = halfSize - abs(p);

                float dist = min(d.x, d.y);
                dist = saturate(dist / halfSize);
                dist = pow(dist, _EdgePower);

                float alpha = dist;

                return float4(_Color.rgb, alpha * _Color.a);
            }

            ENDHLSL
        }
    }
}

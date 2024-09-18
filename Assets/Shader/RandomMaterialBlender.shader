Shader "Custom/RandomMaterialBlenderShader"
{
    Properties
    {
        _ColorA("Color A", Color) = (1, 0, 0, 1) // �⊮����1�ڂ̐F
        _ColorB("Color B", Color) = (0, 0, 1, 1) // �⊮����2�ڂ̐F
        _BlendSpeed("Blend Speed", Range(0, 10)) = 1.0 // �⊮���x
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            fixed4 _ColorA;
            fixed4 _ColorB;
            float _BlendSpeed;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // ���ԂɊ�Â��ĕ⊮�������v�Z���Asin�֐��Ŕg��ɂ���
                float blendFactor = (sin(_Time.y * _BlendSpeed * 3.14159) + 1) * 0.5;

            // �F�̕⊮
            fixed4 blendedColor = lerp(_ColorA, _ColorB, blendFactor);

            return blendedColor;
        }
        ENDCG
    }
    }
        FallBack "Diffuse"
}



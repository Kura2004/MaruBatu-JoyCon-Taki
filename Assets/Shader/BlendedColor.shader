Shader "Custom/BlendedColor"
{
    Properties
    {
        _ColorA("Color A", Color) = (1,1,1,1) // colorA�̏����l
        _ColorB("Color B", Color) = (0,0,0,1) // colorB�̏����l
        _BlendFactor("Blend Factor", Range(0,1)) = 0.5 // blendFactor�̏����l
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

            // �V�F�[�_�[�Ŏg�p����v���p�e�B
            fixed4 _ColorA;
            fixed4 _ColorB;
            float _BlendFactor;

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // �u�����h���ꂽ�F���v�Z����
            fixed4 frag(v2f i) : SV_Target
            {
                // Color.Lerp�̃V�F�[�_�[��
                fixed4 blendedColor = lerp(_ColorA, _ColorB, _BlendFactor);
                return blendedColor;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}

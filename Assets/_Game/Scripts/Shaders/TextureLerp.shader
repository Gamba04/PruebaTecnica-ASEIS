Shader "Gamba/Texture Lerp"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)

        [Space(20)]
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0

        [Space(20)]
        _Factor ("Factor", Range(0, 1)) = 0.0
        _Treshold("Treshold", Range(0, 1)) = 0.0
        _Noise ("Noise", 2D) = "white" {}
        _MainTex ("Albedo", 2D) = "white" {}
        _OverrideTexture ("Override Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _Noise;
        sampler2D _OverrideTexture;

        float _Factor;
        float _Treshold;
        float _NormalAmount;

        fixed4 _Color;
        half _Glossiness;
        half _Metallic;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float noise = tex2D(_Noise, IN.uv_MainTex).r;
            float remappedFactor = _Factor * 2 - 1;
            float noiseFactor = saturate(noise + remappedFactor);
            noiseFactor = noiseFactor >= _Treshold ? 1 : 0;

            fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 overrideTex = tex2D(_OverrideTexture, IN.uv_MainTex);
            o.Albedo = lerp(mainTex.rgb, overrideTex.rgb, noiseFactor);

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = overrideTex.a;
        }

        ENDCG
    }

    FallBack "Diffuse"
}
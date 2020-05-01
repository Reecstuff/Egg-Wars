Shader "Custom/UV_Moving"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _SinFrequency ("SinFrequency", float) = 4.0 // Faster, more sine curves
        _SinInterval ("SinInterval", float) = 10.0 // Between which values moves the sine
        _SinStart ("SinStart", float) = 0.5 // Start of Value

    }
    SubShader
    {
        pass
        {
            CGPROGRAM //------------BEGIN SHADER CODE

            #pragma vertex VS
            #pragma fragment PS

            uniform float4 _Color;
            uniform sampler2D _MainTex;
            uniform float _Glossiness;
            uniform float _Metallic;
            uniform float _SinFrequency;
            uniform float _SinInterval;
            uniform float _SinStart;

            struct VIn
            {
                float4 pos : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct VOut
            {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD0;
            };

            VOut VS(VIn input)
            {
                VOut output;

                output.pos = UnityObjectToClipPos(input.pos);
                output.uv = input.uv;
                
                return output;
            }

            float4 PS(VOut input) : COLOR
            {
                _Color.rgb += sin(_Time * _SinFrequency) / _SinInterval + _SinStart;

                return _Color / tex2D(_MainTex, float2(input.uv.x, input.uv.y + _Time.x * 3));
            }
            ENDCG
        }

    }
}

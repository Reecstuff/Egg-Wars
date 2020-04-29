Shader "Custom/Silhoutte" 
{
    // Quallenshader
	Properties
	{
		_ColorValue ("CValue", Color) = (0, 0, 1, 0.3)
	}
	SubShader				// For every GPU own SubShader
	{
        Tags { "Queue" = "Transparent"} // Ändere Renderqueue auf 3000 (Desto größer desto später wird der Shader gerendert und kann nicht überzeichnet werden)
		Pass
		{
            ZWrite Off // Alle Farbwerte werden angezeigt
            Blend SrcAlpha OneMinusSrcAlpha // 1 - Alphawert

			CGPROGRAM		// ---------- Begin Cg SHADER CODE

			#pragma vertex VS
			#pragma fragment PS
            
            #include "UnityCG.cginc"
			
            uniform float _Yvalue;
			uniform float4 _ColorValue;


			struct VIn
			{
				float4 pos : POSITION;
                float3 normal : NORMAL; // Normal zu Oberfläche
			};
			
            struct VOut
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
                float3 viewDir : TEXCOORD1; // Blickrichtung
            };

			VOut VS(VIn input)
			{
                VOut output;

                float4x4 modelMatrix = unity_ObjectToWorld;
                float4x4 modelMatrixInverse = unity_WorldToObject;

                output.normal = normalize(
                    mul(float4(input.normal,1), modelMatrixInverse).xyz); // Normalen nach Welt berechnen und normalisieren
                output.viewDir = normalize(
                    _WorldSpaceCameraPos - mul(modelMatrix, input.pos).xyz
                ); // Normalisieren, um später Dotprodukt zu berechnen (Länge des Vektors muss 1 sein)

                output.pos = UnityObjectToClipPos(input.pos);
				return output;
			}
				
			float4 PS(VOut input) : COLOR
			{
                float3 normalDir = normalize(input.normal); // Zwischen den beiden Funktionen liegen 4 Shaderstages
                float3 viewDir = normalize(input.viewDir);

                float Opac = min(1.0, _ColorValue.a / abs(dot(viewDir, normalDir))); // abs() nimmt nur positive Werte

				//_ColorValue.rgb = _SinTime.rgb; // Ob in einer Zeile oder in mehreren ist egal

				return float4(_ColorValue.rgb, Opac);
				
			}

			ENDCG			// ---------- END SHADER CODE
		}

	}
}
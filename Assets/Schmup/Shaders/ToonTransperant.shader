Shader "Unlit/ToonTransperant"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _AmbientColor("Ambient Light Color", Color) = (0.2,0.2,0.2,1)
        _Gloss("Glossiness", float) = 30 
        _DiffuseCelCount("Diffuse Cel Count", float) = 4
        _SpecularCelCount("Specular Cel Count", float) = 2
        _RimStrength("Rim Lighting Strength", float) = 0.5
        _RimStep("Rim Edge Step", range(1, 0)) = 0.9
        _RimColor("Rim Color", Color) = (1,1,1,1)
        
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha
            // make fog work
            #pragma multi_compile_fog

            #include "Lighting.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct VertexOutput
            {
                UNITY_FOG_COORDS(1)
                float4 clipSpacePos : SV_POSITION;
                float3 normal : NORMAL;
                float3 worldPos : TEXCOORD0;
            };

            float4 _Color;
            float4 _AmbientColor;
            float _Gloss;
            float _DiffuseCelCount;
            float _SpecularCelCount;
            float _RimStrength;
            float _RimStep;
            float4 _RimColor;
            

            VertexOutput vert (VertexInput i)
            {
                VertexOutput o;
                o.normal = UnityObjectToWorldNormal(i.normal);
                o.worldPos = mul(unity_ObjectToWorld, i.vertex);
                o.clipSpacePos = UnityObjectToClipPos(i.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float Posterize(float pStepCount, float pValue)
            {
                return floor(pStepCount * pValue) / pStepCount;
            }
            
            float4 frag (VertexOutput o) : SV_Target
            {
                float4 col = _Color;
                float alpha = _Color.a;
                float3 normal = normalize(o.normal);

                // cam
                float3 camPos = _WorldSpaceCameraPos.xyz;
                float3 fragToCam = camPos - o.worldPos;

                // view
                float3 viewDir = normalize(fragToCam);
                
                // light variables
                float3 lightColor = _LightColor0.rgb;
                float3 lightDir = _WorldSpaceLightPos0.xyz;

                // diffuse
                float diffuseFallOff = max(0, dot(lightDir, normal)); // clamp between 0 and value
                float3 rampedDiffuse = Posterize(_DiffuseCelCount, diffuseFallOff);
                float3 diffuseLight = lightColor * rampedDiffuse;

                // specular
                float3 halfVector = normalize(lightDir + viewDir); // blinn-phong weirdness
                float specularFalloff = max(0, dot(halfVector, normal));
                specularFalloff = pow(specularFalloff, _Gloss);
                float3 rampedSpecular = Posterize(_SpecularCelCount, specularFalloff);
                float3 specularLight = rampedSpecular * lightColor;

                // rim lighting
                float rimFallOff = dot(normal, viewDir);
                rimFallOff = saturate(1 - rimFallOff);
                float4 rampedRim = step(_RimStep, rimFallOff);
                float4 rimLight = _RimColor * rampedRim * _RimStrength;

                // composite                
                float3 compositeLight = diffuseLight + _AmbientColor + rimLight;
                float3 finalSurfaceColor = compositeLight * col.xyz + specularLight;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return float4(finalSurfaceColor.rgb, alpha);
            }
            ENDCG
        }
    }
}

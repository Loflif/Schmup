Shader "Unlit/BlackHole"
{
    Properties
    {
        _FresnelStrength("Distortion edge multiplier", float) = 6
        _FresnelPow("Distortion raised to", float) = 6
        _HoleSize("Size of Event Horizon", float) = 4
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }

        GrabPass
        {
            "_GrabTexture"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 grabTexUV : TEXCOORD1;
            };

            struct VertexOutput
            {
                UNITY_FOG_COORDS(1)
                float4 clipSpacePos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 grabTexUV : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };

            VertexOutput vert (VertexInput i)
            {
                VertexOutput o;
                o.normal = UnityObjectToWorldNormal(i.normal);
                o.worldPos = mul(unity_ObjectToWorld, i.vertex);
                o.clipSpacePos = UnityObjectToClipPos(i.vertex);
                o.grabTexUV = ComputeGrabScreenPos(o.clipSpacePos);
                o.uv = i.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            sampler2D _GrabTexture;
            float _FresnelStrength;
            float _HoleSize;
            float _FresnelPow;

            float4 frag (VertexOutput o) : SV_Target
            {
                float3 normal = normalize(o.normal);
                float3 flippedNormals = normal * -1;
                // cam
                float3 camPos = _WorldSpaceCameraPos.xyz;
                float3 fragToCam = camPos - o.worldPos;

                // view
                float3 viewDir = normalize(fragToCam);

                // fresnel
                float fresnelFallOff = dot(normal, viewDir);

                // distortion
                float distortion = 1 - fresnelFallOff;
                distortion = mul(fresnelFallOff, _FresnelStrength);
                distortion = pow(distortion, _FresnelPow);
                o.grabTexUV.xyz += (flippedNormals * distortion);
                float4 distortedTransperancy = tex2Dproj(_GrabTexture, o.grabTexUV);
                
                // holeMask
                float holeMask = 1 - fresnelFallOff;
                holeMask *= _HoleSize;
                holeMask = round(holeMask);
                holeMask = clamp(holeMask, 0, 1)

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return distortedTransperancy * holeMask;
            }
            ENDCG
        }
    }
}

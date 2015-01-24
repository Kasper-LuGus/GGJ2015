// Shader created with Shader Forge v1.04 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.04;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:6896,x:32719,y:32712,varname:node_6896,prsc:2|custl-5455-OUT,olwid-4087-OUT,olcol-7700-R;n:type:ShaderForge.SFN_Color,id:9102,x:32076,y:32585,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_9102,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:4087,x:32343,y:33007,ptovrint:False,ptlb:OutlineWidth,ptin:_OutlineWidth,varname:node_4087,prsc:2,min:0,cur:0.05,max:1;n:type:ShaderForge.SFN_Color,id:7700,x:32409,y:33159,ptovrint:False,ptlb:OutlineColor,ptin:_OutlineColor,varname:node_7700,prsc:2,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_LightAttenuation,id:8732,x:31502,y:32611,varname:node_8732,prsc:2;n:type:ShaderForge.SFN_LightColor,id:8952,x:31565,y:32757,varname:node_8952,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9397,x:31810,y:32690,varname:node_9397,prsc:2|A-8732-OUT,B-8952-RGB;n:type:ShaderForge.SFN_Tex2d,id:9124,x:31531,y:32937,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_9124,prsc:2,tex:181ec605fb3b4314bbee61db18ab41c0,ntxv:0,isnm:False|UVIN-265-OUT;n:type:ShaderForge.SFN_Multiply,id:2356,x:32076,y:32788,varname:node_2356,prsc:2|A-9397-OUT,B-9748-OUT;n:type:ShaderForge.SFN_Multiply,id:5455,x:32312,y:32802,varname:node_5455,prsc:2|A-9102-RGB,B-2356-OUT;n:type:ShaderForge.SFN_Append,id:265,x:31292,y:32954,varname:node_265,prsc:2|A-8273-OUT,B-2128-OUT;n:type:ShaderForge.SFN_Dot,id:8273,x:31031,y:33009,varname:node_8273,prsc:2,dt:0|A-3835-OUT,B-6771-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2128,x:31031,y:33223,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_2128,prsc:2,glob:False,v1:0;n:type:ShaderForge.SFN_NormalVector,id:3835,x:30806,y:32848,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:6771,x:30782,y:33103,varname:node_6771,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9748,x:31885,y:32897,varname:node_9748,prsc:2|A-9124-RGB,B-802-RGB;n:type:ShaderForge.SFN_VertexColor,id:802,x:31700,y:33148,varname:node_802,prsc:2;proporder:9102-4087-7700-9124-2128;pass:END;sub:END;*/

Shader "Custom/ToonShaderColor" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _OutlineWidth ("OutlineWidth", Range(0, 1)) = 0.05
        _OutlineColor ("OutlineColor", Color) = (0,0,0,1)
        _Ramp ("Ramp", 2D) = "white" {}
        _Value ("Value", Float ) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float _OutlineWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_OutlineWidth,1));
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                return fixed4(float3(_OutlineColor.r,_OutlineColor.r,_OutlineColor.r),0);
            }
            ENDCG
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _MainColor;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform float _Value;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float2 node_265 = float2(dot(i.normalDir,lightDirection),_Value);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_265, _Ramp));
                float3 finalColor = (_MainColor.rgb*((attenuation*_LightColor0.rgb)*(_Ramp_var.rgb*i.vertexColor.rgb)));
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _MainColor;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform float _Value;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float2 node_265 = float2(dot(i.normalDir,lightDirection),_Value);
                float4 _Ramp_var = tex2D(_Ramp,TRANSFORM_TEX(node_265, _Ramp));
                float3 finalColor = (_MainColor.rgb*((attenuation*_LightColor0.rgb)*(_Ramp_var.rgb*i.vertexColor.rgb)));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

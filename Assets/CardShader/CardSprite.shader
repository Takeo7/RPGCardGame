// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1873,x:33548,y:31809,varname:node_1873,prsc:2|emission-1749-OUT,alpha-603-OUT;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32735,y:31910,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1086,x:33149,y:31910,cmnt:RGB,varname:node_1086,prsc:2|A-3262-OUT,B-5983-RGB,C-5376-RGB,D-554-OUT;n:type:ShaderForge.SFN_Color,id:5983,x:32937,y:32078,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:5376,x:32937,y:32223,varname:node_5376,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1749,x:33354,y:31910,cmnt:Premultiply Alpha,varname:node_1749,prsc:2|A-1086-OUT,B-603-OUT;n:type:ShaderForge.SFN_Multiply,id:603,x:33149,y:32078,cmnt:A,varname:node_603,prsc:2|A-4805-A,B-5983-A,C-5376-A,D-554-OUT;n:type:ShaderForge.SFN_Append,id:8421,x:31730,y:31751,varname:node_8421,prsc:2|A-4339-OUT,B-3167-OUT;n:type:ShaderForge.SFN_Time,id:2324,x:31730,y:31907,varname:node_2324,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4339,x:31556,y:31751,ptovrint:False,ptlb:Waves_U_speed,ptin:_Waves_U_speed,varname:node_4339,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.5;n:type:ShaderForge.SFN_ValueProperty,id:3167,x:31556,y:31907,ptovrint:False,ptlb:Waves_V_speed,ptin:_Waves_V_speed,varname:node_3167,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.5;n:type:ShaderForge.SFN_Multiply,id:8878,x:31918,y:31751,varname:node_8878,prsc:2|A-8421-OUT,B-2324-T;n:type:ShaderForge.SFN_TexCoord,id:8630,x:31918,y:31907,varname:node_8630,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:5334,x:32119,y:31751,varname:node_5334,prsc:2|A-8878-OUT,B-8630-UVOUT;n:type:ShaderForge.SFN_Multiply,id:987,x:32516,y:31751,varname:node_987,prsc:2|A-5146-RGB,B-7805-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7805,x:32310,y:31929,ptovrint:False,ptlb:Waves power,ptin:_Wavespower,varname:node_7805,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Append,id:7060,x:31730,y:32078,varname:node_7060,prsc:2|A-3658-OUT,B-6064-OUT;n:type:ShaderForge.SFN_Time,id:5817,x:31730,y:32235,varname:node_5817,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:3658,x:31556,y:32078,ptovrint:False,ptlb:Mask_U_speed,ptin:_Mask_U_speed,varname:_Waves_U_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6064,x:31556,y:32235,ptovrint:False,ptlb:Mask_V_speed,ptin:_Mask_V_speed,varname:_Waves_V_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:2509,x:31918,y:32078,varname:node_2509,prsc:2|A-7060-OUT,B-5817-T;n:type:ShaderForge.SFN_Add,id:5434,x:32119,y:32078,varname:node_5434,prsc:2|A-2509-OUT,B-8630-UVOUT;n:type:ShaderForge.SFN_Multiply,id:465,x:32516,y:32078,varname:node_465,prsc:2|A-263-RGB,B-5533-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5533,x:32310,y:32270,ptovrint:False,ptlb:Mask power,ptin:_Maskpower,varname:_WavesPower_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:1917,x:31730,y:32411,varname:node_1917,prsc:2|A-4666-OUT,B-5318-OUT;n:type:ShaderForge.SFN_Time,id:2786,x:31730,y:32568,varname:node_2786,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:4666,x:31556,y:32411,ptovrint:False,ptlb:CardMap_U_speed,ptin:_CardMap_U_speed,varname:_Mask_U_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:5318,x:31556,y:32568,ptovrint:False,ptlb:CardMap_V_speed,ptin:_CardMap_V_speed,varname:_Mask_V_speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:8329,x:31918,y:32411,varname:node_8329,prsc:2|A-1917-OUT,B-2786-T;n:type:ShaderForge.SFN_Add,id:136,x:32119,y:32411,varname:node_136,prsc:2|A-8329-OUT,B-8630-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7628,x:32516,y:32411,varname:node_7628,prsc:2|A-6553-RGB,B-5979-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5979,x:32310,y:32593,ptovrint:False,ptlb:CardMap power,ptin:_CardMappower,varname:_MaskPower_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:7496,x:32735,y:32078,varname:node_7496,prsc:2|A-987-OUT,B-465-OUT,C-703-OUT,D-7628-OUT;n:type:ShaderForge.SFN_ValueProperty,id:703,x:32516,y:32235,ptovrint:False,ptlb:Emissive power,ptin:_Emissivepower,varname:node_703,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:10;n:type:ShaderForge.SFN_Add,id:3262,x:32937,y:31910,varname:node_3262,prsc:2|A-4805-RGB,B-7496-OUT;n:type:ShaderForge.SFN_Tex2d,id:5146,x:32310,y:31751,ptovrint:False,ptlb:Waves,ptin:_Waves,varname:node_5146,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5334-OUT;n:type:ShaderForge.SFN_Tex2d,id:263,x:32310,y:32078,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_263,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5434-OUT;n:type:ShaderForge.SFN_Tex2d,id:6553,x:32310,y:32411,ptovrint:False,ptlb:CardMap,ptin:_CardMap,varname:node_6553,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-136-OUT;n:type:ShaderForge.SFN_ValueProperty,id:554,x:32937,y:32393,ptovrint:False,ptlb: Opacity power,ptin:_Opacitypower,varname:node_554,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:4805-5983-703-554-263-3658-6064-5533-6553-4666-5318-5979-5146-4339-3167-7805;pass:END;sub:END;*/

Shader "ErbGameArt/CardSprite" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Emissivepower ("Emissive power", Float ) = 10
        _Opacitypower (" Opacity power", Float ) = 1
        _Mask ("Mask", 2D) = "white" {}
        _Mask_U_speed ("Mask_U_speed", Float ) = 0
        _Mask_V_speed ("Mask_V_speed", Float ) = 0
        _Maskpower ("Mask power", Float ) = 1
        _CardMap ("CardMap", 2D) = "white" {}
        _CardMap_U_speed ("CardMap_U_speed", Float ) = 0.1
        _CardMap_V_speed ("CardMap_V_speed", Float ) = 0.2
        _CardMappower ("CardMap power", Float ) = 1
        _Waves ("Waves", 2D) = "white" {}
        _Waves_U_speed ("Waves_U_speed", Float ) = -0.5
        _Waves_V_speed ("Waves_V_speed", Float ) = -0.5
        _Wavespower ("Waves power", Float ) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _Waves_U_speed;
            uniform float _Waves_V_speed;
            uniform float _Wavespower;
            uniform float _Mask_U_speed;
            uniform float _Mask_V_speed;
            uniform float _Maskpower;
            uniform float _CardMap_U_speed;
            uniform float _CardMap_V_speed;
            uniform float _CardMappower;
            uniform float _Emissivepower;
            uniform sampler2D _Waves; uniform float4 _Waves_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _CardMap; uniform float4 _CardMap_ST;
            uniform float _Opacitypower;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_2324 = _Time + _TimeEditor;
                float2 node_5334 = ((float2(_Waves_U_speed,_Waves_V_speed)*node_2324.g)+i.uv0);
                float4 _Waves_var = tex2D(_Waves,TRANSFORM_TEX(node_5334, _Waves));
                float4 node_5817 = _Time + _TimeEditor;
                float2 node_5434 = ((float2(_Mask_U_speed,_Mask_V_speed)*node_5817.g)+i.uv0);
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(node_5434, _Mask));
                float4 node_2786 = _Time + _TimeEditor;
                float2 node_136 = ((float2(_CardMap_U_speed,_CardMap_V_speed)*node_2786.g)+i.uv0);
                float4 _CardMap_var = tex2D(_CardMap,TRANSFORM_TEX(node_136, _CardMap));
                float node_603 = (_MainTex_var.a*_Color.a*i.vertexColor.a*_Opacitypower); // A
                float3 emissive = (((_MainTex_var.rgb+((_Waves_var.rgb*_Wavespower)*(_Mask_var.rgb*_Maskpower)*_Emissivepower*(_CardMap_var.rgb*_CardMappower)))*_Color.rgb*i.vertexColor.rgb*_Opacitypower)*node_603);
                float3 finalColor = emissive;
                return fixed4(finalColor,node_603);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

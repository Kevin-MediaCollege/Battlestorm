Shader "Transparent/Clouds" {

Properties {  

    _MainTex1 ("Cloud Mask", 2D) = "white" {}

    _MainTex2 ("Cloud Tile A", 2D) = "white" {}

    _MainTex3 ("Cloud Tile B", 2D) = "white" {}

}

 

SubShader {

    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

    LOD 200

 

CGPROGRAM

#pragma surface surf Lambert alpha

 

sampler2D _MainTex1;

sampler2D _MainTex2;

sampler2D _MainTex3;

 

struct Input {

    float2 uv_MainTex1;

    float2 uv_MainTex2;

    float2 uv_MainTex3;

};

 

void surf (Input IN, inout SurfaceOutput o) {

    fixed4 c1 = tex2D(_MainTex1, IN.uv_MainTex1);

    fixed4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);

    fixed4 c3 = tex2D(_MainTex3, IN.uv_MainTex3);

    o.Albedo = c1.rgb;

    o.Alpha = c1.a*c2.a*c3.a;

}

ENDCG

}

Fallback "Transparent/VertexLit"

}
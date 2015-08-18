Shader "Custom/Visibility" {
	Properties 
 {
     _Color ("Main Colour", Color) = (1,1,1,1)
     _Color2 ("Obstructed Colour", Color) = (1,1,1,1)
     _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
 }
 
 Category 
 {
     SubShader 
     { 
         Tags { "Queue"="Overlay+1"
	     "RenderType"="Transparent"}
	      
	     Pass
	     {
	     
	         ZWrite Off
	         ZTest Greater
	         Lighting Off
	         Color [_Color2]
	         
	     }
	      
	     Pass
	     {
	     
	         Blend SrcAlpha OneMinusSrcAlpha
	         ZTest Less
	         ColorMask RGB
        Material {
            Diffuse [_Color]
            Ambient [_Color]
        }
        Lighting On
        SetTexture [_MainTex] {
            Combine texture * primary DOUBLE, texture * primary
        }
	     }
     }
 }
 
 FallBack "Specular", 1
}

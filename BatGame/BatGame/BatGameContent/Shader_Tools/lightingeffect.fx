//Greg
//lighting effect for player and light sources
sampler s0;
texture lightMask;
sampler lightSampler = sampler_state{Texture = lightMask; Filter = MIN_MAG_MIP_LINEAR; AddressU = Clamp; AddressV = Clamp;};		//Clamps the texture so it's acceptable

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	float4 lightColor = tex2D(lightSampler, coords);
	return color * (lightColor -.61f);	//brightens and darkens things based upon their actual color
}
technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
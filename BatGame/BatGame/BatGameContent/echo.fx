//The green coloring given to enemies hit by echolocation
//I have not finished this
sampler s0;
float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
    float4 color = tex2D(s0, coords);

if (!any(color)) 
	return color;

	color = float4(1, 0, 1, 0);

return color;

}
technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
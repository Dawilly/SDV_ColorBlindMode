#define NOFILTER 0
#define PROTANOPIA 1
#define DEUTERANOPIA 2
#define TRITANOPIA 3

// Input Parameter (Determine which color blind filter to use)
int colorBlindFilter;

// Texture Sample
sampler s0;

// Blindness Filter Data
const float3x3 RGBToMatrix = { 0.2814f, -0.0971f, -0.0930f, 
							   0.6938f,  0.1458f, -0.2529f, 
						  	   0.0638f, -0.0250f,  0.4665f
							 };

const float3x3 MatrixToRGB = { 1.1677f, 0.9014f, 0.7214f, 
							  -6.4315f, 2.5970f, 0.1257f, 
							  -0.5044f, 0.0159f, 2.0517f
							 };

// Blindness Vision Data
const float4 ProtanopiaR = float4( 0.20,  0.99, -0.19, 0.0);
const float4 ProtanopiaG = float4( 0.16,  0.79,  0.04, 0.0);
const float4 ProtanopiaB = float4( 0.01, -0.01,  1.00, 0.0);

const float4 DeuteranopiaR = float4( 0.43,  0.72, -0.15, 0.0);
const float4 DeuteranopiaG = float4( 0.34,  0.57,  0.09, 0.0);
const float4 DeuteranopiaB = float4(-0.02,  0.03,  1.00, 0.0);

const float4 TritanopiaR = float4( 0.97,  0.11, -0.08, 0.0);
const float4 TritanopiaG = float4( 0.02,  0.82,  0.16, 0.0);
const float4 TritanopiaB = float4(-0.06,  0.88,  0.18, 0.0);

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

////////////////////
//Shared Functions//
////////////////////
float4 ProDeuSharedFilter(float4 input) {
	float3 color = mul(RGBToMatrix, float3(input.r, input.g, input.b));
	color.x -= color.y * 1.5;
	float3 altColor = mul(MatrixToRGB, color);
	return float4(altColor.r, altColor.g, altColor.b, input.a);
}

/////////////////////////
// Protanopia Functions//
/////////////////////////
float4 ProtanopiaFilter(float4 input) {
	return ProDeuSharedFilter(input);
}

float4 ApplyProtanopia(float4 input) {
	return float4(dot(input, ProtanopiaR), dot(input, ProtanopiaG), dot(input, ProtanopiaB), input.a);
}

//////////////////////////
//Deuteranopia Functions//
//////////////////////////
float4 DeuteranopiaFilter(float4 input) {
	return ProDeuSharedFilter(input);
}

float4 ApplyDeuteranopia(float4 input) {
	return float4(dot(input, DeuteranopiaR), dot(input, DeuteranopiaG), dot(input, DeuteranopiaB), input.a);
}

////////////////////////
//Tritanopia Functions//
////////////////////////
float4 TritanopiaFilter(float4 input) {
	float3 color = mul(RGBToMatrix, float3(input.r, input.g, input.b));
	color.x -= ((3 * color.z) - color.y) * 0.25;
	float3 altColor = mul(MatrixToRGB, color);
	return float4(altColor.r, altColor.g, altColor.b, input.a);
}

float4 ApplyTritanopia(float4 input) {
	return float4(dot(input, TritanopiaR), dot(input, TritanopiaG), dot(input, TritanopiaB), input.a);
}

////////////////
//Pixel Shader//
////////////////
float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(s0, input.TextureCoordinates);
	float4 outputColor = color;

	if (colorBlindFilter == PROTANOPIA) {
		outputColor = ApplyProtanopia(ProtanopiaFilter(outputColor));
	} else if (colorBlindFilter == DEUTERANOPIA) {
		outputColor = ApplyDeuteranopia(DeuteranopiaFilter(outputColor));
	} else if (colorBlindFilter == TRITANOPIA) {
		outputColor = ApplyTritanopia(TritanopiaFilter(outputColor));
	}

    return outputColor;
}

///////////////////////////////////////////
// Technique Pass (Call for XNA/MonoGame)//
///////////////////////////////////////////
technique ColorBlindMode
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

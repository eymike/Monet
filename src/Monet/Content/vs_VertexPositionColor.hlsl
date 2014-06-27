#include "VertexTypes.fxi"

VertexSVPositionColor main(VertexPositionColor input)
{
	VertexSVPositionColor output;
	output.position = mul(mul(mul(float4(input.position, 1), World), View), Proj);
	output.color = input.color;
	return output;
}
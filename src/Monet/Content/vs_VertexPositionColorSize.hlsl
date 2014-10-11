#include "VertexTypes.fxi"

VertexSVPositionColorSize main(VertexPositionColorSize input)
{
	VertexSVPositionColorSize output;
	output.position = mul(mul(mul(float4(input.position, 1), World), View), Proj);
	output.color = input.color;
	output.size = input.size;
	return output;
}
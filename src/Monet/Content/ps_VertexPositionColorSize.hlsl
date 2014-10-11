#include "VertexTypes.fxi"

float4 main(VertexSVPositionColorSize input) : SV_TARGET0
{
	return input.color;
}
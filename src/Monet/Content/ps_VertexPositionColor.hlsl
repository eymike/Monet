#include "VertexTypes.fxi"

float4 main(VertexSVPositionColor input) : SV_TARGET0
{
	return input.color;
}
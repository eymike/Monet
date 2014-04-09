#include "VertexTypes.fxi"

TransformedVertexPosition main(VertexPosition input)
{
    TransformedVertexPosition output;
    output.position = float4(input.position, 1);
    return output;
}


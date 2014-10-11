#include "VertexTypes.fxi"

[maxvertexcount(6)]
void main(line VertexSVPositionColorSize input[2], inout TriangleStream<VertexSVPositionColor> outStream)
{
    float3 p0 = input[0].position.xyz;
    float3 p1 = input[1].position.xyz;

    float s1 = input[0].size;
    float s2 = input[1].size;

    float w0 = input[0].position.w;
    float w1 = input[1].position.w;

    p0 /= w0;
    p1 /= w1;

    float3 dir = normalize(p1 - p0);

    float3 ratio = float3(1.0f / ViewPortDimensions.x, 1.0f / ViewPortDimensions.y, 0);
    float3 normal = normalize(cross(float3(0, 0, -1), dir) * ratio);

    p0 -= (dir * s1 * ratio);
    p1 += (dir * s1 * ratio);

    float3 o1 = normal * s1 * ratio;
    float3 o2 = normal * s2 * ratio;

	VertexSVPositionColor output[4];

    output[0].position = float4(p0 + o1, 1) * w0;
    output[0].color = input[0].color;

    output[1].position = float4(p0 - o1, 1) * w0;
    output[1].color = input[0].color;
    
    output[2].position = float4(p1 + o2, 1) * w1;
    output[2].color = input[1].color;
    
    output[3].position = float4(p1 - o2, 1) * w1;
    output[3].color = input[1].color;

    outStream.Append(output[0]);
    outStream.Append(output[1]);
    outStream.Append(output[2]);

    outStream.RestartStrip();

    outStream.Append(output[3]);
    outStream.Append(output[2]);
    outStream.Append(output[1]);
}
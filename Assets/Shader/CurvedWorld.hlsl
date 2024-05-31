static const float WORLD_CURV = 0.009;

void Curved_float(in float3 WorldPos,out float3 CurvedWorldPos)
{
    CurvedWorldPos = WorldPos;

    float offset = 10;
    float z = WorldPos.z - offset;
    CurvedWorldPos.y -= z * z * WORLD_CURV;
}
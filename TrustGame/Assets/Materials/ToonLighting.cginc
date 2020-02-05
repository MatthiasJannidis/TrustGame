#ifndef CUSTOM_TOON_CGINC_DEFINED
#define CUSTOM_TOON_CGINC_DEFINED


//code from : https://www.ronja-tutorials.com/2018/10/27/improved-toon.html
half4 LightingToon(SurfaceOutput s, half3 lightDir, half3 viewDir, float shadowAttenuation)
{
	//diffuse
	float lambert = dot(s.Normal, lightDir) / 1;

	float lightIntensity = floor(lambert);
	const float lambertchange = fwidth(lambert);
	const float smoothing = smoothstep(.0, lambertchange, frac(lambert));
	lightIntensity += smoothing;
	lightIntensity *= .2f;
	//lightIntensity = saturate(lightIntensity);

	//shadow
	const float attenuationChange = fwidth(shadowAttenuation) * 0.5;
	const float shadow = smoothstep(0.5 - attenuationChange, 0.5 + attenuationChange, shadowAttenuation);

	half4 color;
	//+ s.Albedo*_LightColor0.rgb*specular
	color.rgb = (s.Albedo * _LightColor0.rgb * lightIntensity) * shadow;
	color.a = s.Alpha;
	return color;
}
#endif
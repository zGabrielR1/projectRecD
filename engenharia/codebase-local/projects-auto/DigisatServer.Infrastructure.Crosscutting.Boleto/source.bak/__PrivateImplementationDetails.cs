// Synthesized during reconstruction: Roslyn's generated string hash helper.
internal static class __PrivateImplementationDetails
{
	internal static uint ComputeStringHash(string s)
	{
		uint num = 0u;
		if (s != null)
		{
			num = 2166136261u;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619u;
			}
		}
		return num;
	}
}

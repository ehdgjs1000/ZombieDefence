// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("8/5xcj+UrVy0ifRd5Dy0S8pA9eN5Z/OkPZw+r91YvTKyaNLGVrVR2UPvgwpeXcKh0ikOofv1AUwd3odBGxk0UHEn3t1X0ttBJ50+TqDAnkDhTinF4upOlRj5UbrcIR2lccacCmWFxaUuCenGy50ZqSjKElIe9ifkvStBcGrJiBJ8ESfs3CxqkcPmuT3oa2VqWuhrYGjoa2tqr1/x5h68JgJNKgM9LRb8e+7uxxSNMT1WlzFOn0BRLh3LNdxPKXMzZBgPpksiBxNa6GtIWmdsY0DsIuydZ2tra29qaYARHlrTi5HCNB7crgQosw4zpJFdh5rFD2WlcgQEKudoVTatEfnREsXXSpkJ12jYSGT1ex306w0L2CBPD3jJla22FLtcE2hpa2pr");
        private static int[] order = new int[] { 9,2,8,5,13,11,11,8,10,13,13,11,12,13,14 };
        private static int key = 106;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}

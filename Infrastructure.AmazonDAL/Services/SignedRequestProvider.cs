using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Infrastructure.AmazonDAL.Services
{
    class SignedRequestProvider
    {
        private readonly string _endPoint;
        private readonly string _akid;
        private readonly byte[] _secret;
        private readonly HMAC _signer;

        private const string RequestUri = "/onca/xml";
        private const string RequestMethod = "GET";

        public SignedRequestProvider(string awsAccessKeyId, string awsSecretKey, string destination)
        {
            _endPoint = destination.ToLower();
            _akid = awsAccessKeyId;
            _secret = Encoding.UTF8.GetBytes(awsSecretKey);
            _signer = new HMACSHA256(_secret);
        }

        /// <summary>
        /// Sign a request in the form of a Dictionary of name-value pairs.
        /// 
        /// This method returns a complete URL to use. Modifying the returned URL
        /// in any way invalidates the signature and Amazon will reject the requests.
        /// </summary>
        public string Sign(IDictionary<string, string> request)
        {
            // Use a SortedDictionary to get the parameters in naturual byte order, as
            // required by AWS.
            ParamComparer pc = new ParamComparer();
            SortedDictionary<string, string> sortedMap = new SortedDictionary<string, string>(request, pc);

            // Add the AWSAccessKeyId and Timestamp to the requests.
            sortedMap["AWSAccessKeyId"] = _akid;
            sortedMap["Timestamp"] = GetTimestamp();

            // Get the canonical query string
            string canonicalQS = ConstructCanonicalQueryString(sortedMap);

            // Derive the bytes needs to be signed.
            StringBuilder builder = new StringBuilder();
            builder.Append(RequestMethod)
                .Append("\n")
                .Append(_endPoint)
                .Append("\n")
                .Append(RequestUri)
                .Append("\n")
                .Append(canonicalQS);

            string stringToSign = builder.ToString();
            byte[] toSign = Encoding.UTF8.GetBytes(stringToSign);

            // Compute the signature and convert to Base64.
            byte[] sigBytes = _signer.ComputeHash(toSign);
            string signature = Convert.ToBase64String(sigBytes);

            // now construct the complete URL and return to caller.
            StringBuilder qsBuilder = new StringBuilder();
            qsBuilder.Append("http://")
                .Append(_endPoint)
                .Append(RequestUri)
                .Append("?")
                .Append(canonicalQS)
                .Append("&Signature=")
                .Append(PercentEncodeRfc3986(signature));

            return qsBuilder.ToString();
        }

        /// <summary>
        /// Current time in IS0 8601 format as required by Amazon
        /// </summary>
        private string GetTimestamp()
        {
            DateTime currentTime = DateTime.UtcNow;
            string timestamp = currentTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            return timestamp;
        }

        /// <summary>
        /// Percent-encode (URL Encode) according to RFC 3986 as required by Amazon.
        /// 
        /// This is necessary because .NET's HttpUtility.UrlEncode does not encode
        /// according to the above standard. Also, .NET returns lower-case encoding
        /// by default and Amazon requires upper-case encoding.
        /// </summary>
        private string PercentEncodeRfc3986(string str)
        {
            str = HttpUtility.UrlEncode(str, System.Text.Encoding.UTF8);
            str = str.Replace("'", "%27").Replace("(", "%28").Replace(")", "%29").Replace("*", "%2A").Replace("!", "%21").Replace("%7e", "~").Replace("+", "%20");

            StringBuilder sbuilder = new StringBuilder(str);
            for (int i = 0; i < sbuilder.Length; i++)
            {
                if (sbuilder[i] == '%')
                {
                    if (Char.IsLetter(sbuilder[i + 1]) || Char.IsLetter(sbuilder[i + 2]))
                    {
                        sbuilder[i + 1] = Char.ToUpper(sbuilder[i + 1]);
                        sbuilder[i + 2] = Char.ToUpper(sbuilder[i + 2]);
                    }
                }
            }
            return sbuilder.ToString();
        }

        /// <summary>
        /// Consttuct the canonical query string from the sorted parameter map.
        /// </summary>
        private string ConstructCanonicalQueryString(SortedDictionary<string, string> sortedParamMap)
        {
            StringBuilder builder = new StringBuilder();

            if (sortedParamMap.Count == 0)
            {
                builder.Append("");
                return builder.ToString();
            }

            foreach (KeyValuePair<string, string> kvp in sortedParamMap)
            {
                builder.Append(PercentEncodeRfc3986(kvp.Key));
                builder.Append("=");
                builder.Append(PercentEncodeRfc3986(kvp.Value));
                builder.Append("&");
            }
            string canonicalString = builder.ToString();
            canonicalString = canonicalString.Substring(0, canonicalString.Length - 1);
            return canonicalString;
        }
    }

    /// <summary>
    /// To help the SortedDictionary order the name-value pairs in the correct way.
    /// </summary>
    class ParamComparer : IComparer<string>
    {
        public int Compare(string p1, string p2)
        {
            return string.CompareOrdinal(p1, p2);
        }
    }
}


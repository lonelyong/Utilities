using System.Security.Cryptography;

namespace NgNet.Security
{
    #region <class - AesEncrypt>
    public class AesHElper
    {
        private Aes aes;

        public AesHElper()
        {
            aes = Aes.Create();

        }
    }
    #endregion
}

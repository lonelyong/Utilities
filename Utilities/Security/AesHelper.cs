using System.Security.Cryptography;

namespace Utilities.Security
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

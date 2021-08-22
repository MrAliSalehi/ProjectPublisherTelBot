using System.ComponentModel.DataAnnotations;

namespace NewBot.Security
{
    public class EncryptionModel
    {
        public byte[] DataToDecrypt { get; set; }
        public string DataToEnCrypt { get; set; }
        [Required]
        public Type type { get; set; }
    }

    
    public enum Type
    {
        Encrypt,
        Decrypt
    }
}
using System.ComponentModel.DataAnnotations;

namespace GameNet.Models.Platform
{
    public class Player
    {
        // ATTRIBUTES

        [Key]
        public string ID { get; set; } = Guid.NewGuid().ToString("N");

        public string? Pseudo { get; set; }

        // COMMANDS

        public void SetPseudo(string Pseudo)
        {
            this.Pseudo = "@" + Pseudo;
        }

        // REQUESTS

    }
        
}

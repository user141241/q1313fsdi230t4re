using GameNet.Models.Games;
using GameNet.Models.Games.TicTacToe;
using Mono.TextTemplating;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNet.Models.Platform
{
    public class Lobby
    {
        // ATTRIBUTES
        [Key]
        public string ID { get; set; } = Guid.NewGuid().ToString("N");
        public string Name { get; set; }
        public DateTime Create { get; set; } = DateTime.Now;
        public string Link { get; private set; }
        public string? Pass { get; private set; }
        public bool IsPrivate { get; set; } = false;
        [Required]
        public string SerializedPlayers { get; set; }
        [NotMapped]
        public Dictionary<Player, bool> Players { get; set; } = new Dictionary<Player, bool>();
        public BoardGame SelectedBoardGame { get; set; }
        public State State { get; set; } = State.WAITED;

        [NotMapped]
        public required Player Creator { get; set; } 

        // CONSTRUCTORS 

        public Lobby()
        {
            if (Creator == null)
            {
                throw new Exception();
            }
            Name = "Default"; 
            Players.Add(Creator, true);
            SerializedPlayers = JsonConvert.SerializeObject(Players); 
            Link = $"http://localhost:7021/Lobbies/Launch/{ID}";
        }

        // COMMANDS

        public int GetCountPlayers()
        {
            return GetPlayersWithAccessInLobby().Count;
        }


        public ManageGame StartGame()
        {
            ManageGame? Game = null;
            switch(SelectedBoardGame)
            {
                case BoardGame.TicTacToe:
                    Game = new TicTacToeGame();
                    break;
                case BoardGame.Mastermind:
                    throw new NotSupportedException();
                case BoardGame.Power4:
                    throw new NotSupportedException();
                case BoardGame.Labyrinthe:
                    throw new NotSupportedException();
                case BoardGame.Battleship:
                    throw new NotSupportedException();
                default:
                    throw new NotSupportedException();
            }
            State = State.LAUNCHED;
            return Game;
        }

        public void CheckPass(Player player, string PassPlayer)
        {
            if (Players.ContainsKey(player))
            {
                if (!Players[player] && PassPlayer.Equals(Pass))
                {
                    Players[player] = true;
                }
            }
        }
        
        public void AddPlayer(Player player)
        {
            if (this.GetPlayersWithAccessInLobby().Count == 2)
            {
                return;
            }
            Players.Add(player, IsPrivate ? false : true);

            if (this.GetPlayersWithAccessInLobby().Count == 2)
            {
                State = State.READY;
            }
            SerializePlayers();
        }

        public void RemovePlayer(Player player)
        {
            if (this.GetPlayersWithAccessInLobby().Count == 0)
            {
                return;
            }
            Players.Remove(player);

            if (this.GetPlayersWithAccessInLobby().Count < 2)
            {
                State = State.WAITED;
            }
            SerializePlayers();
        }

        public void Initialize(bool privateLobby)
        {
            IsPrivate = privateLobby;
            Pass = privateLobby ? GetAleaPass(6) : null;
            SerializePlayers();
        }

        // REQUESTS
        public List<Player> GetPlayersWithAccessInLobby()
{
            List<Player> playersWithAccess = [];
            foreach (var p in Players)
            {
                if (p.Value)
                {
                    playersWithAccess.Add(p.Key);
                }
            }
            return playersWithAccess;
        }

        // UTILS
        private string GetAleaPass(int length)
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var charsArr = new char[length];
            var random = new Random();

            for (int i = 0; i < charsArr.Length; i++)
            {
                charsArr[i] = characters[random.Next(characters.Length)];
            }

            return new string(charsArr);
        }

        public void SerializePlayers()
        {
            SerializedPlayers = JsonConvert.SerializeObject(Players);
        }

        public void DeserializePlayers()
        {
            Players = JsonConvert.DeserializeObject<Dictionary<Player, bool>>(SerializedPlayers) ?? new Dictionary<Player, bool>();
        }
    }
}

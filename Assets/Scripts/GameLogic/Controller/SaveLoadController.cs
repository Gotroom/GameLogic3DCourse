using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ermolaev_3D
{
    public sealed class SaveLoadController : BaseController, IInitializable
    {
        private readonly ISavable<SerializableGameObject> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.json";
        private readonly string _path;

        public SaveLoadController() : base()
        {
            _data = new JsonData<SerializableGameObject>();
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Initialization()
        {
            
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            var player = Object.FindObjectOfType<CharacterController>();
            var bots = Object.FindObjectsOfType<Bot>();
            List<SerializableGameObject> savableObjects = new List<SerializableGameObject>();
            savableObjects.Add(new SerializableGameObject
            {
                Pos = player.transform.position,
                Name = player.name,
                IsEnable = player.enabled
            });
            foreach (var bot in bots)
            {
                var savable = new SerializableGameObject
                {
                    Pos = bot.Transform.position,
                    Name = bot.Name,
                    IsEnable = bot.enabled
                };
                savableObjects.Add(savable);
            }
            _data.Save(savableObjects, Path.Combine(_path, _fileName));
        }

        public void Load()
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return;
            var loadedData = _data.LoadList(file);
            var playerData = loadedData.Find(e => e.Name == "Player");
            var botsData = loadedData.FindAll(e => e.Name.Contains("Bot"));

            var player = Object.FindObjectOfType<CharacterController>();
            var pos = new Vector3(playerData.Pos.X, playerData.Pos.Y, playerData.Pos.Z);
            player.transform.position = pos;

            ServiceLocator.Resolve<BotController>().LoadBots(botsData);
        }

        
    }
}
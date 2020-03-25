using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ermolaev_3D
{
    public sealed class BotController : BaseController, IExecutable, IInitializable
    {
        private readonly int _countBot = 4;
        private readonly HashSet<Bot> _getBotList = new HashSet<Bot>();

        public void Initialization()
        {
            for (var index = 0; index < _countBot; index++)
            {
                var tempBot = UnityEngine.Object.Instantiate(Resources.Load<Bot>("Bot"),
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
                //todo разных противников
                AddBotToList(tempBot);
            }
        }

        private void AddBotToList(Bot bot)
        {
            if (!_getBotList.Contains(bot))
            {
                _getBotList.Add(bot);
                bot.Killed += RemoveBotToList;
                ServiceLocatorMonoBehaviour.GetService<ScoreUI>().AddEnemyReference(bot);
            }
        }

        private void RemoveBotToList(BaseEnemyModel bot)
        {
            if (!_getBotList.Contains(bot))
            {
                return;
            }

            bot.Killed -= RemoveBotToList;
            _getBotList.Remove(bot as Bot);
        }

        private void ClearBotList()
        {
            foreach (var bot in _getBotList)
            {
                if (!_getBotList.Contains(bot))
                {
                    continue;
                }
                bot.Killed -= RemoveBotToList;
                bot.ManualDestroy();
            }
            _getBotList.Clear();
        }

        public void LoadBots(List<SerializableGameObject> botsSaveData)
        {
            ClearBotList();
            for (var index = 0; index < botsSaveData.Count; index++)
            {
                var botData = botsSaveData[index];
                var botPosition = new Vector3(botData.Pos.X, botData.Pos.Y, botData.Pos.Y);
                var tempBot = Object.Instantiate(Resources.Load<Bot>("Bot"),
                    botPosition,
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
                AddBotToList(tempBot);
            }
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            for (var i = 0; i < _getBotList.Count; i++)
            {
                var bot = _getBotList.ElementAt(i);
                bot.Tick();
            }
        }
    }
}

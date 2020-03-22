using System.IO;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Ermolaev_3D
{
    public sealed class XMLData : ISavable<SerializableGameObject[]>
    {
        public void Save(SerializableGameObject[] objects, string path = "")
        {
            var xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement($"Objects");
            xmlDoc.AppendChild(rootNode);
            foreach (var obj in objects)
            {
                XmlNode objectNode = xmlDoc.CreateElement($"Object");
                rootNode.AppendChild(objectNode);

                var element = xmlDoc.CreateElement("Name");
                element.SetAttribute("value", obj.Name);
                objectNode.AppendChild(element);

                element = xmlDoc.CreateElement("PosX");
                element.SetAttribute("value", obj.Pos.X.ToString());
                objectNode.AppendChild(element);

                element = xmlDoc.CreateElement("PosY");
                element.SetAttribute("value", obj.Pos.Y.ToString());
                objectNode.AppendChild(element);

                element = xmlDoc.CreateElement("PosZ");
                element.SetAttribute("value", obj.Pos.Z.ToString());
                objectNode.AppendChild(element);

                element = xmlDoc.CreateElement("IsEnable");
                element.SetAttribute("value", obj.IsEnable.ToString());
                objectNode.AppendChild(element);

                
            }
            XmlNode userNode = xmlDoc.CreateElement("Info");
            var attribute = xmlDoc.CreateAttribute("Unity");
            attribute.Value = Application.unityVersion;
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "System Language: " +
                                 Application.systemLanguage;
            rootNode.AppendChild(userNode);

            xmlDoc.Save(path);
        }

        public SerializableGameObject[] Load(string path = "")
        {
            var result = new List<SerializableGameObject>();
            if (!File.Exists(path)) return result.ToArray();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            foreach (XmlNode n in xml.SelectNodes("/Objects/Object"))
            {
                SerializableGameObject obj = new SerializableGameObject();
                obj.Name =  n.SelectSingleNode("Name").Attributes["value"].Value;
                obj.Pos.X =  n.SelectSingleNode("PosX").Attributes["value"].Value.TrySingle();
                obj.Pos.Y =  n.SelectSingleNode("PosY").Attributes["value"].Value.TrySingle();
                obj.Pos.Z =  n.SelectSingleNode("PosZ").Attributes["value"].Value.TrySingle();
                obj.IsEnable = n.SelectSingleNode("IsEnable").Attributes["value"].Value.TryBool();
                result.Add(obj);
            }
            return result.ToArray();
        }

        public void Save(List<SerializableGameObject[]> data, string path = null)
        {
            throw new System.NotImplementedException();
        }

        public List<SerializableGameObject[]> LoadList(string path = null)
        {
            throw new System.NotImplementedException();
        }
    }
}


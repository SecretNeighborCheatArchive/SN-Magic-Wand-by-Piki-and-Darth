using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SecretNeighbour.Configs
{
    [Serializable]
    public class CheatConfig
    {
        public static CheatConfig current = new CheatConfig();

        public NoclipMode noclipMode = NoclipMode.Flat;
        public float noclipSpeed = 20f;
        public bool backfireOtherCheaters = false;
        public bool silentAim = false;
        public bool aimbot = false;
        public float aimDist = 300f;
        public bool drawTracers = false;
        public float fov = 150f;
        public bool chams = false;
        public bool godmodeESP = false;
        public bool forceHost = false;
        public bool basementESP = false;
        public bool keyChams = false;
        public bool keyESP = false;
        public bool playerNameESP = false;
        public bool noSkillCooldowns = false;
        public bool aimXhair = false;

        public bool antiKill = false;
        public bool antiTP = false;
        public bool antiBuff = false;

        public void Save()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), configPath);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (FileStream fs = File.Create(path))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(fs, this);
                fs.Close();
            }
        }

        public static CheatConfig Load()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), configPath);
            if (!File.Exists(path))
            {
                return new CheatConfig();
            }
            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    var formatter = new BinaryFormatter();
                    var result = (CheatConfig)formatter.Deserialize(fs);
                    fs.Close();
                    return result;
                }
            }
            catch (SerializationException ex)
            {
                File.Delete(path);
            }
            catch { }
            return new CheatConfig();
        }

        public const string configPath = @"Magic Wand\Settings.cfg";
    }
    // No wait
    public enum NoclipMode
    {
        Flat,
        CameraFollow
    }
}

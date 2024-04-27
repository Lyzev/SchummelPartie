using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MelonLoader;
using Newtonsoft.Json;

namespace SchummelPartie.setting
{
    public static class SettingManager
    {
        public static List<ISetting> Settings { get; } = new();

        private static readonly string settingsFilePath = "settings.json";

        public static void SaveSettings()
        {
            var settingsToSave = Settings.Select(s => new
            {
                s.Container,
                s.Name,
                Value = s.GetValue()
            });

            var json = JsonConvert.SerializeObject(settingsToSave, Formatting.Indented);
            File.WriteAllText(settingsFilePath, json);
        }

        public static void LoadSettings()
        {
            if (!File.Exists(settingsFilePath))
                return;

            var json = File.ReadAllText(settingsFilePath);
            var settings =
                JsonConvert.DeserializeAnonymousType(json, new[] { new { Container = "", Name = "", Value = "" } });
            foreach (var setting in settings)
            {
                var s = Get(setting.Container, setting.Name);
                if (s != null)
                {
                    s.SetValue(Convert.ChangeType(setting.Value, s.GetValue().GetType()));
                    MelonLogger.Msg($"[Setting] {setting.Container}.{setting.Name} = {s.GetValue()}");
                } else MelonLogger.Warning($"[Setting] {setting.Container}.{setting.Name} not found");
            }
        }

        // Get a list of settings belonging to the specified settings container class.
        public static List<ISetting> Get(string container)
        {
            return Settings.Where(setting => setting.Container == container).ToList();
        }

        // Get a setting based on the provided container class name, setting class name, and setting name.
        public static ISetting Get(string container, string name)
        {
            return Settings.FirstOrDefault(setting =>
                setting.Container == container && setting.Name == name);
        }
    }
}
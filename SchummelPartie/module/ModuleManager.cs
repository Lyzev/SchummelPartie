using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MelonLoader;
using SchummelPartie.module.modules;
using UrGUI.UWindow;

namespace SchummelPartie.module;

public static class ModuleManager
{
    public static List<Module> Modules = new();

    public static void Init()
    {
        Modules.Add(new ModuleDebug());
        Modules.Add(new ModuleForcePresent());
        Modules.Add(new ModuleAnimalArithmetic());
        Modules.Add(new ModuleMysteryMaze());
        Modules.Add(new ModuleMortarMayhem());
        Modules.Add(new ModuleBattyBatter());
        Modules.Add(new ModuleSpookySpikes());
        Modules.Add(new ModuleRythm());
        Modules.Add(new ModulePackAndPile());
        Modules.Add(new ModuleTreasureHunt());
        Modules.Add(new ModuleSwiftShooters());
        Modules.Add(new ModuleBomber());
        Modules.Add(new ModuleFinder());
        Modules.Add(new ModuleBarnBrawl());
        Modules.Add(new ModulePresents());
        Modules.Add(new ModuleSelfishStride());
        Modules.Add(new ModuleMemoryMenu());
        Modules.Add(new ModuleTanks());
        Modules.Add(new ModuleGUI());
        Modules.Add(new ModuleSidestepSlope());
        Modules.Add(new ModuleSpeedySpotlights());
        // Code to generate markdown for the modules
        // StringBuilder markdown = new StringBuilder();
        // foreach (var module in Modules)
        // {
        //     markdown.AppendLine("<details>");
        //     markdown.AppendLine($"<summary>{module.Name.Replace("Module", "")}</summary>");
        //     markdown.AppendLine();
        //     markdown.AppendLine($"{module.Description}");
        //     markdown.AppendLine("</details>");
        //     markdown.AppendLine();
        // }
        // MelonLogger.Msg(markdown.ToString());
    }

    public static void OnUpdate()
    {
        foreach (var module in Modules) module.OnUpdate();
    }

    public static void OnGUI()
    {
        foreach (var module in Modules) module.OnGUI();
    }

    public static void OnSettings(UWindow window)
    {
        foreach (var module in Modules)
        {
            Type moduleType = module.GetType();
            while (moduleType != null)
            {
                if (moduleType.IsGenericType && moduleType.GetGenericTypeDefinition() == typeof(ModuleMinigame<>))
                    break;
                moduleType = moduleType.BaseType;
            }
            if (moduleType != null)
                continue;
            module.OnSettings(window);
        }
    }

    public static List<T> GetModules<T>() where T : Module
    {
        var modules = new List<T>();
        foreach (var module in Modules)
            if (module is T)
                modules.Add((T)module);
        return modules;
    }

    public static T GetModule<T>() where T : Module
    {
        return Modules.First(m => m is T) as T;
    }

    public static bool IsMe(this CharacterBase player)
    {
        return player.IsOwner && !player.GamePlayer.IsAI;
    }
}
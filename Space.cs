using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Reflection;

namespace ds.test.impl
{
    /// <summary>
    /// Плагин
    /// </summary>
    public interface IPlugin
    {
        string PluginName { get; }
        string Version { get; }
        System.Drawing.Image Image { get; }
        string Description { get; }
        int Run(int input1, int input2);
    }


    /// <summary>
    /// Часть реализации интерфейса IPlugin унаследована от закрытого абстрактного класса
    /// </summary>
    abstract class AbstractPlugin : IPlugin
    {
        public abstract string PluginName { get; }
        public abstract string Version { get; }
        public abstract Image Image { get; }
        public abstract string Description { get; }

        /// <summary>
        /// Базовая реализация метода Run для выполнения сложение чисел
        /// </summary>
        public int Run(int input1, int input2) => input1 + input2;
    }


    /// <summary>
    /// Класс для работы с плагином
    /// </summary>
    class Plugin : AbstractPlugin
    {
        public Plugin(string pluginName, string version, Image image, string description)
        {
            PluginName = pluginName;
            Version = version;
            Image = image;
            Description = description;
        }

        public override string PluginName { get; }
        public override string Version { get; }
        public override Image Image { get; }
        public override string Description { get; }
    }


    /// <summary>
    /// Интерфейс для манипуляции с плагинами
    /// </summary>
    interface PluginFactory
    {
        int PluginsCount { get; }
        string[] GetPluginNames { get; }
        IPlugin GetPlugin(string pluginName);
    }


    /// <summary>
    /// Реализация интерфейса для манипуляции с плагинами
    /// </summary>
    class ReleasePluginFactory : PluginFactory
    {
        private List<IPlugin> plugins;

        public ReleasePluginFactory()
        {
            plugins = new List<IPlugin>();
            plugins.Add(new Plugin("SmartPlay", "1.1.1", null, "Авто установщик игр"));
            plugins.Add(new Plugin("DownloadVideo", "4.5.1", null, "Возможность скачать любое видео на понравившемся сайте"));
            plugins.Add(new Plugin("SiteInfo", "0.0.1", null, "Позволяет узнать на чем написан сайт"));
            plugins.Add(new Plugin("SmartTime", "1.1.1", null, "Умные часы позволет синронизировать время по местоположению"));
        }

        public int PluginsCount { get { return plugins.Count; } }

        public string[] GetPluginNames { get { return plugins.Select(c => c.PluginName).ToArray(); } }

        public IPlugin GetPlugin(string pluginName)
        {
            if (!string.IsNullOrEmpty(pluginName) && !string.IsNullOrWhiteSpace(pluginName))
            {
                IPlugin plugin = plugins.FirstOrDefault(c => c?.PluginName == pluginName);
                if (plugin != null)
                {
                    return plugin;
                }
            }
            return null;
        }
    }


    /// <summary>
    /// Класс для работы с плагинами
    /// </summary>
    public static class Plugins
    {
        /// <summary>
        /// Экземпляр класса для манипуляции с плагинами
        /// </summary>
        private static PluginFactory pluginFactory = new ReleasePluginFactory();

        /// <summary>
        /// Операция деления 2-ух чисел
        /// </summary>
        public static int RunDivision(this IPlugin pl, int input1, int input2) => input1 / input2;

        /// <summary>
        /// Операция умножения 2-ух чисел
        /// </summary>
        public static int RunMultiplication(this IPlugin pl, int input1, int input2) => input1 * input2;

        /// <summary>
        /// Операция вычитания 2-ух чисел
        /// </summary>
        public static int RunSubtraction(this IPlugin pl, int input1, int input2) => input1 - input2;

        /// <summary>
        /// Операция сложения 2-ух чисел
        /// </summary>
        public static int RunSum(this IPlugin pl, int input1, int input2) => input1 + input2;

        /// <summary>
        /// Получить название всех плагинов
        /// </summary>
        public static string[] GetPluginNames => pluginFactory.GetPluginNames;

        /// <summary>
        /// Получить плагин
        /// </summary>
        /// <param name="name">Название плагина</param>
        public static IPlugin GetPlugin(string name) => pluginFactory.GetPlugin(name);

        /// <summary>
        /// Получить количество плагинов
        /// </summary>
        public static int PluginsCount => pluginFactory.PluginsCount;
    }
}

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Clio.Utilities;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Nicked from zzi's LeveGen.
    /// </summary>
    internal class WpfHelper
    {
        #region Singleton

        private static WpfHelper _wpfHelper;
        internal static WpfHelper Instance => _wpfHelper ?? (_wpfHelper = new WpfHelper());

        #endregion

        /// <summary>
        /// load our Resource file that contains styles and magic.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="control"></param>
        // ReSharper disable once UnusedMember.Local
        private void LoadResourceForWindow(string filename, UserControl control)
        {
            try
            {
                var resource = LoadAndTransformXamlFile<ResourceDictionary>(filename);
                foreach (DictionaryEntry res in resource)
                {
                    if (!control.Resources.Contains(res.Key))
                        control.Resources.Add(res.Key, res.Value);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Log("Error Loading Resource {0}", ex);
            }
        }

        /// <summary>
        /// loads up the window content for an xml file.
        /// </summary>
        /// <param name="xamlFilePath"></param>
        /// <returns></returns>
        internal UserControl LoadWindowContent([NotNull]string xamlFilePath)
        {

            try
            {
                var windowContent = LoadAndTransformXamlFile<UserControl>(xamlFilePath);
                //if (File.Exists(Path.Combine(Path.GetDirectoryName(xamlFilePath), "Dictionary.xaml")))
                //    LoadResourceForWindow(Path.Combine(Path.GetDirectoryName(xamlFilePath), "Dictionary.xaml"), windowContent);
                return windowContent;
            }
            catch (Exception arg)
            {
                LogHelper.Instance.Log("Exception loading window content! {0}", arg);
            }
            return null;
        }
        /// <summary>
        /// loads our xaml files into a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xamlText"></param>
        /// <returns></returns>
        private T LoadAndTransformXamlFile<T>(string xamlText)
        {
            T result;
            try
            {
                //var xamlText = File.ReadAllText(filePath);
                xamlText = Regex.Replace(xamlText, "xmlns:(.+?)=\"clr-namespace:([\\w\\d\\._]+?)\"",
                    "xmlns:$1=\"clr-namespace:$2;assembly=" + Assembly.GetCallingAssembly().GetName().Name + "\"",
                    RegexOptions.Compiled | RegexOptions.Singleline);
                xamlText = Regex.Replace(xamlText,
                    "<ResourceDictionary.MergedDictionaries>.*</ResourceDictionary.MergedDictionaries>", string.Empty,
                    RegexOptions.Compiled | RegexOptions.Singleline);
                result = (T)XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(xamlText)));
            }
            catch (Exception exception)
            {
                LogHelper.Instance.Log("Error loading/transforming XAML\n{0}", exception);
                result = default(T);
            }
            return result;
        }
    }
}
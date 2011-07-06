// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Lokaldelen AB">
//   Mattias Z
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebReader
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ParseButtonClick(object sender, RoutedEventArgs e)
        {
            var parser = new WebParser(urlField.Text);
            string result = null;
            try
            {
                result = parser.Parse();
            }
            catch (Exception exception)
            {
                result = exception.Message;
            }

            textBlock1.Text = result;

            resultList.Items.Clear();
            foreach (var item in this.LoadResults(result))
            {
                resultList.Items.Add(item);
            }
            
        }

        /// <summary>
        /// Loads the parsed result and produces the requested data
        /// </summary>
        /// <param name="parsedPage">
        /// The parsed page.
        /// </param>
        private string[] LoadResults(string parsedPage)
        {
            var array = new List<string>();

            // Let's parse out all <img>-tags first so we can potentially id all values later
            var startPos = parsedPage.IndexOf("<img");
            int endPos;
            while (startPos != -1)
            {
                endPos = parsedPage.IndexOf(">", startPos);
                array.Add(parsedPage.Substring(startPos, endPos - startPos + 1).Replace("\n", string.Empty));
                startPos = parsedPage.IndexOf("<img", endPos + 1);
            }

            var imgs = new List<string>();

            // Then we extract only the path itself for the source image
            
            foreach (var item in array)
            {
                startPos = item.IndexOf("src=\"") + 5;
                endPos = item.IndexOf("\"", startPos + 1);
                imgs.Add(item.Substring(startPos, endPos - startPos));
            }

            return imgs.ToArray();
        }
    }
}
